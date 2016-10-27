using FunnyBees.Engine;

namespace FunnyBees.Game.Components
{
    public class HomeBeehive : Component<Bee>
    {
        private Beehive beehive;

        public Beehive Beehive
        {
            get
            {
                return beehive;
            }
            set
            {
                if (value == beehive)
                {
                    return;
                }

                if (null != beehive)
                {
                    var owner = beehive.GetComponent<BeeManager>();
                    owner.RemoveBee(Container);
                }

                beehive = value;

                if (null != beehive)
                {
                    var owner = beehive.GetComponent<BeeManager>();
                    owner.AddBee(Container);
                }
            }
        }
    }
}