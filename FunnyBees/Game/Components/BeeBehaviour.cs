using System.Diagnostics;
using FunnyBees.Engine;

namespace FunnyBees.Game.Components
{
    public class BeeBehaviour : Component<Bee>
    {
        public Beehive Beehive
        {
            get;
            set;
        }

        public void Die()
        {
            var parent = Container.Parent;

            Beehive.GetComponent<BeeManager>().RemoveBee(Container);
            parent.RemoveChild(Container);
        }
    }
}