namespace FunnyBees.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInteractor
    {
        void Interact(ComponentContainer source, ComponentContainer target);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    public interface IInteractor<in TComponent, in TTarget>
        where TComponent : ComponentContainer
        where TTarget : ComponentContainer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="component"></param>
        /// <param name="target"></param>
        void Interact(TComponent component, TTarget target);
    }
}