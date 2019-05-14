using Karma.Fsm;

namespace Karma.Procedure
{
    public abstract class ProcedureBase<TProcedureManager> : ContextState<TProcedureManager>
    {
        public override TProcedureManager Context { get; protected set; }

        public override void SetContext(TProcedureManager context)
        {
            Context = context;
        }
    }
}