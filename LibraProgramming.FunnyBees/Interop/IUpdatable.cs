using LibraProgramming.FunnyBees.Models;

namespace LibraProgramming.FunnyBees.Interop
{
    public interface IUpdatable<in TContext>
        where TContext : IContext
    {
        void Update(TContext context);
    }
}