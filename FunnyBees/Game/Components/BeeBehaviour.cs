using System;
using FunnyBees.Engine;
using FunnyBees.Shapes;
using LibraProgramming.Windows.StateMachine;

namespace FunnyBees.Game.Components
{
    public interface IBeeActions
    {
        void Die();

        void Collect();
    }

    public class BeeBehaviour : Component<Bee>, IBeeActions
    {
        private readonly StateMachine<BeeStates, BeeActions> states;
        private CircleShape circle;
        private TimeSpan currentStateTime;

        public Beehive Beehive
        {
            get;
            set;
        }

        public BeeBehaviour()
        {
            states = new StateMachine<BeeStates, BeeActions>(BeeStates.Idle);

            states
                .Configure(BeeStates.Idle)
                .Permit(BeeActions.Search, BeeStates.Searching).OnEnter(DoSearchingBegin);
            states
                .Configure(BeeStates.Searching)
                .Permit(BeeActions.Collect, BeeStates.Collecting).OnEnter(DoCollecting)
                .Permit(BeeActions.Die, BeeStates.Died).OnEnter(DoDie);
            states
                .Configure(BeeStates.Collecting)
                .Permit(BeeActions.Return, BeeStates.Returning).OnEnter(DoReturning)
                .Permit(BeeActions.Die, BeeStates.Died).OnEnter(DoDie);
            states
                .Configure(BeeStates.Returning)
                .Permit(BeeActions.Idle, BeeStates.Idle).OnEnter(DoIdle);
            states
                .Configure(BeeStates.Died)
                .Ignore(BeeActions.Die)
                .Ignore(BeeActions.Idle)
                .Ignore(BeeActions.Return)
                .Ignore(BeeActions.Collect)
                .Ignore(BeeActions.Search);
        }

        public void Collect()
        {
            states.Fire(BeeActions.Collect);
        }

        public void Die()
        {
            states.Fire(BeeActions.Die);
        }

        public override void Update(TimeSpan elapsed)
        {
            switch (states.CurrentState)
            {
                case BeeStates.Collecting:
                {
                    var duration = elapsed - currentStateTime;

                    if (TimeSpan.FromSeconds(10.0d) < duration)
                    {
                        states.Fire(BeeActions.Return);
                    }

                    break;
                }

                case BeeStates.Returning:
                {
                    var duration = elapsed - currentStateTime;

                    if (TimeSpan.FromSeconds(1.0d) < duration)
                    {
                        states.Fire(BeeActions.Idle);
                    }


                    break;
                }
            }
        }

        private void DoIdle(BeeStates obj)
        {
            Container.RemoveChild(circle);
            circle = null;
        }

        private void DoDie(BeeStates obj)
        {
            var parent = Container.Parent;

            Beehive.GetComponent<BeeManager>().RemoveBee(Container);
            parent.RemoveChild(Container);
        }

        private void DoSearchingBegin(BeeStates obj)
        {
            circle = new CircleShape();
            Container.AddChild(circle);
        }

        private void DoCollecting(BeeStates obj)
        {
            currentStateTime = Scene.Current.ElapsedTime;
        }

        private void DoReturning(BeeStates obj)
        {
            currentStateTime = Scene.Current.ElapsedTime;
        }

        /// <summary>
        /// 
        /// </summary>
        private enum BeeStates
        {
            Died = -1,
            Idle,
            Searching,
            Collecting,
            Returning
        }

        /// <summary>
        /// 
        /// </summary>
        private enum BeeActions
        {
            Idle,
            Search,
            Collect,
            Return,
            Die
        }
    }
}