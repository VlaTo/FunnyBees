using System;

namespace FunnyBees.Engine
{
    public interface IComponent
    {
        void Attach(ComponentContainer container);

        void Detach();

        void Update(TimeSpan elapsed);
    }
}