using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FunnyBees.Services;

namespace FunnyBees.Models
{
    public class BeehiveFactory : IBeehiveFactory
    {
        private readonly Random random;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public BeehiveFactory()
        {
            random = new Random();
        }

        public Beehive GetBeehive()
        {
            var number = random.Next(25, 50);
            return new Beehive(number);
        }
    }
}