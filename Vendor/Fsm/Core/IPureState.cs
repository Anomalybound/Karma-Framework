using System.Threading.Tasks;

namespace Karma.Fsm
{
    public interface IPureState
    {
        #region Lifetime

        Task Enter();

        void Update(float deltaTime);

        Task Exit();

        float ElapsedTime { get; }

        #endregion
    }
}