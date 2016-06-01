using LibraProgramming.FunnyBees.Services;

namespace LibraProgramming.FunnyBees.Models
{
    public sealed class Bee : Entity, IBee
    {
        public IBeeBehaviour Behaviour
        {
            get;
        }

        public int NativeBeehiveIndex
        {
            get;
        }

        public int CurrentBeehiveIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public Bee(int beehiveIndex, IBeeBehaviour behaviour)
        {
            NativeBeehiveIndex = beehiveIndex;
            Behaviour = behaviour;
        }

        public void Update(ISessionContext context)
        {
            if (null == Behaviour)
            {
                return;
            }

            Behaviour.Update(this, context);
        }
    }
}