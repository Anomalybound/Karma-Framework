using Karma.UIStack;

namespace Karma.View
{
    public abstract class UIViewBase : Widget, IUIView
    {
        public abstract void SetViewModel(object context);
    }
}