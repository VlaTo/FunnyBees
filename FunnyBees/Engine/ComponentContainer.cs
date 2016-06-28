﻿using System;
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
        public void AddComponent<TComponent>()
            where TComponent : IComponent, new ()
        {
            if (IsComponentExists(typeof(TComponent)))
            {
                throw new ArgumentException();
            }

            var component = new TComponent();

            components.Add(typeof(TComponent), component);

            component.Attach(this);
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddComponent<TComponent>(Func<TComponent> builder)
            where TComponent : IComponent
        {
            if (null == builder)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (IsComponentExists(typeof (TComponent)))
            {
                throw new ArgumentException();
            }

            var component = builder.Invoke();

            components.Add(typeof (TComponent), component);

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
            return IsComponentExists(typeof (TComponent));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public bool RemoveComponent<TComponent>()
            where TComponent : IComponent
        {
            var component = FindComponentInstance(typeof (TComponent));

            if (null == component)
            {
                return false;
            }

            if (components.Remove(typeof(TComponent)))
            {
                component.Remove();
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        /*public bool RemoveComponent(IComponent component)
        {
            if (null == component)
            {
                throw new ArgumentNullException(nameof(component));
            }

            if (false == IsComponentExists(component.GetType()))
            {
                return false;
            }

            if (components.Remove(component.GetType()))
            {
                component.Remove();
                return true;
            }

            return false;
        }*/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public IInteraction InteractWith(ComponentContainer other)
        {
            if (null == other)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return new Interaction(this, other);
        }

        private bool IsComponentExists(Type targetType)
        {
            return components.ContainsKey(targetType);
        }

        private IComponent FindComponentInstance(Type targetType)
        {
            IComponent component;

            if (components.TryGetValue(targetType, out component))
            {
                return component;
            }

            return null;
        }
    }
}