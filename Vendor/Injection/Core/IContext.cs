using System;

namespace Karma.Injection
{
    public interface IContext
    {
        IDependencyContainer Container { get; }

        object Create(Type type);

        T Create<T>() where T : class;

        object Resolve(Type type);

        T Resolve<T>() where T : class;

        T Inject<T>(T target);
    }
}