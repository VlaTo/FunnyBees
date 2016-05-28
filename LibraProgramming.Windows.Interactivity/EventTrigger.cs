using Windows.UI.Xaml;

namespace LibraProgramming.Windows.Interactivity
{
    public class EventTrigger : EventTriggerBase<object>
    {
        public static readonly DependencyProperty EventNameProperty;

        public string EventName
        {
            get
            {
                return (string)GetValue(EventNameProperty);
            }
            set
            {
                SetValue(EventNameProperty, value);
            }
        }

        public EventTrigger()
        {
        }

        public EventTrigger(string eventName)
        {
            EventName = eventName;
        }

        protected override string GetEventName()
        {
            return EventName;
        }

        static EventTrigger()
        {
            EventNameProperty = DependencyProperty.Register("EventName",
                typeof (string),
                typeof (EventTrigger),
                new PropertyMetadata("Loaded",
                    OnEventNameChanged));
        }
    
        private static void OnEventNameChanged(object source, DependencyPropertyChangedEventArgs args)
        {
            ((EventTriggerBase)source).OnEventNameChanged((string)args.OldValue, (string)args.NewValue);
        }
    }
}
