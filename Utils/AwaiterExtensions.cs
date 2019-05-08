using UnityEngine;

namespace Plugins.Karma.Utils
{
    public static class AwaiterExtensions
    {
        public static ResourcesRequestAwaiter GetAwaiter(this ResourceRequest asyncOp) =>
            new ResourcesRequestAwaiter(asyncOp);
    }
}