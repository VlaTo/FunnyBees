using LibraProgramming.Windows.Locator;

namespace FunnyBees.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// 
        /// </summary>
        public HostPageViewModel HostPageViewModel => ServiceLocator.Current.GetInstance<HostPageViewModel>();

        /// <summary>
        /// 
        /// </summary>
        public OptionsPageViewModel OptionsPageViewModel => ServiceLocator.Current.GetInstance<OptionsPageViewModel>();
    }
}