using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace FunnyBees.Engine
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

        /// <summary>
        /// 
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

        /// <summary>
        /// 
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

        /// <summary>
        /// 
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

        /// <summary>
        /// 
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