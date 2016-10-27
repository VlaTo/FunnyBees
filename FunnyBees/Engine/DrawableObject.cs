using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using Microsoft.Graphics.Canvas;

namespace FunnyBees.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class DrawableObject : UpdatableObject, IDrawableObject
    {
        private ImmutableList<IDrawableObject> drawables;

        public IReadOnlyCollection<IDrawableObject> Drawables => new ReadOnlyCollection<IDrawableObject>(drawables);

        protected DrawableObject()
        {
            drawables = ImmutableList<IDrawableObject>.Empty;
        }

        public virtual void Draw(CanvasDrawingSession session)
        {
            foreach (var drawable in drawables)
            {
                drawable.Draw(session);
            }
        }

        protected override void DoChildAdded(ISceneObject child)
        {
            base.DoChildAdded(child);

            var drawable = child as IDrawableObject;

            if (null != drawable)
            {
                drawables = drawables.Add(drawable);
            }
        }

        protected override void DoChildRemoved(ISceneObject child)
        {
            base.DoChildRemoved(child);

            var drawable = child as IDrawableObject;

            if (null != drawable)
            {
                drawables = drawables.Remove(drawable);
            }
        }
    }
}