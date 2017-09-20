using System;

namespace LibraProgramming.Game.Engine
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    public class Scene : DrawableObject
    {
        /// <summary>
        /// 
        /// </summary>
        public static Scene Current
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan ElapsedTime
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elapsedTime"></param>
        public override void Update(TimeSpan elapsedTime)
        {
            ElapsedTime = elapsedTime;
            base.Update(elapsedTime);
        }
    }
}