namespace FunnyBees.Models
{
    public interface IBee : IUpdatable<UpdateContext>
    {
        int Number
        {
            get;
        }

        IBeeBehaviour Behaviour
        {
            get;
        }

        IBeehive Beehive
        {
            get;
        }

        void Die();
    }
}