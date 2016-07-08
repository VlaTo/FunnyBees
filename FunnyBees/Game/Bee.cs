using FunnyBees.Engine;
using Microsoft.Graphics.Canvas;

namespace FunnyBees.Game
{
    /// <summary>
    /// 
    /// </summary>
    public class Bee : DrawableObject
    {
        /// <summary>
        /// 
        /// </summary>
        public void Die()
        {
            Scene.RemoveObject(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public override void Draw(CanvasDrawingSession session)
        {
            throw new System.NotImplementedException();
        }
    }
}