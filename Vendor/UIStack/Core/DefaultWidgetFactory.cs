using System.Threading.Tasks;
using Karma;
using Object = UnityEngine.Object;

namespace wLib.UIStack
{
    [CustomWidgetFactory(typeof(Widget))]
    public class DefaultWidgetFactory : IWidgetFactory<Widget>
    {
        public async Task<Widget> CreateInstance(IUIManager manager, string name, int assignedId,
            UIMessage message)
        {
            var loader = Kar.Resolve<IViewLoader>();
            var prefab = await loader.LoadView(name);
            var instance = Object.Instantiate(prefab).GetComponent<Widget>();
            instance.SetManagerInfo(assignedId, name, manager, message);
            Kar.Inject(instance);
            instance.OnDestroyEvent += ReturnInstance;
            return instance;
        }

        public void ReturnInstance(Widget widget)
        {
            widget.OnDestroyEvent -= ReturnInstance;
            Object.Destroy(widget);
        }

        async Task<IWidget> IWidgetFactory.CreateInstance(IUIManager manager, string name, int assignedId,
            UIMessage message)
        {
            var loader = Kar.Resolve<IViewLoader>();
            var prefab = await loader.LoadView(name);
            var instance = Object.Instantiate(prefab).GetComponent<IWidget>();
            instance.SetManagerInfo(assignedId, name, manager, message);
            Kar.Inject(instance);
            return instance;
        }
    }
}