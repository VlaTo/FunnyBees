using System;
using FunnyBees.Shapes;
using LibraProgramming.Game.Engine;
using LibraProgramming.Windows.StateMachine;

namespace FunnyBees.Game.Components
{
    public class HoneyBeeBehaviour : BeeBehaviour
    {
        private readonly TimeSpan lifespan;
        private readonly StateMachine<BeeStates, BeeActions> states;
        private TimeSpan birth;
        private CircleShape circle;
        private TimeSpan currentStateTime;

        public HoneyBeeBehaviour(TimeSpan lifespan)
        {
            this.lifespan = lifespan;

            states = new StateMachine<BeeStates, BeeActions>(BeeStates.Idle);
            birth = TimeSpan.Zero;

            states
                .Configure(BeeStates.Idle)
                .OnEnter(DoIdle)
                .Permit(BeeActions.Search, BeeStates.Searching)
                .Permit(BeeActions.Die, BeeStates.Died);
            states
                .Configure(BeeStates.Searching)
                .OnEnter(DoSearchingBegin)
                .Permit(BeeActions.Collect, BeeStates.Collecting)
                .Permit(BeeActions.Die, BeeStates.Died)
                .Ignore(BeeActions.Search);
            states
                .Configure(BeeStates.Collecting)
                .OnEnter(DoCollecting)
                .Permit(BeeActions.Return, BeeStates.Returning)
                .Permit(BeeActions.Die, BeeStates.Died)
                .Ignore(BeeActions.Search);
            states
                .Configure(BeeStates.Returning)
                .OnEnter(DoReturning)
                .Permit(BeeActions.Idle, BeeStates.Idle)
                .Ignore(BeeActions.Search);
            states
                .Configure(BeeStates.Died)
                .OnEnter(DoDie)
                .Ignore(BeeActions.Die)
                .Ignore(BeeActions.Idle)
                .Ignore(BeeActions.Return)
                .Ignore(BeeActions.Collect)
                .Ignore(BeeActions.Search);
        }

        public void Search()
        {
            states.Fire(BeeActions.Search);
        }

        public override void Die()
        {
            states.Fire(BeeActions.Die);
        }

        public override void Update(TimeSpan elapsed)
        {
            if (TimeSpan.Zero == birth)
            {
                birth = elapsed;
            }
            else if ((elapsed - birth) > lifespan)
            {
                Die();
                return;
            }

            switch (states.CurrentState)
            {
                case BeeStates.Collecting:
                    {
                        var duration = elapsed - currentStateTime;

                        if (TimeSpan.FromSeconds(1.0d) < duration)
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
            var scene = Container.Parent;
            var beehive = Container.Beehive;

            beehive.GetComponent<BeeManager>().RemoveBee(Container);
            scene.RemoveChild(Container);
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