using System;

namespace Karma.Injection
{
    public interface IDependencyContainer : IDisposable, IDependencyBinder, IDependencyResolver
    {
        #region Container

        IDependencyContainer MountModule(params IModule[] modules);

        void Build();

        #endregion
    }
}