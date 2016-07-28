namespace LibraProgramming.Windows.Dependency
{
    /// <summary>
    /// 
    /// </summary>
    public class ObservablePropertyChangedEventArgs
    {
        public ObservableProperty Property
        {
            get;
        }

        public object NewValue
        {
            get;
        }

        public object OldValue
        {
            get;
        }

        internal ObservablePropertyChangedEventArgs(ObservableProperty property, object newValue, object oldValue)
        {
            Property = property;
            NewValue = newValue;
            OldValue = oldValue;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="e"></param>
    public delegate void ObservablePropertyChangedCallback(ObservableModel model, ObservablePropertyChangedEventArgs e);

    /// <summary>
    /// 
    /// </summary>
    public class ObservableMetadata : ObservablePropertyMetadata
    {
        private readonly ObservablePropertyChangedCallback callback;

        public ObservableMetadata(object defaultValue, ObservablePropertyChangedCallback callback = null)
            : base(defaultValue)
        {
            this.callback = callback;
        }

        protected internal override void RaisePropertyChanged(ObservableModel model, ObservableProperty property, object current, object value)
        {
            callback?.Invoke(model, new ObservablePropertyChangedEventArgs(property, value, current));
        }
    }
}