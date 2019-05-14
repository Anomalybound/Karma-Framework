using System;

namespace Karma.Injection
{
    public interface IModule : IDisposable
    {
        IDependencyContainer Container { get; }

        void RegisterBindings();
    }
}