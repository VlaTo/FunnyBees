using Microsoft.Graphics.Canvas;

namespace FunnyBees.Engine
{
    public abstract class SceneObject : GameObject
    {
        public abstract void Draw(CanvasDrawingSession session);
    }
}