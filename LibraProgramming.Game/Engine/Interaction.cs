using System;

namespace FunnyBees.Engine
{
    public class Interaction : IInteraction
    {
        private readonly ComponentContainer source;
        private readonly ComponentContainer target;

        public Interaction(ComponentContainer source, ComponentContainer target)
        {
            this.source = source;
            this.target = target;
        }

        public void Using<TInteractor>()
            where TInteractor : IInteractor, new ()
        {
            Using(new TInteractor());
        }

        public void Using(IInteractor interactor)
        {
            if (null == interactor)
            {
                throw new ArgumentNullException(nameof(interactor));
            }

            interactor.Interact(source, target);

        }
    }
}