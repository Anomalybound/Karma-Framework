using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Karma.Injection
{
    public class DiContainer : IDependencyContainer
    {
        protected readonly List<IBinderInfo> BinderInfos = new List<IBinderInfo>();

        protected readonly List<IModule> Modules = new List<IModule>();

        protected readonly Dictionary<(string, Type), object> SingletonInstance =
            new Dictionary<(string, Type), object>();

        protected readonly Dictionary<(string, Type), List<BindingInfo>> ContractTypeLookup =
            new Dictionary<(string, Type), List<BindingInfo>>();

        protected readonly Dictionary<Type, List<(MemberInfo memberInfo, string id)>> MemberInfoCaches =
            new Dictionary<Type, List<(MemberInfo, string)>>();

        protected readonly Queue<object> PendingInjectionQueue = new Queue<object>();

        public IDependencyContainer MountModule(params IModule[] modules)
        {
            Modules.AddRange(modules);

            foreach (var module in modules) { module.RegisterBindings(this); }

            return this;
        }

        public void Build()
        {
            foreach (var binderInfo in BinderInfos) { Build(binderInfo); }
        }

        private void Build(IBinderInfo binderInfo)
        {
            if (!(binderInfo is BindingInfo info)) { return; }

            foreach (var contractType in info.ContractTypes)
            {
                if (ContractTypeLookup.ContainsKey((info.BindingId, contractType)))
                {
                    BindEnumerableType(info.BindingId, contractType, info);
                }
                else { ContractTypeLookup.Add((info.BindingId, contractType), new List<BindingInfo> {info}); }
            }

            if (info.BuildImmediately) { Resolve(info); }
        }

        private void BindEnumerableType(string id, Type contractType, BindingInfo info)
        {
            var enumerableType = GetEnumerableType(contractType);
            if (!ContractTypeLookup.TryGetValue((id, enumerableType), out var binderInfos)) { return; }

            if (binderInfos.Contains(info))
            {
                Debug.Log($"Binding cType: {contractType} eType: {enumerableType}, info: {info}");
            }
            else { binderInfos.Add(info); }
        }

        protected readonly Dictionary<Type, Type> EnumerableTypeCache = new Dictionary<Type, Type>();

        private Type GetEnumerableType(Type contractType)
        {
            if (EnumerableTypeCache.TryGetValue(contractType, out var ret)) { return ret; }

            var enumerableType = typeof(IEnumerable<>).MakeGenericType(contractType);

            EnumerableTypeCache.Add(contractType, enumerableType);

            return enumerableType;
        }

        #region Binds

        public IBinderInfo Bind(Type contractType)
        {
            var info = new BindingInfo(this);
            BindContractType(info, contractType);
            BinderInfos.Add(info);

            return info;
        }

        public IBinderInfo BindInterfaces(Type implementationType)
        {
            var info = new BindingInfo(this);
            var interfaces = implementationType.GetInterfaces();
            foreach (var interfaceType in interfaces) { BindContractType(info, interfaceType); }

            BinderInfos.Add(info);

            return info;
        }

        public IBinderInfo BindAll(Type implementType)
        {
            var info = new BindingInfo(this);
            var interfaces = implementType.GetInterfaces();

            BindContractType(info, implementType);
            foreach (var interfaceType in interfaces) { BindContractType(info, interfaceType); }

            BinderInfos.Add(info);

            return info.To(implementType);
        }

        public IBinderInfo BindInstance(Type instanceType, object instance)
        {
            return Bind(instanceType).FromInstance(instance);
        }

        public IBinderInfo<TContract> Bind<TContract>()
        {
            var info = new BindingInfo<TContract>(this);
            BindContractType(info, typeof(TContract));
            BinderInfos.Add(info);

            return info;
        }

        public IBinderInfo<TImplementation> BindInterfaces<TImplementation>()
        {
            var info = new BindingInfo<TImplementation>(this);
            var interfaces = typeof(TImplementation).GetInterfaces();

            foreach (var interfaceType in interfaces) { BindContractType(info, interfaceType); }

            BinderInfos.Add(info);

            return info;
        }

        public IBinderInfo<TImplementation> BindAll<TImplementation>()
        {
            var info = new BindingInfo<TImplementation>(this);
            var interfaces = typeof(TImplementation).GetInterfaces();

            BindContractType(info, typeof(TImplementation));
            foreach (var interfaceType in interfaces) { BindContractType(info, interfaceType); }

            BinderInfos.Add(info);

            return info.To<TImplementation>();
        }

        public IBinderInfo<TImplementation> BindInstance<TImplementation>(TImplementation instance)
        {
            return Bind<TImplementation>().FromInstance(instance);
        }

        #endregion

        public object Instance(Type contract, string id = null)
        {
            return OperationHelper(Instance, contract, id);
        }

        public object Resolve(Type contract, string id = null)
        {
            return OperationHelper(Resolve, contract, id);
        }

        public object Singleton(Type contract, string id = null)
        {
            return OperationHelper(Singleton, contract, id);
        }

        public void InjectGameObject(GameObject target)
        {
            var behaviours = target.GetComponents<MonoBehaviour>();
            foreach (var monoBehaviour in behaviours) { Inject(monoBehaviour); }
        }

        public T Inject<T>(T target)
        {
            var contractType = target.GetType();

            if (!MemberInfoCaches.TryGetValue(contractType, out var injectedMembers))
            {
                injectedMembers = new List<(MemberInfo, string)>();
                var members = contractType
                    .GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(x => x.CustomAttributes.Any(a => a.AttributeType == typeof(Inject)));

                var memberInfos = members as MemberInfo[] ?? members.ToArray();
                var mappedInfos =
                    memberInfos.Select(m => ValueTuple.Create(m, m.GetCustomAttribute<Inject>().BindingId));
                injectedMembers.AddRange(mappedInfos);

                MemberInfoCaches.Add(contractType, injectedMembers);
            }

            var memberInfoCaches = MemberInfoCaches[contractType];

            foreach (var (memberInfo, id) in memberInfoCaches)
            {
                object instance;
                switch (memberInfo.MemberType)
                {
                    case MemberTypes.Field:
                        var fieldInfo = memberInfo as FieldInfo;
                        if (fieldInfo != null)
                        {
                            instance = Resolve(fieldInfo.FieldType, id);
                            fieldInfo.SetValue(target, instance);
                        }

                        break;
                    case MemberTypes.Property:
                        var propertyInfo = memberInfo as PropertyInfo;
                        if (propertyInfo != null && propertyInfo.SetMethod != null)
                        {
                            instance = Resolve(propertyInfo.PropertyType, id);
                            propertyInfo.SetValue(target, instance);
                        }

                        break;
                    case MemberTypes.Method:
                        var methodInfo = memberInfo as MethodInfo;
                        if (methodInfo != null)
                        {
                            var parameters = methodInfo.GetParameters();

                            var invokeParameter = new object[parameters.Length];
                            for (var i = 0; i < parameters.Length; i++)
                            {
                                var parameter = parameters[i];
                                invokeParameter[i] = Resolve(parameter.ParameterType);
                            }

                            methodInfo.Invoke(target, invokeParameter);
                        }

                        break;
                }
            }

            return target;
        }

        public T Instance<T>(string id = null) where T : class
        {
            return Instance(typeof(T), id) as T;
        }

        public T Singleton<T>(string id = null) where T : class
        {
            return Singleton(typeof(T), id) as T;
        }

        public T Resolve<T>(string id = null) where T : class
        {
            return Resolve(typeof(T), id) as T;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) { return; }

            MemberInfoCaches.Clear();
            BinderInfos.Clear();
            Modules.Clear();
            SingletonInstance.Clear();
            ContractTypeLookup.Clear();
            PendingInjectionQueue.Clear();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #region Internal Implementations

        protected object OperationHelper(Func<BindingInfo, object> operation, Type contractType, string id)
        {
            if (!ContractTypeLookup.TryGetValue((id, contractType), out var binderInfos)) { return null; }

            if (binderInfos.Count == 0) { throw new Exception($"Can not resolve type: {contractType}."); }

            if (binderInfos.Count == 1) { return operation.Invoke(binderInfos[0]); }

            var enumeratorType = typeof(List<>).MakeGenericType(contractType);

            if (!(Activator.CreateInstance(enumeratorType) is IList list))
            {
                throw new Exception($"Something went wrong when resolving of type: {contractType}.");
            }

            // Resolving Enumerable Type
            foreach (var binderInfo in binderInfos) { list.Add(operation.Invoke(binderInfo)); }

            return enumeratorType;
        }

        protected object Resolve(BindingInfo info)
        {
            switch (info.As)
            {
                case AsType.Singleton:
                    return Singleton(info);

                case AsType.Transient:
                    return Create(info);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected object Instance(BindingInfo info) => Create(info);

        protected object Singleton(BindingInfo info)
        {
            object ret = null;

            var id = info.BindingId;
            foreach (var contractType in info.ContractTypes)
            {
                if (SingletonInstance.TryGetValue((id, contractType), out ret)) { break; }
            }

            // No instances were found, create new instance
            if (ret != null) { return ret; }

            ret = Create(info);
            foreach (var contractType in info.ContractTypes) { SingletonInstance.Add((id, contractType), ret); }

            return ret;
        }

        protected object Create(BindingInfo info)
        {
            var targetType = info.ImplementType;
            switch (info.From)
            {
                case FromType.FromNew:
                    var constructors =
                        targetType.GetConstructors(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Default);

                    // Only react to the first available constructor
                    for (var j = 0; j < constructors.Length;)
                    {
                        var constructor = constructors[j];
                        var parameterInfos = constructor.GetParameters();

                        if (parameterInfos.Length == 0) { return Activator.CreateInstance(targetType); }

                        var parameters = new List<object>(parameterInfos.Length);
                        var resolvedData = parameterInfos.Select(parameterInfo => Resolve(parameterInfo.ParameterType));
                        parameters.AddRange(resolvedData);

                        return Inject(constructor.Invoke(parameters.ToArray()));
                    }

                    throw new Exception($"Unable to use From New Scope on {targetType}, no suitable Constructor");

                case FromType.FromInstance:
                    return info.BindingInstance;
                case FromType.FromMethods:
                    return info.BuildMethod.Invoke(this);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        #region Helpers

        public void BindContractType(BindingInfo info, Type contractType)
        {
            if (info.ContractTypes.Contains(contractType)) { return; }

            info.ContractTypes.Add(contractType);
        }

        #endregion
    }
}