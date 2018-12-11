using wLib;

namespace Karma
{
    public partial class Kar
    {
        #region Events

        public static void On(string eventName, EventAction action)
        {
            Instance._eventBroker.Subscribe(eventName, action);
        }

        public static void On<T>(string eventName, EventAction<T> action)
        {
            Instance._eventBroker.Subscribe(eventName, action);
        }

        public static void Un(string eventName, EventAction action, bool keepEvent = false)
        {
            Instance._eventBroker.Unsubscribe(eventName, action, keepEvent);
        }

        public static void Un<T>(string eventName, EventAction<T> action, bool keepEvent = false)
        {
            Instance._eventBroker.Unsubscribe(eventName, action, keepEvent);
        }

        public static void Fire(string eventName)
        {
            Instance._eventBroker.Publish(eventName);
        }

        public static void Fire<T>(string eventName, T message)
        {
            Instance._eventBroker.Publish(eventName, message);
        }

        #endregion
    }
}