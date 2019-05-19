using System;
using System.Collections.Generic;

namespace Karma.Injection
{
    public enum AsType
    {
        Singleton,
        Transient
    }

    public enum FromType
    {
        FromNew,
        FromInstance,
        FromMethods,
    }

    public class BindingInfo : IBinderInfo
    {
        public IDependencyContainer Container { get; }

        public List<Type> ContractTypes { get; }

        public Type ImplementType { get; protected set; }

        public object BindingInstance { get; private set; }

        public AsType As { get; private set; } = AsType.Singleton;

        public FromType From { get; protected set; } = FromType.FromNew;

        public Func<IDependencyContainer, object> BuildMethod { get; protected set; }

        public string BindingId { get; private set; }

        public bool BuildImmediately { get; private set; }

        public BindingInfo(IDependencyContainer container)
        {
            Container = container;
            ContractTypes = new List<Type>();
        }

        public IBinderInfo To(Type implementationType)
        {
            ToCheck(implementationType);
            ImplementType = implementationType;

            return this;
        }

        public IBinderInfo To<TImplementation>()
        {
            ToCheck(typeof(TImplementation));
            ImplementType = typeof(TImplementation);

            return this;
        }

        public IBinderInfo AsSingleton()
        {
            As = AsType.Singleton;

            return this;
        }

        public IBinderInfo AsTransient()
        {
            As = AsType.Transient;

            return this;
        }

        public IBinderInfo FromNew()
        {
            From = FromType.FromNew;

            return this;
        }

        public IBinderInfo FromInstance(object instance)
        {
            BindingInstance = instance;
            From = FromType.FromInstance;

            return this;
        }

        public IBinderInfo FromMethod(Func<IDependencyContainer, object> method)
        {
            BuildMethod = method;
            From = FromType.FromMethods;

            return this;
        }

        public IBinderInfo WithId(string id)
        {
            BindingId = id;

            return this;
        }

        public void NonLazy()
        {
            BuildImmediately = true;
        }

        #region Check Functions

        // TODO: warning prompt
        protected static void ToCheck(Type type) { }

        #endregion
    }

    public class BindingInfo<TContract> : BindingInfo, IBinderInfo<TContract>
    {
        public BindingInfo(IDependencyContainer container) : base(container) { }

        public new IBinderInfo<TContract> To(Type implementType)
        {
            ToCheck(implementType);
            ImplementType = implementType;
            return this;
        }

        public new IBinderInfo<TContract> To<TImplement>()
        {
            ToCheck(typeof(TImplement));
            ImplementType = typeof(TImplement);
            return this;
        }

        public new IBinderInfo<TContract> AsSingleton()
        {
            base.AsSingleton();
            return this;
        }

        public new IBinderInfo<TContract> AsTransient()
        {
            base.AsTransient();
            return this;
        }

        public new IBinderInfo<TContract> FromNew()
        {
            base.FromNew();
            return this;
        }

        public IBinderInfo<TContract> FromInstance(TContract instance)
        {
            base.FromInstance(instance);
            return this;
        }

        public IBinderInfo<TContract> FromMethod(Func<IDependencyContainer, TContract> method)
        {
            BuildMethod = container => method(container);
            From = FromType.FromMethods;

            return this;
        }

        public new IBinderInfo<TContract> WithId(string id)
        {
            base.WithId(id);

            return this;
        }
    }
}