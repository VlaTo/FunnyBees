using System.Linq;
using Microsoft.Graphics.Canvas;

namespace FunnyBees.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class DrawableObject : SceneObject, IDrawableObject
    {
/*
        public Scene Scene
        {
            get;
            set;
        }
*/

        public virtual void Draw(CanvasDrawingSession session)
        {
            foreach (var child in Children.OfType<IDrawableObject>())
            {
                child.Draw(session);
            }
        }
    }
}