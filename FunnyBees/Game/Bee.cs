using System.Diagnostics;
using FunnyBees.Engine;
using Microsoft.Graphics.Canvas;

namespace FunnyBees.Game
{
    /// <summary>
    /// 
    /// </summary>
    public class Bee : DrawableObject
    {
        public Beehive Beehive
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public override void Draw(CanvasDrawingSession session)
        {
        }

        public void Die()
        {
            Beehive.RemoveChild(this);
            Debug.WriteLine("[Bee.Die] executed");
        }
    }
}