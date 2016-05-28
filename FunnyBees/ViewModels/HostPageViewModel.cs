using System.Threading.Tasks;
using LibraProgramming.FunnyBees.Services;
using LibraProgramming.Windows.Infrastructure;

namespace FunnyBees.ViewModels
{
    public class HostPageViewModel : ObservableViewModel, ISetupRequired, ICleanupRequired
    {
        private readonly IBeeApiarOptionsProvider optionsProvider;

        public HostPageViewModel(IBeeApiarOptionsProvider optionsProvider)
        {
            this.optionsProvider = optionsProvider;
            optionsProvider.OptionsChanged += OnOptionsChanged;
        }

        async Task ISetupRequired.SetupAsync()
        {
            var options = await optionsProvider.GetOptionsAsync();
        }

        Task ICleanupRequired.CleanupAsync()
        {
            optionsProvider.OptionsChanged -= OnOptionsChanged;
            return Task.CompletedTask;
        }

        private void OnOptionsChanged(IBeeApiarOptionsProvider provider, OptionsChangedEventArgs e)
        {
        }
    }
}