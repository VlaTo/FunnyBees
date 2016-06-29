using System;

namespace FunnyBees.Engine
{
    public abstract class ComponentObserver : Component, IComponentObserver
    {
        private IDisposable subscription;

        protected override void OnAttach()
        {
            subscription = ((IComponentObservable) Container).Subscribe(this);
        }

        protected override void OnDetach()
        {
            subscription.Dispose();
        }

        protected abstract void OnComponentAdded(IComponent component);

        protected abstract void OnComponentRemoved(IComponent component);

        void IComponentObserver.OnComponentAttached(IComponent component)
        {
            OnComponentAdded(component);
        }

        void IComponentObserver.OnComponentDetached(IComponent component)
        {
            OnComponentRemoved(component);
        }
    }
}