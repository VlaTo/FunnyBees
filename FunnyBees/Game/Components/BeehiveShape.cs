using Windows.Foundation;
using Windows.UI;
using FunnyBees.Game.Visuals;
using LibraProgramming.Game.Components;

namespace FunnyBees.Game.Components
{
    public class BeehiveShape : Component<Beehive>
    {
        public Point Origin
        {
            get;
            set;
        }

        public Size Size
        {
            get;
            set;
        }

        protected override void OnAttach()
        {
            Container.AddChild(new RectangleShape(Origin, Size, Colors.DarkBlue));
        }
    }
}