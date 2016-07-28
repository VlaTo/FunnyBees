using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LibraProgramming.Windows.Dependency;
using LibraProgramming.Windows.Infrastructure;

namespace LibraProgramming.Windows
{
    public class ObservableModel : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private readonly ConcurrentDictionary<PropertyKey, object> properties; 
        private readonly WeakEvent<PropertyChangedEventHandler> propertyChanged;
        private readonly WeakEvent<PropertyChangingEventHandler> propertyChanging;

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging
        {
            add
            {
                propertyChanging.AddHandler(value);
            }
            remove
            {
                propertyChanging.RemoveHandler(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                propertyChanged.AddHandler(value);
            }
            remove
            {
                propertyChanged.RemoveHandler(value);
            }
        }

        protected ObservableModel()
        {
            propertyChanging = new WeakEvent<PropertyChangingEventHandler>();
            propertyChanged = new WeakEvent<PropertyChangedEventHandler>();
            properties = new ConcurrentDictionary<PropertyKey, object>();
        }

        protected object GetProperty(ObservableProperty property)
        {
            if (null == property)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return property.GetValue(properties);
        }

        protected void SetProperty(ObservableProperty property, object value)
        {
            if (null == property)
            {
                throw new ArgumentNullException(nameof(property));
            }

            var name = property.Key.PropertyName;

            propertyChanging.Invoke(this, new PropertyChangingEventArgs(name));

            if (property.SetValue(this, properties, value))
            {
                propertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}