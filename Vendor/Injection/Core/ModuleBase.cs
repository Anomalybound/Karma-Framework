namespace Karma.Injection
{
    public abstract class ModuleBase : IModule
    {
        public abstract void RegisterBindings(IDependencyContainer Container);

        public abstract void UnRegisterBindings(IDependencyContainer container);
    }
}