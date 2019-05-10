using System.Threading.Tasks;

namespace wLib.UIStack
{
    public interface IWidgetFactory
    {
        Task<IWidget> CreateInstance(IUIManager manager, string name, int assignedId, UIMessage message);
    }

    public interface IWidgetFactory<TWidget> : IWidgetFactory where TWidget : Widget
    {
        new Task<TWidget> CreateInstance(IUIManager manager, string name, int assignedId, UIMessage message);

        void ReturnInstance(TWidget widget);
    }
}