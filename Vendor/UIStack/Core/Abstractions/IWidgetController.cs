using System;

namespace Karma.UIStack
{
    public interface IWidgetController : IDisposable
    {
        void SetControllerInfo(Widget widget, IUIStack manager, UIMessage message);
        void Initialize();
        void OnDestroy();
    }
}