using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace FunnyBees.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdatableObject : SceneObject, ISceneUpdatable
    {
        private ImmutableList<ISceneUpdatable> updatables;

        public IReadOnlyCollection<ISceneUpdatable> Updatables => new ReadOnlyCollection<ISceneUpdatable>(updatables);

        protected UpdatableObject()
        {
            updatables = ImmutableList<ISceneUpdatable>.Empty;
        }

        public void Update(TimeSpan elapsedTime)
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
            var updatable = child as ISceneUpdatable;

            if (null != updatable)
            {
                updatables = updatables.Add(updatable);
            }
        }

        protected override void DoChildRemoved(ISceneObject child)
        {
            var updatable = child as ISceneUpdatable;

            if (null != updatable)
            {
                updatables = updatables.Remove(updatable);
            }
        }
    }
}