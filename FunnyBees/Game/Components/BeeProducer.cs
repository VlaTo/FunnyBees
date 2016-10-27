using System;
using FunnyBees.Engine;

namespace FunnyBees.Game.Components
{
/*
    public class BeeProducer : ComponentObserver<Beehive>, IBeeLifetimeObserver, IComponentObserver<BeeManager>
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

            var component = Container.GetComponent<BeeManager>(failIfNotExists: false);

            SubscribeToBeesOwner(component);
        }

        protected override void OnDetach()
        {
            base.OnDetach();

            ReleaseExistingSubscription();
        }

        void IComponentObserver<BeeManager>.OnAttached(BeeManager component)
        {
            SubscribeToBeesOwner(component);
        }

        void IComponentObserver<BeeManager>.OnDetached(BeeManager component)
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

        private void SubscribeToBeesOwner(BeeManager component)
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
*/
}