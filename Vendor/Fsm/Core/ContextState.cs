using System.Threading.Tasks;

namespace Karma.Fsm
{
    public abstract class ContextState<TContext> : State
    {
        public abstract TContext Context { get; protected set; }
        
        public abstract Task SetContext(TContext context);
    }
}