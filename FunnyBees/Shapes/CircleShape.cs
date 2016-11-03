using System.Numerics;
using Windows.UI;
using Microsoft.Graphics.Canvas;

namespace FunnyBees.Shapes
{
    public class CircleShape : Shape
    {
        public override void Draw(CanvasDrawingSession session)
        {
            base.Draw(session);

            var center = new Vector2(80.0f, 80.0f);
            session.DrawCircle(center, 20.0f, Colors.Black);
            //session.DrawLine(center,);
        }
    }
}