using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Foundation;
using LibraProgramming.Windows.Infrastructure;

namespace FunnyBees.Models
{
    public class Beehive : IBeehive
    {
        private readonly ICollection<IBee> bees;
        private readonly TypedWeakEventHandler<IBeehive, BeehiveChangedEventArgs> changed;

        public IEnumerable<IBee> Bees => bees;

        public int Number
        {
            get;
        }

        public int MaximumNumberOfBees
        {
            get;
        }

        public event TypedEventHandler<IBeehive, BeehiveChangedEventArgs> Changed
        {
            add
            {
                changed.AddHandler(value);
            }
            remove
            {
                changed.RemoveHandler(value);
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public Beehive(int number, int maximumNumberOfBees)
        {
            Number = number;
            MaximumNumberOfBees = maximumNumberOfBees;
            bees = new Collection<IBee>();
            changed = new TypedWeakEventHandler<IBeehive, BeehiveChangedEventArgs>();
        }

        public void AddBee(IBee bee)
        {
            bees.Add(bee);
            changed.Invoke(this, new BeehiveChangedEventArgs(BeehiveAcion.BeeAdded, bee));
        }

        public void RemoveBee(IBee bee)
        {
            bees.Remove(bee);
            changed.Invoke(this, new BeehiveChangedEventArgs(BeehiveAcion.BeeRemoved, bee));
        }

        void IUpdatable<UpdateContext>.Update(UpdateContext context)
        {
            var temp = Bees.ToArray();

            foreach (var bee in temp)
            {
                bee.Update(context);
            }
        }
    }
}