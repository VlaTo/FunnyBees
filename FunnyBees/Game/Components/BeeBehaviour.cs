using FunnyBees.Engine;
using FunnyBees.Shapes;

namespace FunnyBees.Game.Components
{
    public interface IBeeActions
    {
        bool IsAppeared
        {
            get;
            set;
        }

        void Die();
    }

    public class BeeBehaviour : Component<Bee>, IBeeActions
    {
        private bool isAppeared;
        private CircleShape circle;

        public bool IsAppeared
        {
            get
            {
                return isAppeared;
            }
            set
            {
                if (value == isAppeared)
                {
                    return;
                }

                if (value)
                {
                    circle = new CircleShape();
                    Container.AddChild(circle);
                }
                else
                {
                    Container.RemoveChild(circle);
                }
            }
        }

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