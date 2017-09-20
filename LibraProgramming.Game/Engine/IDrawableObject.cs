using Microsoft.Graphics.Canvas;

namespace FunnyBees.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDrawableObject : ISceneObject, ISceneUpdatable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        void Draw(CanvasDrawingSession session);
    }
}