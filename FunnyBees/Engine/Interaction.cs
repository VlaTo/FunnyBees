using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FunnyBees.Engine
{
    public class Interaction : IInteraction
    {
        private readonly ComponentContainer left;
        private readonly ComponentContainer right;

        public Interaction(ComponentContainer left, ComponentContainer right)
        {
            this.left = left;
            this.right = right;
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

            interactor.Interact(left, right);

            /*var type = typeof (IInteractor).MakeGenericType(left.GetType(), right.GetType());

            if (type.IsInstanceOfType(interactor))
            {
                var methods = interactor.GetType().GetRuntimeMethods();

                foreach (var method in methods.Where(info=>String.Equals("Interact", info.Name)))
                {
                }
            }*/
        }
    }
}