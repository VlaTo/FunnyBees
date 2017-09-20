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

        /*private void OnCanvasCreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
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
            scene.Update(args.Timing.TotalTime);
        }*/

        /*private Task DoCanvasCreateResources()
        {
            var map = new Map(new Size(200.0d, 200.0d), 20, 20);
            var beehive = new Beehive();

            scene.AddChild(map);

            beehive.AddComponent(new BeeManager(10));
            beehive.AddComponent(new BeehiveShape
            {
                Origin = new Point(60.0d, 60.0d),
                Size = new Size(100.0d, 100.0d)
            });

            scene.AddChild(beehive);

            var queen = new Bee();

            queen.AddComponent<QueenBeeBehaviour>();
            queen.InteractWith(beehive).Using<BeeHoster>();

            scene.AddChild(queen);

            var bee = new Bee();

            bee.AddComponent(new HoneyBeeBehaviour(lifespan: TimeSpan.FromSeconds(30.0d)));
            bee.InteractWith(beehive).Using<BeeHoster>();

            scene.AddChild(bee);

            return Task.CompletedTask;
        }*/
    }
}
