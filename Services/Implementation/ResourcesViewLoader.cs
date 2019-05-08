using System.Threading.Tasks;
using Plugins.Karma.Utils;
using UnityEngine;

namespace Karma.Services
{
    public class ResourcesViewLoader : IViewLoader
    {
        private ILog Logger { get; }

        public ResourcesViewLoader(ILog logger)
        {
            Logger = logger;
        }

        public async Task<GameObject> LoadView(string key)
        {
            var request = Resources.LoadAsync<GameObject>(key);
            await request;

            var go = request.asset as GameObject;
            if (go != null) { return go; }

            Logger.Warn($"Can't load view at key:{key}", go);
            return default;
        }

        public async Task<TView> LoadView<TView>(string key) where TView : IView
        {
            var request = Resources.LoadAsync<GameObject>(key);
            await request;

            var go = request.asset as GameObject;
            if (go != null)
            {
                var view = go.GetComponent<TView>();
                if (view != null) { return view; }
            }

            Logger.Warn($"Can't load view at key:{key}", go);
            return default;
        }
    }
}