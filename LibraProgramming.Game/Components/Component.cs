using System;

namespace LibraProgramming.Game.Components
{
    public class Component<TContainer> : IComponent
        where TContainer : ComponentContainer
    {
        protected TContainer Container
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

            Container = (TContainer) container;

            OnAttach();
        }

        public void Detach()
        {
            if (null == Container)
            {
                throw new InvalidOperationException();
            }

            Container = null;

            OnDetach();
        }

        public virtual void Update(TimeSpan elapsed)
        {
        }

        protected virtual void OnAttach()
        {
        }

        protected virtual void OnDetach()
        {
        }
    }
}