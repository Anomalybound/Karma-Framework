using System.Threading.Tasks;
using Karma.UIStack;

namespace Karma
{
    public partial class Kar
    {
        #region UI Manager

        public static async Task<int> Push(string widgetName)
        {
            return await Push<Widget>(widgetName);
        }

        public static async Task<int> Push(string widgetName, UIMessage message)
        {
            return await Push<Widget>(widgetName, message);
        }

        public static async Task<int> Push<TWidget>(string widgetName) where TWidget : Widget
        {
            return await Push<TWidget>(widgetName, UIMessage.Empty);
        }

        public static async Task<int> Push<TWidget>(string widgetName, UIMessage message)
            where TWidget : Widget
        {
            return await Instance._iuiStack.Push<TWidget>(widgetName, message);
        }

        public static async void Pop(bool recycle = false)
        {
            await Instance._iuiStack.Pop(recycle);
        }

        public static async void ClearPopups()
        {
            await Instance._iuiStack.ClearPopups();
        }

        public static async void ClearFixes()
        {
            await Instance._iuiStack.ClearFixes();
        }

        public static async void ClearWindows()
        {
            await Instance._iuiStack.ClearWindows();
        }

        public static async void ClearAll()
        {
            await Instance._iuiStack.ClearAll();
        }

        public static async void Close(int widgetId, bool recycle = false)
        {
            await Instance._iuiStack.Close(widgetId, recycle);
        }

        #endregion
    }
}