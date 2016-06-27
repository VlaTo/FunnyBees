namespace FunnyBees.Engine
{
    public interface IComponent
    {
        void Attach(ComponentContainer container);

        void Remove();
    }
}