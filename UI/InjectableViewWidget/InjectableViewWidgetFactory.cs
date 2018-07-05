using System;
using wLib.Injection;
using wLib.UIStack;
using Object = UnityEngine.Object;

namespace Karma.UI
{
    [CustomWidgetFactory(typeof(InjectableViewWidget))]
    public class InjectableViewWidgetFactory : ViewWidgetFactory, IWidgetFactory<InjectableViewWidget>
    {
        private readonly SceneContext _context;

        public InjectableViewWidgetFactory()
        {
            _context = Object.FindObjectOfType<SceneContext>();
        }

        public override void CreateInstance(IUIManager manager, string widgetName, int assignedId,
            Action<BaseWidget> onCreated)
        {
            CreateInstance(manager, widgetName, assignedId, widget => onCreated.Invoke(widget));
        }

        public override object GetInstance(Type instanceType)
        {
            return _context.Create(instanceType);
        }

        public void CreateInstance(IUIManager manager, string widgetName, int assignedId,
            Action<InjectableViewWidget> onCreated)
        {
            CreateInstance(manager, widgetName, assignedId,
                (ViewWidget widget) => { onCreated.Invoke((InjectableViewWidget) widget); });
        }
    }
}