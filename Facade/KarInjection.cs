using System;
using UnityEngine;

namespace Karma
{
    public partial class Kar
    {
        #region Static Facade Methods

        public static object Instance(Type type, string id = null)
        {
            return Current._container.Instance(type, id);
        }

        public static object Singleton(Type type, string id = null)
        {
            return Current._container.Singleton(type, id);
        }

        public static object Resolve(Type type, string id = null)
        {
            return Current._container.Resolve(type, id);
        }

        public static T Singleton<T>(string id = null) where T : class
        {
            return Current._container.Singleton<T>(id);
        }

        public static T Instance<T>(string id = null) where T : class
        {
            return Current._container.Instance<T>(id);
        }

        public static T Resolve<T>(string id = null) where T : class
        {
            return Current._container.Resolve<T>(id);
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