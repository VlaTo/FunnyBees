using System.Threading.Tasks;
using FunnyBees.Localization;
using LibraProgramming.FunnyBees.Services;
using LibraProgramming.Windows.Dependency.Tracking;
using LibraProgramming.Windows.Infrastructure;
using LibraProgramming.Windows.Locator;

namespace FunnyBees.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class OptionsPageViewModel : ObservableViewModel, ISetupRequired
    {
        private static readonly IDependencyTracker<OptionsPageViewModel> tracker;
        private readonly IBeeApiarOptionsProvider provider;
        private readonly IApplicationLocalization localizer;
        private readonly IDependencyTracketSubscription subscription;
        private int delay;

        /// <summary>
        /// 
        /// </summary>
        public bool IsDirty
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Delay
        {
            get
            {
                return delay;
            }
            set
            {
                SetProperty(ref delay, value);
            }
        }

        static OptionsPageViewModel()
        {
            tracker = DependencyTracker.Create<OptionsPageViewModel>(configurator =>
            {
                configurator
                    .RaiseProperty(model => model.IsDirty)
                    .CalculatedBy(model => true)
                    .WhenPropertyChanged(model => model.Delay);
            });
        }

        [PrefferedConstructor]
        public OptionsPageViewModel(IBeeApiarOptionsProvider provider, IApplicationLocalization localizer)
        {
            this.provider = provider;
            this.localizer = localizer;

            subscription = tracker.Subscribe(this);
        }

        async Task ISetupRequired.SetupAsync()
        {
            using (subscription.DisableTracking(true))
            {
//                var enumType = typeof(ShowWelcomeDialog);
                var options = await provider.GetOptionsAsync();

                /*foreach (ShowWelcomeDialog value in Enum.GetValues(enumType))
                {
                    dialogModes.Add(new ShowDialogActionItem(value, localizer.GetStringForShowDialogAction(Enum.GetName(enumType, value))));
                }

                CurrentDialogMode = options.WelcomeDialogAction;*/
            }
        }
    }
}