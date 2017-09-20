namespace LibraProgramming.Game.Interactors
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInteraction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInteractor"></typeparam>
        void Using<TInteractor>() where TInteractor : IInteractor, new ();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="interactor"></param>
        void Using(IInteractor interactor);
    }
}