using LibraProgramming.FunnyBees.Interop;

namespace LibraProgramming.FunnyBees.Models
{
    public interface IBee : IEntity, IUpdatable
    {
        Beehive Beehive
        {
            get;
            set;
        }
    }
}