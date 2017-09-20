using LibraProgramming.FunnyBees.Interop;
using LibraProgramming.FunnyBees.Services;

namespace LibraProgramming.FunnyBees.Models
{
    public interface IBee : IEntity, IUpdatable<ISessionContext>
    {
        IBeeBehaviour Behaviour
        {
            get;
        }

        int NativeBeehiveIndex
        {
            get;
        }

        int CurrentBeehiveIndex
        {
            get;
            set;
        }
    }
}