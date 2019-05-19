using System;
using UnityEngine;

namespace Karma
{
    public partial class Kar
    {
        #region Static Facade Methods

        public static object Instance(Type type)
        {
            return Current._container.Instance(type);
        }

        public static T Instance<T>() where T : class
        {
            return Current._container.Instance<T>();
        }

        public static object Singleton(Type type)
        {
            return Current._container.Singleton(type);
        }

        public static T Singleton<T>() where T : class
        {
            return Current._container.Singleton<T>();
        }

        public static object Resolve(Type type)
        {
            return Current._container.Resolve(type);
        }

        public static T Resolve<T>() where T : class
        {
            return Current._container.Resolve<T>();
        }

        public static T Inject<T>(T target)
        {
            return Current._container.Inject(target);
        }

        public static void InjectGameObject(GameObject target)
        {
            Current._container.InjectGameObject(target);
        }

        #endregion
    }
}