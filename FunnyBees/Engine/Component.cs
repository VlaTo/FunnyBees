using System;

namespace FunnyBees.Engine
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

        public void Remove()
        {
            if (null == Container)
            {
                throw new InvalidOperationException();
            }

            Container = null;

            OnDetach();
        }

        protected virtual void OnAttach()
        {
        }

        protected virtual void OnDetach()
        {
        }
    }
}