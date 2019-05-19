using System;

namespace Karma.Injection
{
    public interface IBinderInfo
    {
        #region To

        IBinderInfo To(Type implementationType);

        IBinderInfo To<TImplementation>();

        #endregion

        #region AsScope

        IBinderInfo AsSingleton();

        IBinderInfo AsTransient();

        #endregion

        #region From

        IBinderInfo FromNew();

        IBinderInfo FromInstance(object instance);

        IBinderInfo FromMethod(Func<IDependencyContainer, object> method);

        #endregion

        #region Identifier

        IBinderInfo WithId(string id);

        #endregion

        #region Non Lazy

        void NonLazy();

        #endregion
    }

    public interface IBinderInfo<in TContract> : IBinderInfo
    {
        #region To

        new IBinderInfo<TContract> To(Type implementType);

        new IBinderInfo<TContract> To<TImplement>();

        #endregion

        #region AsScope

        new IBinderInfo<TContract> AsSingleton();

        new IBinderInfo<TContract> AsTransient();

        #endregion

        #region From

        new IBinderInfo<TContract> FromNew();

        IBinderInfo<TContract> FromInstance(TContract instance);

        IBinderInfo<TContract> FromMethod(Func<IDependencyContainer, TContract> method);

        #endregion

        #region Identifier

        new IBinderInfo<TContract> WithId(string id);

        #endregion
    }
}