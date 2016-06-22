namespace FunnyBees.Models
{
    public interface IBeeBehaviour
    {
        void Update(IBee bee, UpdateContext context);
    }
}