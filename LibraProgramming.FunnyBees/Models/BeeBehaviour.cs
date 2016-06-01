using Windows.UI.Composition;
using LibraProgramming.FunnyBees.Services;

namespace LibraProgramming.FunnyBees.Models
{
    public abstract class BeeBehaviour : IBeeBehaviour
    {
        public abstract void Update(IBee bee, ISessionContext context);
    }
}