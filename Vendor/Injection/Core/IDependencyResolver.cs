using System;
using UnityEngine;

namespace Karma.Injection
{
    public interface IDependencyResolver
    {
        object Instance(Type contract, string id = null);

        T Instance<T>(string id = null) where T : class;

        object Singleton(Type contract, string id = null);

        T Singleton<T>(string id = null) where T : class;

        object Resolve(Type contract, string id = null);

        T Resolve<T>(string id = null) where T : class;

        T Inject<T>(T target);

        void InjectGameObject(GameObject target);
    }
}