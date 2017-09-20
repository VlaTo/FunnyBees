using LibraProgramming.FunnyBees.Models;
using LibraProgramming.Windows.Infrastructure;

namespace LibraProgramming.FunnyBees.Services
{
    public interface IBeeBuilder : IObjectBuilder<IBee>
    {
        IBeeBuilder SetBehaviour<TBehaviour>() where TBehaviour : IBeeBehaviour, new ();
    }
}