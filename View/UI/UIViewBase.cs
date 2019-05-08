using wLib.Injection;
using wLib.UIStack;

namespace Karma.View
{
    public abstract class UIViewBase : Widget, IUIView
    {
        [Inject]
        protected ILog Logger { get; }
        
        public abstract void SetDataContext(object context);
    }
}