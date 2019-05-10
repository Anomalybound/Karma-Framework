using wLib.UIStack;
using System.Threading.Tasks;

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
            return await Instance._uiManager.Push<TWidget>(widgetName, message);
        }

        public static async void Pop(bool recycle = false)
        {
            await Instance._uiManager.Pop(recycle);
        }

        public static async void ClearPopups()
        {
            await Instance._uiManager.ClearPopups();
        }

        public static async void ClearFixes()
        {
            await Instance._uiManager.ClearFixes();
        }

        public static async void ClearWindows()
        {
            await Instance._uiManager.ClearWindows();
        }

        public static async void ClearAll()
        {
            await Instance._uiManager.ClearAll();
        }

        public static async void Close(int widgetId, bool recycle = false)
        {
            await Instance._uiManager.Close(widgetId, recycle);
        }

        #endregion
    }
}