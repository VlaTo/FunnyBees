using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using LibraProgramming.Game.Components;

namespace LibraProgramming.Game.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class SceneObject : ComponentContainer, ISceneObject
    {
        private ImmutableList<ISceneObject> children;

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ISceneObject> Children => children;

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public SceneObject Parent
        {
            get;
            set;
        }

        protected SceneObject()
        {
            children = ImmutableList<ISceneObject>.Empty;
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="child"></param>
        public void AddChild(ISceneObject child)
        {
            if (null == child)
            {
                throw new ArgumentNullException(nameof(child));
            }

            if (children.Contains(child))
            {
                throw new ArgumentException("", nameof(child));
            }

            children = children.Add(child);
            child.Parent = this;

            DoChildAdded(child);
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        public int GetChildIndex(ISceneObject child)
        {
            if (null == child)
            {
                throw new ArgumentNullException(nameof(child));
            }

            return children.IndexOf(child);
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="child"></param>
        public void RemoveChild(ISceneObject child)
        {
            if (null == child)
            {
                throw new ArgumentNullException(nameof(child));
            }

            if (false == children.Contains(child))
            {
                throw new ArgumentException("", nameof(child));
            }

            children = children.Remove(child);
            child.Parent = null;

            DoChildRemoved(child);
        }

        protected virtual void DoChildAdded(ISceneObject child)
        {
        }

        protected virtual void DoChildRemoved(ISceneObject child)
        {
        }
    }
}