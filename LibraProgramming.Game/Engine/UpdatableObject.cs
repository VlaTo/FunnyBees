using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace LibraProgramming.Game.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdatableObject : SceneObject, ISceneUpdatable
    {
        private ImmutableList<ISceneUpdatable> updatables;

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyCollection<ISceneUpdatable> Updatables => new ReadOnlyCollection<ISceneUpdatable>(updatables);

        protected UpdatableObject()
        {
            updatables = ImmutableList<ISceneUpdatable>.Empty;
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="elapsedTime"></param>
        public virtual void Update(TimeSpan elapsedTime)
        {
            foreach (var component in Components)
            {
                component.Update(elapsedTime);
            }

            foreach (var updatable in updatables)
            {
                updatable.Update(elapsedTime);
            }
        }

        protected override void DoChildAdded(ISceneObject child)
        {
            if (child is ISceneUpdatable updatable)
            {
                updatables = updatables.Add(updatable);
            }
        }

        protected override void DoChildRemoved(ISceneObject child)
        {
            if (child is ISceneUpdatable updatable)
            {
                updatables = updatables.Remove(updatable);
            }
        }
    }
}