using System;
using System.Collections;
using System.Threading.Tasks;
using Karma;
using UnityEngine;
using Object = System.Object;

namespace wLib.UIStack
{
    public class Widget : MonoBehaviour, IWidget
    {
        [SerializeField]
        private UILayer _layer = UILayer.Window;

        public int Id { get; private set; }

        public string Path { get; private set; }

        public IWidgetController Controller { get; set; }

        public virtual UILayer Layer => _layer;

        protected IUIManager UIManager { get; private set; }

        protected UIMessage Message { get; private set; } = UIMessage.Empty;

        public void SetManagerInfo(int id, string path, IUIManager manager, UIMessage message)
        {
            Id = id;
            Path = path;
            UIManager = manager;
            Message = message;
        }

        #region Events

        public event Action<Widget> OnDestroyEvent;

        #endregion

        public virtual async Task OnShow()
        {
            await Task.FromResult(default(object));
        }

        public virtual async Task OnHide()
        {
            await Task.FromResult(default(object));
        }

        public virtual async Task OnResume()
        {
            await Task.FromResult(default(object));
        }

        public virtual async Task OnFreeze()
        {
            await Task.FromResult(default(object));
        }

        public void DestroyWidget()
        {
            OnDestroyEvent?.Invoke(this);
        }

        public virtual void OnDestroy()
        {
            DestroyWidget();
        }
    }
}