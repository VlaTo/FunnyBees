using System;

namespace FunnyBees.Engine
{
    public class Component : IComponent
    {
        protected ComponentContainer Container
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

            Container = container;

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