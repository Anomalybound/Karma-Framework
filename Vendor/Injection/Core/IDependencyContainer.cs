using System;

namespace Karma.Injection
{
    public interface IDependencyContainer : IDisposable
    {
        #region Generic Binding
        
        IBinderInfo<TImplementation> BindAll<TImplementation>();
        
        IBinderInfo<TImplementation> BindAll<TImplementation>(TImplementation instance);

        IBinderInfo<TImplementation> Bind<TImplementation>();

        IBinderInfo<TImplementation> Bind<TImplementation>(TImplementation instance);

        IBinderInfo<TImplementation> Bind<TContract, TImplementation>() where TImplementation : TContract;

        IBinderInfo<TImplementation> Bind<TContract, TImplementation>(TContract instance)
            where TImplementation : TContract;

        #endregion

        #region Non Generic

        IBinderInfo BindAll(Type implementation);
        

        IBinderInfo BindAll(Type implementation, object instance);

        IBinderInfo Bind(Type implementation);
        

        IBinderInfo Bind(Type implementation, object instance);

        IBinderInfo Bind(Type contract, Type implementation);

        IBinderInfo Bind(Type contract, Type implementation, object instance);

        #endregion

        #region Helpers

        bool ContainsSingleton(Type type);

        void AddSingleton(Type type, object instance);

        void AddTransient(Type type);

        #endregion

        T Resolve<T>() where T : class;

        object Resolve(Type contract, bool createMode = false);

        T Inject<T>(T target);

        IDependencyContainer MountModule(params IModule[] modules);
    }
}