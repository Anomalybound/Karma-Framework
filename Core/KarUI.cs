using System;
using wLib.UIStack;

namespace Karma
{
    public partial class Kar
    {
        #region UI Manager

        public static void Push(string widgetName)
        {
            Push<Widget>(widgetName, UIMessage.Empty, null);
        }

        public static void Push(string widgetName, Action<int> onCreated)
        {
            Push<Widget>(widgetName, UIMessage.Empty, onCreated);
        }

        public static void Push(string widgetName, UIMessage message)
        {
            Push<Widget>(widgetName, message, null);
        }

        public static void Push(string widgetName, UIMessage message, Action<int> onCreated)
        {
            Push<Widget>(widgetName, message, onCreated);
        }

        public static void Push<TWidget>() where TWidget : Widget
        {
            Push<TWidget>(null, UIMessage.Empty, null);
        }

        public static void Push<TWidget>(Action<int> onCreated) where TWidget : Widget
        {
            Push<TWidget>(null, UIMessage.Empty, onCreated);
        }

        public static void Push<TWidget>(UIMessage message) where TWidget : Widget
        {
            Push<TWidget>(null, message, null);
        }

        public static void Push<TWidget>(UIMessage message, Action<int> onCreated) where TWidget : Widget
        {
            Push<TWidget>(null, UIMessage.Empty, null);
        }

        public static void Push<TWidget>(string widgetName) where TWidget : Widget
        {
            Push<TWidget>(widgetName, UIMessage.Empty, null);
        }

        public static void Push<TWidget>(string widgetName, Action<int> onCreated) where TWidget : Widget
        {
            Push<TWidget>(widgetName, UIMessage.Empty, onCreated);
        }

        public static void Push<TWidget>(string widgetName, UIMessage message) where TWidget : Widget
        {
            Push<TWidget>(widgetName, message, null);
        }

        public static void Push<TWidget>(string widgetName, UIMessage message, Action<int> onCreated)
            where TWidget : Widget
        {
            Instance._uiManager.Push<TWidget>(widgetName, message, onCreated);
        }

        public static void Pop(bool recycle = false)
        {
            Instance._uiManager.Pop(recycle);
        }

        public static void Pop(Action onDone, bool recycle = false)
        {
            Instance._uiManager.Pop(onDone, recycle);
        }

        public static void ClearPopups()
        {
            Instance._uiManager.ClearPopups();
        }

        public static void ClearFixes()
        {
            Instance._uiManager.ClearFixes();
        }

        public static void ClearWindows()
        {
            Instance._uiManager.ClearWindows();
        }

        public static void ClearAll()
        {
            Instance._uiManager.ClearAll();
        }

        public static void Close(int widgetId, bool recycle = false)
        {
            Instance._uiManager.Close(widgetId, recycle);
        }

        public static void Close(int widgetId, Action onClosed, bool recycle = false)
        {
            Instance._uiManager.Close(widgetId, onClosed, recycle);
        }

        #endregion
    }
}