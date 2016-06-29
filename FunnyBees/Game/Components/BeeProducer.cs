using System;
using FunnyBees.Engine;

namespace FunnyBees.Game.Components
{
    public class BeeProducer : ComponentObserver, IBeeLifetimeObserver, IComponentObserver<BeesOwner>
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

            if (null != component)
            {
                subscription = ((Engine.IObservable<IBeeLifetimeObserver>) component).Subscribe(this);
            }
        }

        protected override void OnDetach()
        {
            base.OnDetach();

            if (null != subscription)
            {
                subscription.Dispose();
                subscription = null;
            }
        }

        void IComponentObserver<BeesOwner>.OnAttached(BeesOwner component)
        {
            if (null != subscription)
            {
                subscription.Dispose();
                subscription = null;
            }

            subscription = ((Engine.IObservable<IBeeLifetimeObserver>)component).Subscribe(this);
        }

        void IComponentObserver<BeesOwner>.OnDetached(BeesOwner component)
        {
            if (null != subscription)
            {
                subscription.Dispose();
                subscription = null;
            }
        }

        void IBeeLifetimeObserver.OnAdded(Bee bee)
        {
            //throw new System.NotImplementedException();
        }

        void IBeeLifetimeObserver.OnRemoved(Bee bee)
        {
            //throw new System.NotImplementedException();
        }
    }
}