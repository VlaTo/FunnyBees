using System;
using System.Linq;

namespace FunnyBees.Models
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BeeBehaviour : IBeeBehaviour
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        protected BeeBehaviour()
        {
        }

        public abstract void Update(IBee bee, UpdateContext context);
    }

    /// <summary>
    /// 
    /// </summary>
    public class QueenBeeBehaviour : BeeBehaviour
    {
        private readonly Random random;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public QueenBeeBehaviour()
        {
            random = new Random();
        }

        public override void Update(IBee bee, UpdateContext context)
        {
            var beehive = bee.Beehive;
            var currentBeesCount = beehive.Bees.Count();

            if (beehive.MaximumNumberOfBees > currentBeesCount)
            {
                var choice = random.NextDouble();

                if (choice < 0.9d)
                {
                    return;
                }

                var newbee = new Bee(beehive, currentBeesCount + 1, new WorkingBeeBehaviour(random.Next(100, 500)));

                beehive.AddBee(newbee);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class WorkingBeeBehaviour : BeeBehaviour
    {
        private readonly int lifetime;
        private int age;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public WorkingBeeBehaviour(int lifetime)
        {
            this.lifetime = lifetime;
        }

        public override void Update(IBee bee, UpdateContext context)
        {
            if (0 == (lifetime - age))
            {
                bee.Die();
                return;
            }

            age++;
        }
    }
}