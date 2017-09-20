using System;
using LibraProgramming.Game.Components;

namespace LibraProgramming.Game.Interactors
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    public class Interaction : IInteraction
    {
        private readonly ComponentContainer source;
        private readonly ComponentContainer target;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public Interaction(ComponentContainer source, ComponentContainer target)
        {
            this.source = source;
            this.target = target;
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <typeparam name="TInteractor"></typeparam>
        public void Using<TInteractor>()
            where TInteractor : IInteractor, new ()
        {
            Using(new TInteractor());
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="interactor"></param>
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