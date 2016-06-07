namespace FunnyBees.Models
{
    public class Bee : IBee
    {
        public int Lifetime
        {
            get;
            private set;
        }

        public Beehive Beehive
        {
            get;
            set;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public Bee(int lifetime)
        {
            Lifetime = lifetime;
        }

        public void Update(UpdateContext context)
        {
            if (Lifetime <= 0)
            {
                return;
            }

            if (--Lifetime == 0)
            {
//                Beehive
            }
        }
    }
}