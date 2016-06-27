using System;
using System.Collections.Generic;

namespace FunnyBees.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class ComponentContainer
    {
        private readonly IDictionary<Type, IComponent> components;

        protected ComponentContainer()
        {
            components = new Dictionary<Type, IComponent>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        public void AddComponent(IComponent component)
        {
            if (null == component)
            {
                throw new ArgumentNullException(nameof(component));
            }

            if (HasComponentInternal(component.GetType()))
            {
                throw new ArgumentException();
            }

            components.Add(component.GetType(), component);

            component.Attach(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <returns></returns>
        public TComponent GetComponent<TComponent>()
            where TComponent : IComponent
        {
            IComponent component;

            if (false == components.TryGetValue(typeof (TComponent), out component))
            {
                return default(TComponent);
            }

            return (TComponent) component;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <returns></returns>
        public bool HasComponent<TComponent>()
            where TComponent : IComponent
        {
            return HasComponentInternal(typeof (TComponent));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public bool RemoveComponent(IComponent component)
        {
            if (null == component)
            {
                throw new ArgumentNullException(nameof(component));
            }

            if (false == HasComponentInternal(component.GetType()))
            {
                return false;
            }

            if (components.Remove(component.GetType()))
            {
                component.Remove();
                return true;
            }

            return false;
        }

        public IInteraction InteractWith(ComponentContainer container)
        {
            if (null == container)
            {
                throw new ArgumentNullException(nameof(container));
            }

            return new Interaction(this, container);
        }

        private bool HasComponentInternal(Type targetType)
        {
            return components.ContainsKey(targetType);
        }
    }
}