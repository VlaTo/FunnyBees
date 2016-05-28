using System.Collections.Generic;
using LibraProgramming.FunnyBees.Models;
using LibraProgramming.FunnyBees.Services;
using LibraProgramming.Windows.Infrastructure;

namespace FunnyBees.Services
{
    internal sealed class BeehiveBuilder : IBeehiveBuilder
    {
        private readonly int beehiveIndex;
        private readonly IList<IBee> bees;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public BeehiveBuilder(int beehiveIndex)
        {
            this.beehiveIndex = beehiveIndex;
            bees = new List<IBee>();
        }

        public IBeehiveBuilder AddBee(IBee bee)
        {
            bees.Add(bee);
            return this;
        }

        /// <summary>
        /// Constructs target object of type <see cref="TObject" />.
        /// </summary>
        /// <returns></returns>
        Beehive IObjectBuilder<Beehive>.Construct()
        {
            return new Beehive(bees);
        }
    }
}