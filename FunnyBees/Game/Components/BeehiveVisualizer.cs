﻿using Windows.Foundation;
using Windows.UI;
using FunnyBees.Engine;

namespace FunnyBees.Game.Components
{
    public class BeehiveVisualizer : Component<Beehive>
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