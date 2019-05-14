using System;
using Karma.Injection;
using UnityEngine;

namespace Karma
{
    public partial class Kar
    {
        #region DiContainer

        public static T Create<T>() where T : class
        {
            return Create(typeof(T)) as T;
        }

        public static T Resolve<T>() where T : class
        {
            return Resolve(typeof(T)) as T;
        }

        public static object Create(Type contract)
        {
            return Instance._container.Resolve(contract, true);
        }

        public static object Resolve(Type contract)
        {
            return Instance._container.Resolve(contract);
        }

        public static void Inject(object target)
        {
            Instance._container.Inject(target);
        }

        public static void Inject(GameObject target)
        {
            var components = target.GetComponents<MonoBehaviour>();
            foreach (var mono in components)
            {
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