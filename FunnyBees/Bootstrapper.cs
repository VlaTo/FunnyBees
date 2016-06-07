using Windows.UI.Xaml;
using FunnyBees.Localization;
using FunnyBees.Models;
using FunnyBees.Services;
using FunnyBees.ViewModels;
using LibraProgramming.Windows.Locator;
using LibraProgramming.Windows.Messaging;

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
        /// <param name="registry"></param>
        public static void RegisterServicesAsync(IServiceRegistry registry)
        {
            registry.Register<IEventMessenger, EventMessenger>(lifetime: InstanceLifetime.Singleton);
            registry.Register<IApplicationLocalization>(() => ApplicationLocalizationManager.Current, lifetime: InstanceLifetime.Singleton);
            registry.Register<IDispatcherProvider>(() => (App) Application.Current, lifetime: InstanceLifetime.Singleton);
            registry.Register<IApplicationOptionsProvider, ApplicationOptionProvider>(lifetime: InstanceLifetime.Singleton);
//            registry.Register<ITimeIntervalGenerator, TimeIntervalGenerator>(lifetime: InstanceLifetime.Singleton);
//            registry.Register<ISessionController, SessionController>(lifetime: InstanceLifetime.Singleton);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registry"></param>
        public static void RegisterViewModels(IServiceRegistry registry)
        {
            registry.Register<IBeehiveFactory, BeehiveFactory>(lifetime: InstanceLifetime.Singleton);
            registry.Register<IBeeFactory, BeeFactory>(lifetime: InstanceLifetime.Singleton);
            registry.Register<ISimulation, Simulation>(lifetime: InstanceLifetime.CreateNew);

            registry.Register<HostPageViewModel>(lifetime: InstanceLifetime.CreateNew);
            registry.Register<MainPageViewModel>(lifetime: InstanceLifetime.CreateNew);
            registry.Register<OptionsPageViewModel>(lifetime: InstanceLifetime.CreateNew);
        }
    }
}