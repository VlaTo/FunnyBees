using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace LibraProgramming.Game.Engine
{
    /// <summary>
    /// </summary>
    public class DrawableObject : UpdatableObject, IDrawableObject
    {
        private ImmutableList<IDrawableObject> drawables;

        public IReadOnlyCollection<IDrawableObject> Drawables => new ReadOnlyCollection<IDrawableObject>(drawables);

        protected DrawableObject()
        {
            drawables = ImmutableList<IDrawableObject>.Empty;
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="session"></param>
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

            if (child is IDrawableObject drawable)
            {
                drawables = drawables.Add(drawable);
            }
        }

        protected override void DoChildRemoved(ISceneObject child)
        {
            base.DoChildRemoved(child);

            if (child is IDrawableObject drawable)
            {
                drawables = drawables.Remove(drawable);
            }
        }
    }
}