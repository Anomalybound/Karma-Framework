using System;
using System.Threading.Tasks;

namespace Karma.UIStack
{
    public interface IUIStack
    {
        Task<int> Push(string widgetName);

        Task<int> Push(string widgetName, UIMessage message);

        Task<int> Push<TWidget>(string widgetName) where TWidget : Widget;

        Task<int> Push<TWidget>(string widgetName, UIMessage message) where TWidget : Widget;

        Task Pop(bool recycle = false);

        Task Pop(Action onDone, bool recycle = false);

        Task ClearPopups();

        Task ClearFixes();

        Task ClearWindows();

        Task ClearAll();

        Task Close(int widgetId, bool recycle = false);
    }
}