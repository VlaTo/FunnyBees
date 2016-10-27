using System;
using FunnyBees.Engine;

namespace FunnyBees.Game.Components
{
    public class QueenBee : Component<Bee>
    {
        private static TimeSpan Period = TimeSpan.FromMilliseconds(1000.0d);

        private readonly Beehive beehive;
        private TimeSpan lastChecked;

        public QueenBee(Beehive beehive)
        {
            this.beehive = beehive;
            lastChecked = TimeSpan.MinValue;
        }

        public override void Update(TimeSpan elapsed)
        {
        }
    }
}