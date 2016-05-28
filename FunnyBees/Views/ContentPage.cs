using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LibraProgramming.Windows.Infrastructure;

namespace FunnyBees.Views
{
    /// <summary>
    /// 
    /// </summary>
    public class ContentPage : Page
    {
        protected ContentPage()
        {
            Loaded += OnPageLoaded;
            Unloaded += OnPageUnloaded;
        }

        private async void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as ISetupRequired;

            if (null != viewModel)
            {
                await viewModel.SetupAsync();
            }
        }

        private async void OnPageUnloaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OnPageLoaded;
            Unloaded -= OnPageUnloaded;

            var viewModel = DataContext as ICleanupRequired;

            if (null != viewModel)
            {
                await viewModel.CleanupAsync();
            }
        }
    }
}