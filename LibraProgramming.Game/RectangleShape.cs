using System;
using System.Numerics;
using Windows.Foundation;
using Windows.UI;
using FunnyBees.Engine;
using Microsoft.Graphics.Canvas;

namespace FunnyBees.Game
{
    public class RectangleShape : DrawableObject
    {
        private static readonly TimeSpan duration = TimeSpan.FromSeconds(3.0d);

        private readonly Point origin;
        private readonly Size size;
        private readonly Color color;
        private float angle;


        public RectangleShape(Point origin, Size size, Color color)
        {
            this.origin = origin;
            this.size = size;
            this.color = color;
            angle = 0.0f;
        }

        public override void Draw(CanvasDrawingSession session)
        {
            var center = new Vector2((float) (size.Width / 2.0d + origin.X), (float) (size.Height / 2.0d + origin.Y));

            session.Transform = Matrix3x2.CreateRotation(angle, center);

            session.DrawRectangle(new Rect(origin, size), color);

            session.Transform = Matrix3x2.Identity;

            base.Draw(session);
        }

        public override void Update(TimeSpan elapsedTime)
        {
            var d = duration.TotalMilliseconds;
            var milliseconds = Math.IEEERemainder(elapsedTime.TotalMilliseconds, d);

            angle = (float) (Math.PI * milliseconds / d);
            
            base.Update(elapsedTime);
        }
    }
}