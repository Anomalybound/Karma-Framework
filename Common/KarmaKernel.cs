using Karma.Injection;

namespace Karma.Common
{
    [ScriptOrder(-10000)]
    public class KarmaKernel : SceneContext
    {
        public static Kar KarInstance { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            KarInstance = Singleton<Kar>();
        }
    }
}