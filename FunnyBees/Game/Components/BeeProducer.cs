using System.Linq;
using FunnyBees.Engine;

namespace FunnyBees.Game.Components
{
    public class BeeProducer : ComponentObserver, IBeeLifetimeObserver, IComponentObserver<BeesOwner>
    {
        private readonly int maximumBeesCount;

        public BeeProducer(int maximumBeesCount)
        {
            this.maximumBeesCount = maximumBeesCount;
        }

        void IComponentObserver<BeesOwner>.OnAttach(BeesOwner component)
        {
            var count = component.Bees.Count();

            if (count < maximumBeesCount)
            {
                ;
            }
        }

        void IComponentObserver<BeesOwner>.OnDetach(BeesOwner component)
        {
            throw new System.NotImplementedException();
        }

        void IBeeLifetimeObserver.OnAdded(Bee bee)
        {
            throw new System.NotImplementedException();
        }

        void IBeeLifetimeObserver.OnRemoved(Bee bee)
        {
            throw new System.NotImplementedException();
        }
    }
}