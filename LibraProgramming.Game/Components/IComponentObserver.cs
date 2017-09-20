using LibraProgramming.Game.Engine;

namespace LibraProgramming.Game.Components
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    public interface IComponentObserver : IObserver
    {
        void OnAttached(IComponent component);

        void OnDetached(IComponent component);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    public interface IComponentObserver<in TComponent>
        where TComponent : IComponent
    {
        void OnAttached(TComponent component);

        void OnDetached(TComponent component);
    }
}