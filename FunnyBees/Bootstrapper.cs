using Windows.UI.Xaml;
using FunnyBees.Engine;
using FunnyBees.Game;
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
            registry.Register<IUIThreadAccessor>(() => (App) Application.Current, lifetime: InstanceLifetime.Singleton);
            registry.Register<IApplicationOptionsProvider, ApplicationOptionProvider>(lifetime: InstanceLifetime.Singleton);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registry"></param>
        public static void RegisterViewModels(IServiceRegistry registry)
        {
            // Game
            registry.Register<ISceneCreator, ApiarySceneCreator>(lifetime: InstanceLifetime.CreateNew);
            // ViewModels
            registry.Register<ISimulation, Simulation>(lifetime: InstanceLifetime.CreateNew);
            registry.Register<HostPageViewModel>(lifetime: InstanceLifetime.CreateNew);
            registry.Register<MainPageViewModel>(lifetime: InstanceLifetime.CreateNew);
            registry.Register<OptionsPageViewModel>(lifetime: InstanceLifetime.CreateNew);
        }
    }
}