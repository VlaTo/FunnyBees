using LibraProgramming.FunnyBees.Models;
using LibraProgramming.FunnyBees.Services;

namespace FunnyBees.Services
{
    internal class BeeBuilder : IBeeBuilder
    {
        private readonly int beehiveIndex;
        private IBeeBehaviour behaviour;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public BeeBuilder(int beehiveIndex)
        {
            this.beehiveIndex = beehiveIndex;
        }

        /// <summary>
        /// Constructs target object of type <see cref="TObject" />.
        /// </summary>
        /// <returns></returns>
        public IBee Construct()
        {
            return new Bee(beehiveIndex, behaviour);
        }

        public IBeeBuilder SetBehaviour<TBehaviour>() where TBehaviour : IBeeBehaviour, new ()
        {
            behaviour = new TBehaviour();
            return this;
        }
    }
}