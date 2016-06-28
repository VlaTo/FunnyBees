namespace FunnyBees.Engine
{
    public interface IComponentObserver
    {
        void OnComponentAttached(IComponent component);

        void OnComponentDetached(IComponent component);
    }
}