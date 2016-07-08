using Microsoft.Graphics.Canvas;

namespace FunnyBees.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DrawableObject : SceneObject, IDrawableObject
    {
        public Scene Scene
        {
            get;
            set;
        }

        public abstract void Draw(CanvasDrawingSession session);
    }
}