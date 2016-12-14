using System;
using LibraProgramming.Windows.StateMachine;

namespace FunnyBees.Game.Components
{
    public class QueenBeeBehaviour : BeeBehaviour
    {
        private static readonly TimeSpan Period = TimeSpan.FromSeconds(1.0d);

        private readonly StateMachine<BehaviourStates, BehaviourActions> states;
        private int count;
        private TimeSpan lastChecked;

        public QueenBeeBehaviour()
        {
            lastChecked = TimeSpan.Zero;
            states = new StateMachine<BehaviourStates, BehaviourActions>(BehaviourStates.Idle);

            states
                .Configure(BehaviourStates.Idle)
                .OnEnter(OnIdle)
                .Permit(BehaviourActions.Update, BehaviourStates.Tick);

            states
                .Configure(BehaviourStates.Tick)
                .OnEnter(OnTick)
                .Permit(BehaviourActions.Update, BehaviourStates.Toe)
                .Permit(BehaviourActions.Die, BehaviourStates.Dead);

            states
                .Configure(BehaviourStates.Toe)
                .OnEnter(OnToe)
                .Permit(BehaviourActions.Update, BehaviourStates.Tick)
                .Permit(BehaviourActions.Die, BehaviourStates.Dead);

            states
                .Configure(BehaviourStates.Dead)
                .OnEnter(OnDie)
                .Ignore(BehaviourActions.Update)
                .Ignore(BehaviourActions.Die);
        }

        public override void Update(TimeSpan elapsed)
        {
            states.Fire(BehaviourActions.Update);

            /*if (TimeSpan.Zero == lastChecked)
            {
                lastChecked = elapsed;
            }
            else if (Period < (elapsed - lastChecked))
            {
                var beehive = Container.Beehive;

                foreach (var bee in beehive.GetComponent<BeeManager>().Bees)
                {
                    var worker = bee.GetComponent<WorkingBeeBehaviour>(failIfNotExists: false);

                    if (null == worker)
                    {
                        continue;
                    }

                    worker.Search();
                }
                
                lastChecked = elapsed;
            }*/
        }

        public override void Die()
        {
            states.Fire(BehaviourActions.Die);
        }

        private void OnDie(BehaviourStates obj)
        {
        }

        private void OnIdle(BehaviourStates obj)
        {
        }

        private void OnTick(BehaviourStates obj)
        {
        }

        private void OnToe(BehaviourStates obj)
        {
            if (count++ > 100)
            {
                var beehive = Container.Beehive;

                foreach (var bee in beehive.GetComponent<BeeManager>().Bees)
                {
                    var worker = bee.GetComponent<WorkingBeeBehaviour>(failIfNotExists: false);

                    if (null == worker)
                    {
                        continue;
                    }

                    worker.Search();
                }

                count = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private enum BehaviourStates
        {
            Idle,
            Tick,
            Toe,
            Dead
        }

        /// <summary>
        /// 
        /// </summary>
        private enum BehaviourActions
        {
            Update,
            Die
        }
    }
}