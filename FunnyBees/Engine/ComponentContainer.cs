using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LibraProgramming.Windows;

namespace FunnyBees.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class ComponentContainer : IObservable<IComponentObserver>
    {
        private readonly IDictionary<Type, IComponent> components;
        private readonly ICollection<IComponentObserver> componentObservers;

        protected IEnumerable<IComponent> Components => components.Values;

        protected ComponentContainer()
        {
            components = new Dictionary<Type, IComponent>();
            componentObservers = new List<IComponentObserver>();
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddComponent<TComponent>()
            where TComponent : IComponent, new()
        {
            if (IsComponentExists(typeof (TComponent)))
            {
                throw new ArgumentException();
            }

            var component = new TComponent();

            components.Add(typeof (TComponent), component);

            var observers = componentObservers.ToArray();

            component.Attach(this);

            foreach (var observer in observers)
            {
                observer.OnAttached(component);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddComponent<TComponent>(TComponent component)
            where TComponent : IComponent
        {
            if (null == component)
            {
                throw new ArgumentNullException(nameof(component));
            }

            if (IsComponentExists(typeof (TComponent)))
            {
                throw new ArgumentException();
            }

            components.Add(typeof (TComponent), component);

            var observers = componentObservers.ToArray();

            component.Attach(this);

            foreach (var observer in observers)
            {
                observer.OnAttached(component);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <returns></returns>
        public TComponent GetComponent<TComponent>(bool failIfNotExists = true)
            where TComponent : IComponent
        {
            IComponent component;

            if (false == components.TryGetValue(typeof (TComponent), out component))
            {
                if (failIfNotExists)
                {
                    throw new KeyNotFoundException();
                }

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
        /// <returns></returns>
        public bool RemoveComponent<TComponent>()
            where TComponent : IComponent
        {
            var component = FindComponentInstance(typeof (TComponent));

            if (null == component)
            {
                return false;
            }

            if (components.Remove(typeof (TComponent)))
            {
                component.Remove();

                foreach (var observer in componentObservers.ToArray())
                {
                    observer.OnDetached(component);
                }

                return true;
            }

            return false;
        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        IDisposable IObservable<IComponentObserver>.Subscribe(IComponentObserver observer)
        {
            if (false == componentObservers.Contains(observer))
            {
                componentObservers.Add(observer);
            }

            return new DisposableToken<IComponentObserver>(observer, RemoveComponentObserver);
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

        private void RemoveComponentObserver(IComponentObserver observer)
        {
            componentObservers.Remove(observer);
        }
    }
}