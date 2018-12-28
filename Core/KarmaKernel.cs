using wLib.Injection;

namespace Karma.Core
{
    [ScriptOrder(-10000)]
    public class KarmaKernel : SceneContext
    {
        public static Kar KarInstance { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            KarInstance = Create<Kar>();
        }
    }
}