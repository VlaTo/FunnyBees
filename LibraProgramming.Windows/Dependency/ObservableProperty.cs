using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace LibraProgramming.Windows.Dependency
{
    public sealed class ObservableProperty
    {
        private static readonly IDictionary<Type, OwnedPropertyCollection> registry;
        private static readonly object sync;

        private readonly Type ownerType;
        private readonly IEqualityComparer comparer;

        internal PropertyKey Key
        {
            get;
        }

        public static ObservableProperty Register(string propertyName, Type propertyType, Type ownerType, ObservablePropertyMetadata metadata = null)
        {
            if (null == propertyName)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (0 == propertyName.Length)
            {
                throw new ArgumentException("", nameof(propertyName));
            }

            lock (sync)
            {
                OwnedPropertyCollection properties;

                if (false == registry.TryGetValue(ownerType, out properties))
                {
                    properties = new OwnedPropertyCollection();
                    registry.Add(ownerType, properties);
                }

                var key = new PropertyKey(propertyName, propertyType);

                if (properties.ContainsKey(key))
                {
                    throw new InvalidOperationException();
                }

                properties.Add(key, metadata);

                var property = new ObservableProperty(key, ownerType);

                return property;
            }
        }

        static ObservableProperty()
        {
            sync = new object();
            registry = new Dictionary<Type, OwnedPropertyCollection>();
        }

        public ObservablePropertyMetadata GetMetadata()
        {
            lock (sync)
            {
                OwnedPropertyCollection properties;

                if (false == registry.TryGetValue(ownerType, out properties))
                {
                    throw new InvalidOperationException();
                }

                ObservablePropertyMetadata metadata;

                if (false == properties.TryGetValue(Key, out metadata))
                {
                    throw new InvalidOperationException();
                }

                return metadata;
            }
        }

        internal object GetValue(ConcurrentDictionary<PropertyKey, object> properties)
        {
            object value;

            if (false == properties.TryGetValue(Key, out value))
            {
                var metadata = GetMetadata();
                value = metadata.DefaultValue;
            }

            var type = Key.PropertyType;

            if (type != value.GetType())
            {
                throw new InvalidDataException();
            }

            return value;
        }

        internal bool SetValue(ObservableModel model, IDictionary<PropertyKey, object> properties, object value)
        {
            lock (((ICollection)properties).SyncRoot)
            {
                var metadata = GetMetadata();

                object current;

                if (false == properties.TryGetValue(Key, out current))
                {
                    current = metadata.DefaultValue;
                }

                if (null != comparer && comparer.Equals(current, value))
                {
                    return false;
                }

                properties[Key] = value;

                metadata.RaisePropertyChanged(model, this, current, value);
            }

            return true;
        }


        private ObservableProperty(PropertyKey propertyKey, Type ownerType)
        {
            this.ownerType = ownerType;
            Key = propertyKey;
            comparer = GetComparer();
        }

        private IEqualityComparer GetComparer()
        {
            var type = typeof(EqualityComparer<>).MakeGenericType(Key.PropertyType);
            var p = type.GetRuntimeProperty("Default");
            return p.GetValue(null) as IEqualityComparer;
        }

        /// <summary>
        /// 
        /// </summary>
        private class OwnedPropertyCollection : Dictionary<PropertyKey, ObservablePropertyMetadata>
        {

        }
    }
}