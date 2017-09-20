using System;
using System.Collections.Generic;
using LibraProgramming.FunnyBees.Interop;

namespace LibraProgramming.FunnyBees.Models
{
    public class Entity : IEntity
    {
        private readonly IDictionary<EntityProperty, object> properties;

        public object this[EntityProperty property]
        {
            get
            {
                if (null == property)
                {
                    throw new ArgumentNullException(nameof(property));
                }

                object value;

                if (false == properties.TryGetValue(property, out value))
                {
                    throw new ArgumentException("", nameof(property));
                }

                return value;
            }
            set
            {
                properties[property] = value;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        protected Entity()
        {
            properties = new Dictionary<EntityProperty, object>();
        }

        public bool PropertyExists(EntityProperty property)
        {
            if (null == property)
            {
                throw new ArgumentNullException(nameof(property));
            }

            return properties.ContainsKey(property);
        }
    }
}