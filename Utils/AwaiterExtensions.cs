using UnityEngine;
using Karma.Utils;

namespace Karma
{
    public static class AwaiterExtensions
    {
        public static ResourcesRequestAwaiter GetAwaiter(this ResourceRequest self)
        {
            return new ResourcesRequestAwaiter(self);
        }

#if KARMA_DOTWEEN
        public static TweenAwaiter GetAwaiter(this DG.Tweening.Tween self)
        {
            return new TweenAwaiter(self);
        }

        public static TweenAwaiter GetCancellableAwaiter(this DG.Tweening.Tween self,
            System.Threading.CancellationToken cancellationToken = default)
        {
            return new TweenAwaiter(self, cancellationToken);
        }
#endif
    }
}