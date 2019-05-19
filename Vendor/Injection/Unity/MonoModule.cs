using UnityEngine;

namespace Karma.Injection
{
    public abstract class MonoModule : MonoBehaviour, IModule
    {
        public abstract void RegisterBindings(IDependencyContainer Container);

//        public abstract void UnRegisterBindings(IDependencyContainer container);
    }
}