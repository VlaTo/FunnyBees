using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using FunnyBees.Engine;

namespace FunnyBees.Game.Components
{
    public class BeeManager : Component<Beehive>
    {
        private ImmutableList<Bee> bees;

        public IReadOnlyCollection<Bee> Bees => new ReadOnlyCollection<Bee>(bees);

        public int Capacity
        {
            get;
        }

        public BeeManager(int capacity)
        {
            Capacity = capacity;
            bees = ImmutableList<Bee>.Empty;
        }

        public bool AddBee(Bee bee)
        {
            if (null == bee)
            {
                throw new ArgumentNullException(nameof(bee));
            }

            if (Capacity == bees.Count)
            {
                return false;
            }

            bees = bees.Add(bee);

            bee.GetComponent<BeeBehaviour>().Beehive = Container;

            DoBeeAdded(bee);

            return true;
        }

        public void RemoveBee(Bee bee)
        {
            if (null == bee)
            {
                throw new ArgumentNullException(nameof(bee));
            }

            var behaviour = bee.GetComponent<BeeBehaviour>();

            if (Object.Equals(behaviour.Beehive, Container))
            {
                bees = bees.Remove(bee);
                behaviour.Beehive = null;

                DoBeeRemoved(bee);
            }
        }

        protected virtual void DoBeeAdded(Bee bee)
        {
        }

        protected virtual void DoBeeRemoved(Bee bee)
        {
        }
    }
}