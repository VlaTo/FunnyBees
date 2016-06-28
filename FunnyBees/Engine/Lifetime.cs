using System;

namespace FunnyBees.Engine
{
    public class Lifetime : Component
    {
        private TimeSpan elapsed;

        protected override void OnAttach()
        {
            elapsed = TimeSpan.Zero;
        }
    }
}