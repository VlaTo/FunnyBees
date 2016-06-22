namespace FunnyBees.Models
{
    public class Bee : IBee
    {
        public IBeeBehaviour Behaviour
        {
            get;
        }

        public IBeehive Beehive
        {
            get;
        }

        public int Number
        {
            get;
            set;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public Bee(IBeehive beehive, int number, IBeeBehaviour behaviour)
        {
            Beehive = beehive;
            Number = number;
            Behaviour = behaviour;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Update(UpdateContext context)
        {
            Behaviour.Update(this, context);
        }

        public void Die()
        {
            Beehive.RemoveBee(this);
        }
    }
}