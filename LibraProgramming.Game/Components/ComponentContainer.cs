using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using LibraProgramming.Game.Interactors;
using LibraProgramming.Windows;

namespace LibraProgramming.Game.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class ComponentContainer : Engine.IObservable<IComponentObserver>
    {
        private ImmutableDictionary<Type, IComponent> components;
        private readonly ICollection<IComponentObserver> observers;

        protected IEnumerable<IComponent> Components => components.Values;

        protected ComponentContainer()
        {
            components = ImmutableDictionary<Type, IComponent>.Empty;
            observers = new List<IComponentObserver>();
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

            components = components.Add(typeof(TComponent), component);

            DoAttachComponent(component);
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

            components = components.Add(typeof(TComponent), component);

            DoAttachComponent(component);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <returns></returns>
        public TComponent GetComponent<TComponent>(bool failIfNotExists = true)
            where TComponent : IComponent
        {
            if (false == components.TryGetValue(typeof (TComponent), out var component))
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
            var component = FindComponentInstance(typeof(TComponent));

            if (null == component)
            {
                return false;
            }

            components = components.Remove(typeof(TComponent));

            DoDetachComponent(component);

            return true;
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

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="observer"></param>
        /// <returns></returns>
        IDisposable Engine.IObservable<IComponentObserver>.Subscribe(IComponentObserver observer)
        {
            if (false == observers.Contains(observer))
            {
                observers.Add(observer);
            }

            return new DisposableToken<IComponentObserver>(observer, RemoveObserver);
        }

        private void DoAttachComponent(IComponent component)
        {
            var array = observers.ToArray();

            component.Attach(this);

            foreach (var observer in array)
            {
                observer.OnAttached(component);
            }
        }

        private void DoDetachComponent(IComponent component)
        {
            component.Detach();

            foreach (var observer in observers.ToArray())
            {
                observer.OnDetached(component);
            }
        }

        private bool IsComponentExists(Type targetType)
        {
            return components.ContainsKey(targetType);
        }

        private IComponent FindComponentInstance(Type targetType)
        {
            if (components.TryGetValue(targetType, out var component))
            {
                return component;
            }

            return null;
        }

        private void RemoveObserver(IComponentObserver observer)
        {
            observers.Remove(observer);
        }
    }
}