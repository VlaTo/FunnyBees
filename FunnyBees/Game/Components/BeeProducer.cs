using System;
using FunnyBees.Engine;

namespace FunnyBees.Game.Components
{
    public class BeeProducer : ComponentObserver<Beehive>, IBeeLifetimeObserver, IComponentObserver<BeesOwner>
    {
        private readonly int maximumBeesCount;
        private IDisposable subscription;

        public BeeProducer(int maximumBeesCount)
        {
            this.maximumBeesCount = maximumBeesCount;
        }

        protected override void OnAttach()
        {
            base.OnAttach();

            var component = Container.GetComponent<BeesOwner>(failIfNotExists: false);

            SubscribeToBeesOwner(component);
        }

        protected override void OnDetach()
        {
            base.OnDetach();

            ReleaseExistingSubscription();
        }

        void IComponentObserver<BeesOwner>.OnAttached(BeesOwner component)
        {
            SubscribeToBeesOwner(component);
        }

        void IComponentObserver<BeesOwner>.OnDetached(BeesOwner component)
        {
            ReleaseExistingSubscription();
        }

        void IBeeLifetimeObserver.OnAdded(Bee bee)
        {
            //throw new System.NotImplementedException();
        }

        void IBeeLifetimeObserver.OnRemoved(Bee bee)
        {
            //throw new System.NotImplementedException();
        }

        private void SubscribeToBeesOwner(BeesOwner component)
        {
            ReleaseExistingSubscription();
            subscription = ((Engine.IObservable<IBeeLifetimeObserver>) component).Subscribe(this);
        }

        private void ReleaseExistingSubscription()
        {
            if (null == subscription)
            {
                return;
            }

            subscription.Dispose();
            subscription = null;
        }
    }
}