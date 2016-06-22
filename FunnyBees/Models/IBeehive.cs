using System;
using System.Collections.Generic;
using Windows.Foundation;

namespace FunnyBees.Models
{
    public enum BeehiveAcion
    {
        BeeAdded,
        BeeRemoved
    }

    public sealed class BeehiveChangedEventArgs : EventArgs
    {
        public BeehiveAcion Acion
        {
            get;
        }

        public IBee Bee
        {
            get;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.EventArgs"/>.
        /// </summary>
        public BeehiveChangedEventArgs(BeehiveAcion acion, IBee bee)
        {
            Acion = acion;
            Bee = bee;
        }
    }

    public interface IBeehive : IUpdatable<UpdateContext>
    {
        IEnumerable<IBee> Bees
        {
            get;
        }

        int Number
        {
            get;
        }

        int MaximumNumberOfBees
        {
            get;
        }

        event TypedEventHandler<IBeehive, BeehiveChangedEventArgs> Changed; 

        void AddBee(IBee bee);

        void RemoveBee(IBee bee);
    }
}