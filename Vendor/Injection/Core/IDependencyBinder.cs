using System;

namespace Karma.Injection
{
    public interface IDependencyBinder
    {
        #region Bind

        IBinderInfo Bind(Type contractType);

        IBinderInfo BindInterfaces(Type implementationType);

        IBinderInfo BindAll(Type implementType);

        IBinderInfo BindInstance(Type instanceType, object instance);

        IBinderInfo<TContract> Bind<TContract>();

        IBinderInfo<TImplementation> BindInterfaces<TImplementation>();

        IBinderInfo<TImplementation> BindAll<TImplementation>();

        IBinderInfo<TImplementation> BindInstance<TImplementation>(TImplementation instance);

        #endregion
    }
}