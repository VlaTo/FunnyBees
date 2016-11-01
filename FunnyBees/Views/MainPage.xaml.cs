using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using FunnyBees.Engine;
using FunnyBees.Game;
using FunnyBees.Game.Components;
using FunnyBees.Game.Interactors;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace FunnyBees.Views
{
    /// <summary>
    /// </summary>
    public sealed partial class MainPage
    {
        private readonly Scene scene;

        public MainPage()
        {
            InitializeComponent();
            scene = new Scene();
            Scene.Current = scene;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void OnUnload(object sender, RoutedEventArgs e)
        {
            AnimatedControl.RemoveFromVisualTree();
            AnimatedControl = null;
        }

        private void OnCanvasCreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(DoCanvasCreateResources().AsAsyncAction());
        }

        private void OnCanvasDraw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            using (var drawingSession = args.DrawingSession)
            {
                scene.Draw(drawingSession);
            }
        }

        private void OnCanvasUpdate(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            var elapsedTime = args.Timing.TotalTime;
            scene.Update(elapsedTime);
        }

        private Task DoCanvasCreateResources()
        {
            var beehive = new Beehive();

            beehive.AddComponent(new BeeManager(10));
            beehive.AddComponent(new BeehiveVisualizer
            {
                Origin = new Point(60.0d, 60.0d),
                Size = new Size(100.0d, 100.0d)
            });

            scene.AddChild(beehive);

            var queen = new Bee();

            queen.AddComponent(new BeeLifetime(TimeSpan.FromSeconds(3.0d)));
            queen.AddComponent<BeeBehaviour>();
            queen.AddComponent<QueenBee>();
            queen.InteractWith(beehive).Using<BeeHoster>();

            scene.AddChild(queen);

            return Task.CompletedTask;
        }
    }
}
