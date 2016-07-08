using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FunnyBees.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class SceneObject : ComponentContainer, ISceneObject, ISceneObjectCollectionObserver
    {
        private ICollection<IUpdatable> updatables;
        private IDisposable subscription;

        public SceneObjectCollection Children
        {
            get;
        }

        public SceneObject Parent
        {
            get;
            private set;
        }

        protected SceneObject()
        {
            updatables = new Collection<IUpdatable>();
            Children = new SceneObjectCollection();
            subscription = ((IObservable<ISceneObjectCollectionObserver>) Children).Subscribe(this);
        }

        /*public void AddChild(SceneObject @object)
        {
            InsertChild(children.Count, @object);
        }

        public void InsertChild(int position, SceneObject @object)
        {
            if (null == @object)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            children.Insert(position, @object);

            @object.Parent = this;
        }

        public void RemoveChild(SceneObject @object)
        {
            if (null == @object)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            if (children.Remove(@object))
            {
                @object.Parent = null;
            }
        }*/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elapsedTime"></param>
        public virtual void Update(TimeSpan elapsedTime)
        {
            foreach (var component in Components.OfType<IUpdatable>())
            {
                component.Update(elapsedTime);
            }
        }

        void ISceneObjectCollectionObserver.OnChildCollectionChanged(SceneObjectCollectionChange action, int index, SceneObject @object, SceneObject source)
        {
            switch (action)
            {
                case SceneObjectCollectionChange.Inserted:
                {
                    @object.Parent = this;
                    RegisterAsUpdatable(@object);

                    break;
                }

                case SceneObjectCollectionChange.Replaced:
                {
                    @object.Parent = this;
                    source.Parent = null;
                    RegisterAsUpdatable(@object);

                    break;
                }

                case SceneObjectCollectionChange.Removed:
                {
                    @object.Parent = null;

                    break;
                }
            }
        }

        private void RegisterAsUpdatable(SceneObject @object)
        {
            var impl = @object as IUpdatable;

            if (null != impl)
            {
                updatables.Add(impl);
            }
        }
    }
}