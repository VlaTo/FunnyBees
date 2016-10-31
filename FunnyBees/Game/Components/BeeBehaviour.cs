using FunnyBees.Engine;
using FunnyBees.Shapes;
using LibraProgramming.Windows.StateMachine;

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
        private StateMachine<BeeStates, BeeActions> states;
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

        public BeeBehaviour()
        {
            states = new StateMachine<BeeStates, BeeActions>(BeeStates.Imago);

            states
                .Configure(BeeStates.Imago)
                .Permit(BeeActions.Flyout, BeeStates.Working)
                .OnEnter(DoWorkingBegin);
            states
                .Configure(BeeStates.Working)
                .Permit(BeeActions.Flyin, )
        }

        public void Die()
        {
            var parent = Container.Parent;

            Beehive.GetComponent<BeeManager>().RemoveBee(Container);
            parent.RemoveChild(Container);
        }

        private void DoWorkingBegin(BeeStates obj)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        private enum BeeStates
        {
            Died = -1,
            Idle,

            Working,
        }

        /// <summary>
        /// 
        /// </summary>
        private enum BeeActions
        {
            Flyout,
            Flyin,
            Die
        }
    }
}