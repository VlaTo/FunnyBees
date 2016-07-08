using System;

namespace FunnyBees.Engine
{
    public interface ISceneObject
    {
        void Update(TimeSpan elapsedTime);
    }
}