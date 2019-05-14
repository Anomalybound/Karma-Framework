using System;

namespace Karma.UIStack
{
    public class CustomWidgetFactoryAttribute : Attribute
    {
        public Type WidgetType { get; }

        public CustomWidgetFactoryAttribute(Type widgetType)
        {
            WidgetType = widgetType;
        }
    }
}