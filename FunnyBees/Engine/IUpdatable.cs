using System;

namespace FunnyBees.Engine
{
    public interface IUpdatable
    {
        void Update(TimeSpan elapsedTime);
    }
}