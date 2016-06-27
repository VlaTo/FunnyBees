namespace FunnyBees.Engine
{
    public interface IInteraction
    {
        void Using<TInteractor>() where TInteractor : IInteractor, new ();

        void Using(IInteractor interactor);
    }
}