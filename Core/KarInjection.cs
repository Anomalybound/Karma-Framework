using System;
using UnityEngine;
using wLib.Injection;

namespace Karma
{
    public partial class Kar
    {
        #region DiContainer

        #region Generic

        public static IBinderInfo<TImplementation> Bind<TImplementation>()
        {
            return Bind<TImplementation, TImplementation>();
        }

        public static IBinderInfo<TImplementation> Bind<TImplementation>(TImplementation instance)
        {
            return Bind<TImplementation, TImplementation>(instance);
        }

        public static IBinderInfo<TImplementation> Bind<TContract, TImplementation>() where TImplementation : TContract
        {
            return Instance._container.Bind<TContract, TImplementation>();
        }

        public static IBinderInfo<TImplementation> Bind<TContract, TImplementation>(TContract instance)
            where TImplementation : TContract
        {
            return Instance._container.Bind<TContract, TImplementation>(instance);
        }

        #endregion

        #region Non Generic

        public static IBinderInfo Bind(Type contract)
        {
            return Instance._container.Bind(contract);
        }

        public static IBinderInfo Bind(Type contract, Type implementation)
        {
            return Instance._container.Bind(contract, implementation);
        }

        public static IBinderInfo Bind(Type contract, Type implementation, object instance)
        {
            return Instance._container.Bind(contract, implementation, instance);
        }

        #endregion

        public static T Resolve<T>() where T : class
        {
            return Resolve(typeof(T)) as T;
        }

        public static object Resolve(Type contract, bool createMode = false)
        {
            return Instance._container.Resolve(contract, createMode);
        }

        public static void Inject(object target)
        {
            Instance._container.Inject(target);
        }

        public static void InjectGameObject(GameObject target)
        {
            var monos = target.GetComponents<MonoBehaviour>();
            for (var i = 0; i < monos.Length; i++)
            {
                var mono = monos[i];
                if (mono == null) { continue; }

                Inject(mono);
            }
        }

        #endregion

        #region Helpers

        public static bool ContainsSingleton(Type type)
        {
            return Instance._container.ContainsSingleton(type);
        }

        public static void AddSingleton(Type type, object instance)
        {
            Instance._container.AddSingleton(type, instance);
        }

        public static void AddTransient(Type type)
        {
            Instance._container.AddTransient(type);
        }

        #endregion
    }
}