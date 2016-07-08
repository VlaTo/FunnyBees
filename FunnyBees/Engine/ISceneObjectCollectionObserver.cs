namespace FunnyBees.Engine
{
    public enum SceneObjectCollectionChange
    {
        Inserted,
        Replaced,
        Removed
    }

    public interface ISceneObjectCollectionObserver : IObserver
    {
        void OnChildCollectionChanged(SceneObjectCollectionChange action, int index, SceneObject @object, SceneObject source);
    }
}