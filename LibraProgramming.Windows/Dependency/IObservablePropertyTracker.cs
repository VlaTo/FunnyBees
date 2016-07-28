namespace LibraProgramming.Windows.Dependency
{
    public interface IObservablePropertyTracker
    {
        IObservableModelSubscription Subscribe(object model);
    }
}