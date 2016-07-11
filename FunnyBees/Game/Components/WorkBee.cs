using FunnyBees.Engine;

namespace FunnyBees.Game.Components
{
    public class WorkBee : Component<Bee>
    {
        private readonly Beehive beehive;

        public WorkBee(Beehive beehive)
        {
            this.beehive = beehive;
        }
    }
}