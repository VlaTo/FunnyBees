using System;

namespace FunnyBees.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISceneUpdatable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="elapsedTime"></param>
        void Update(TimeSpan elapsedTime);
    }
}