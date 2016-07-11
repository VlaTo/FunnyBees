using System;
using FunnyBees.Game;
using FunnyBees.Game.Components;
using FunnyBees.Game.Interactors;

namespace FunnyBees.Engine
{
    public class BeeLifetime : Component<Bee>, IUpdatable
    {
        public TimeSpan Lifetime
        {
            get;
        }

        public TimeSpan Age
        {
            get;
            private set;
        }

        public BeeLifetime(TimeSpan lifetime)
        {
            Lifetime = lifetime;
        }

        protected override void OnAttach()
        {
            Age = TimeSpan.Zero;
        }

        void IUpdatable.Update(TimeSpan elapsedTime)
        {
            Age = elapsedTime;

            if (Age >= Lifetime)
            {
                var home = Container.GetComponent<HomeBeehive>();
                Container.InteractWith(home.Beehive).Using<BeeRemover>();
            }
        }
    }
}