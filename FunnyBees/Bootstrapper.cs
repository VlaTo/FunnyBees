using Windows.UI.Xaml;
using FunnyBees.Localization;
using FunnyBees.Services;
using FunnyBees.ViewModels;
using LibraProgramming.FunnyBees.Services;
using LibraProgramming.Windows.Locator;

namespace FunnyBees
{
    /// <summary>
    /// 
    /// </summary>
    internal static class Bootstrapper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="locator"></param>
        public static void RegisterServicesAsync(ServiceLocator locator)
        {
//            locator.Register<IEventMessenger, EventMessenger>(lifetime: InstanceLifetime.Singleton);
            locator.Register<IApplicationLocalization>(() => ApplicationLocalizationManager.Current,lifetime: InstanceLifetime.Singleton);
            locator.Register<IUIThreadDispatcher>(() => new UIThreadDispatcher(((App)Application.Current).Dispatcher),lifetime: InstanceLifetime.CreateNew);
            locator.Register<IBeeApiarOptionsProvider, BeeApiarOptionProvider>(lifetime: InstanceLifetime.Singleton);
            locator.Register<ITimeIntervalGenerator, TimeIntervalGenerator>(lifetime: InstanceLifetime.Singleton);
            locator.Register<ISessionController, SessionController>(lifetime: InstanceLifetime.Singleton);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locator"></param>
        public static void RegisterViewModels(ServiceLocator locator)
        {
            locator.Register<HostPageViewModel>(lifetime: InstanceLifetime.CreateNew);
            locator.Register<MainPageViewModel>(lifetime: InstanceLifetime.CreateNew);
            locator.Register<OptionsPageViewModel>(lifetime: InstanceLifetime.CreateNew);
        }
    }
}