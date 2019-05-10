using System.Threading.Tasks;
using UnityEngine;

namespace wLib.UIStack
{
    public interface IWidget
    {
        Task OnShow();

        Task OnHide();

        Task OnResume();

        Task OnFreeze();

        void DestroyWidget();

        void SetManagerInfo(int id, string path, IUIManager manager, UIMessage message);
    }
}