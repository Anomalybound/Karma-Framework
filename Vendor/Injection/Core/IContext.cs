namespace Karma.Injection
{
    public interface IContext : IDependencyResolver
    {
        IDependencyContainer Container { get; }
    }
}