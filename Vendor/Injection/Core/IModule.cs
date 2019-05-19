namespace Karma.Injection
{
    public interface IModule
    {
        void RegisterBindings(IDependencyContainer Container);

//        void UnRegisterBindings(IDependencyContainer container);
    }
}