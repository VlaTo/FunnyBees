namespace FunnyBees.Models
{
    public interface IBee : IUpdatable<UpdateContext>
    {
        int Lifetime
        {
            get;
        }

        Beehive Beehive
        {
            get;
            set;
        }
    }
}