using System;

namespace FunnyBees.Engine
{
    public class Component : IComponent, IComponentObserver
    {
        private IDisposable subscription;

        protected ComponentContainer Container
        {
            get;
            private set;
        }

        public void Attach(ComponentContainer container)
        {
            if (null != Container)
            {
                throw new InvalidOperationException();
            }

            Container = container;

            subscription = ((IComponentObservable) container).Subscribe(this);

            OnAttach();
        }

        public void Remove()
        {
            if (null == Container)
            {
                throw new InvalidOperationException();
            }

            subscription.Dispose();

            Container = null;

            OnDetach();
        }

        void IComponentObserver.OnComponentAttached(IComponent component)
        {
            OnComponentAdded(component);
        }

        void IComponentObserver.OnComponentDetached(IComponent component)
        {
            OnComponentRemoved(component);
        }

        protected virtual void OnAttach()
        {
        }

        protected virtual void OnDetach()
        {
        }

        protected virtual void OnComponentAdded(IComponent component)
        {
        }

        protected virtual void OnComponentRemoved(IComponent component)
        {
        }
    }
}