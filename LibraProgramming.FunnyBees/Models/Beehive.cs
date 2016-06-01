using System.Collections.Generic;
using LibraProgramming.FunnyBees.Interop;

namespace LibraProgramming.FunnyBees.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Beehive : Entity, IUpdatable<ISessionContext>
    {
        /// <summary>
        /// 
        /// </summary>
        public IList<IBee> Bees
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Index
        {
            get;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public Beehive(int index, IEnumerable<IBee> collection)
        {
            Bees = new List<IBee>();
            Index = index;

            foreach (var bee in collection)
            {
                Bees.Add(bee);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Update(ISessionContext context)
        {
            foreach (var bee in Bees)
            {
                bee.Update(context);
            }
        }
    }
}