using System;

namespace FunnyBees.Engine
{
    public class Scene : DrawableObject
    {
        public static Scene Current
        {
            get;
            set;
        }

        public TimeSpan ElapsedTime
        {
            get;
            private set;
        }

        public override void Update(TimeSpan elapsedTime)
        {
            ElapsedTime = elapsedTime;
            base.Update(elapsedTime);
        }
    }
}