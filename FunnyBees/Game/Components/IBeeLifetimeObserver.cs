using FunnyBees.Engine;

namespace FunnyBees.Game.Components
{
    public interface IBeeLifetimeObserver : IObserver
    {
        void OnAdded(Bee bee);

        void OnRemoved(Bee bee);
    }
}