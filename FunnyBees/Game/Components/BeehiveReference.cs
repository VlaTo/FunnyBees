using FunnyBees.Engine;

namespace FunnyBees.Game.Components
{
    public class BeehiveReference : Component<Bee>
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
                    var owner = beehive.GetComponent<BeesOwner>();
                    owner.RemoveBee(Container);
                }

                beehive = value;

                if (null != beehive)
                {
                    var owner = beehive.GetComponent<BeesOwner>();
                    owner.AddBee(Container);
                }
            }
        }
    }
}