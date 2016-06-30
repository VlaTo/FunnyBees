using FunnyBees.Engine;

namespace FunnyBees.Game.Components
{
    public class GuardBee : Component
    {
        private readonly Beehive beehive;

        public GuardBee(Beehive beehive)
        {
            this.beehive = beehive;
        }
    }
}