using System;

namespace FunnyBees.Engine
{
    public class Lifetime : Component
    {
        private readonly TimeSpan lifetime;
        private TimeSpan elapsed;

        public Lifetime(TimeSpan lifetime)
        {
            this.lifetime = lifetime;
        }

        protected override void OnAttach()
        {
            elapsed = TimeSpan.Zero;
        }
    }
}