namespace LibraProgramming.Windows.Dependency
{
    public abstract class ObservablePropertyMetadata
    {
        public object DefaultValue
        {
            get;
        }

        protected ObservablePropertyMetadata(object defaultValue)
        {
            DefaultValue = defaultValue;
        }

        protected internal abstract void RaisePropertyChanged(ObservableModel model, ObservableProperty property, object current, object value);
    }
}