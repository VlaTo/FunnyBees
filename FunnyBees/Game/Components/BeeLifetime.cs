using System;
using FunnyBees.Engine;

namespace FunnyBees.Game.Components
{
    public class BeeLifetime : Component<Bee>
    {
        private readonly TimeSpan lifespan;
        private TimeSpan birth;

        public BeeLifetime(TimeSpan lifespan)
        {
            this.lifespan = lifespan;
            birth = TimeSpan.Zero;
        }

        public override void Update(TimeSpan elapsed)
        {
            if (TimeSpan.Zero == birth)
            {
                birth = elapsed;
            }
            else if ((elapsed - birth) > lifespan)
            {
                Container.GetComponent<BeeBehaviour>().Die();
            }
        }

        public void Die()
        {
            Container.GetComponent<BeeBehaviour>().Die();
        }
    }
}