namespace FunnyBees.Models
{
    public interface IUpdatable<in TContext>
    {
        void Update(TContext context);
    }
}