using System.Threading.Tasks;
using UnityEngine;

namespace Karma
{
    public interface IViewLoader
    {
        Task<GameObject> LoadView(string key);

        Task<TView> LoadView<TView>(string key) where TView : IView;
    }
}