using Windows.UI.Xaml;
using FunnyBees.ViewModels;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace FunnyBees.Views
{
    /// <summary>
    /// </summary>
    public sealed partial class MainPage
    {
        private MainPageViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();
            AnimatedControl.Paused = true;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            viewModel = (MainPageViewModel) DataContext;
        }

        private void OnUnload(object sender, RoutedEventArgs e)
        {
            AnimatedControl.RemoveFromVisualTree();
            AnimatedControl = null;
        }

        private void OnCanvasCreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
        }

        private void OnCanvasDraw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            using (var drawingSession = args.DrawingSession)
            {
                viewModel.DrawScene(drawingSession);
            }
        }

        private void OnCanvasUpdate(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            //args.Timing.TotalTime
            viewModel.Update(args.Timing);
        }
    }
}
