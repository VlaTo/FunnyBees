using System.Collections.Generic;
using LibraProgramming.FunnyBees.Interop;

namespace LibraProgramming.FunnyBees.Models
{
    public class Beehive : IEntity, IUpdatable
    {
        private IList<IBee> bees;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public Beehive(IEnumerable<IBee> collection)
        {
            bees = new List<IBee>();

            foreach (var bee in collection)
            {
                bees.Add(bee);
            }
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}