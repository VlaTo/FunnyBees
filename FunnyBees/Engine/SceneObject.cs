using System;
using System.Linq;

namespace FunnyBees.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public class SceneObject : ComponentContainer, ISceneObject, ISceneObjectCollectionObserver
    {
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
            Children = new SceneObjectCollection();
            ((IObservable<ISceneObjectCollectionObserver>) Children).Subscribe(this);
        }

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

            foreach (SceneObject child in Children)
            {
                child.Update(elapsedTime);
            }
        }

        protected virtual void DoObjectAdded(SceneObject @object)
        {
        }

        protected virtual void DoObjectReplaced(SceneObject removedObject, SceneObject insertedObject)
        {
        }

        protected virtual void DoObjectRemove(SceneObject @object)
        {
        }

        void ISceneObjectCollectionObserver.OnChildCollectionChanged(SceneObjectCollectionChange action, int index, SceneObject @object, SceneObject source)
        {
            switch (action)
            {
                case SceneObjectCollectionChange.Inserted:
                {
                    @object.Parent = this;
                    DoObjectAdded(@object);

                    break;
                }

                case SceneObjectCollectionChange.Replaced:
                {
                    @object.Parent = this;
                    source.Parent = null;
                    DoObjectReplaced(source, @object);

                    break;
                }

                case SceneObjectCollectionChange.Removed:
                {
                    @object.Parent = null;
                    DoObjectRemove(@object);

                    break;
                }
            }
        }
    }
}