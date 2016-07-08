using FunnyBees.Engine;

namespace FunnyBees.Game.Components
{
    public class QueenBee : Component<Bee>
    {
        private readonly Beehive beehive;

        public QueenBee(Beehive beehive)
        {
            this.beehive = beehive;
        }


    }
}