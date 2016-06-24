using System.Threading;
using System.Threading.Tasks;
using FunnyBees.Services;
using LibraProgramming.Windows.Infrastructure;

namespace FunnyBees.ViewModels
{
    public class HostPageViewModel : ViewModel, ISetupRequired, ICleanupRequired
    {
        private readonly IApplicationOptionsProvider optionsProvider;

        public HostPageViewModel(IApplicationOptionsProvider optionsProvider)
        {
            this.optionsProvider = optionsProvider;
            optionsProvider.OptionsChanged += OnOptionsChanged;
        }

        async Task ISetupRequired.SetupAsync()
        {
            var options = await optionsProvider.GetOptionsAsync(CancellationToken.None);
        }

        Task ICleanupRequired.CleanupAsync()
        {
            optionsProvider.OptionsChanged -= OnOptionsChanged;
            return Task.CompletedTask;
        }

        private void OnOptionsChanged(IApplicationOptionsProvider provider, OptionsChangedEventArgs e)
        {
        }
    }
}