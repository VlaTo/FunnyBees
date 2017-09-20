using System;

namespace LibraProgramming.Game.Components
{
    /// <summary>
    /// 
    /// </summary>
    public interface IComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        void Attach(ComponentContainer container);

        /// <summary>
        /// 
        /// </summary>
        void Detach();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elapsed"></param>
        void Update(TimeSpan elapsed);
    }
}