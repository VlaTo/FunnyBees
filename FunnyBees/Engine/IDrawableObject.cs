using Microsoft.Graphics.Canvas;

namespace FunnyBees.Engine
{
    public interface IDrawableObject : ISceneObject
    {
        Scene Scene
        {
            get;
            set;
        }

        void Draw(CanvasDrawingSession session);
    }
}