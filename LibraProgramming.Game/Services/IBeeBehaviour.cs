using LibraProgramming.FunnyBees.Models;

namespace LibraProgramming.FunnyBees.Services
{
    public interface IBeeBehaviour
    {
        void Update(IBee bee, ISessionContext context);
    }
}