using System;

namespace FunnyBees.Models
{
    internal class BeeFactory : IBeeFactory
    {
        private readonly Random random;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public BeeFactory()
        {
            random = new Random();
        }

        public IBee CreateBee(int index)
        {
            var lifetime = random.Next(10, 15);
            return new Bee(index, lifetime);
        }
    }
}