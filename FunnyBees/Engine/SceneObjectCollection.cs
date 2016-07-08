using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LibraProgramming.Windows;

namespace FunnyBees.Engine
{
    public sealed class SceneObjectCollection : CollectionBase, IList<SceneObject>, IObservable<ISceneObjectCollectionObserver>
    {
        private readonly ICollection<ISceneObjectCollectionObserver> observers;
        private int revision;

        public bool IsReadOnly { get; } = false;

        public SceneObject this[int index]
        {
            get
            {
                return (SceneObject) InnerList[index];
            }
            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                var existing = (SceneObject) InnerList[index];

                InnerList[index] = value;

                DoCollectionChange(SceneObjectCollectionChange.Replaced, index, value, existing);
            }
        }

        public SceneObjectCollection()
        {
            observers = new Collection<ISceneObjectCollectionObserver>();
        }

        public void Add(SceneObject item)
        {
            if (null == item)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var index = InnerList.Add(item);

            DoCollectionChange(SceneObjectCollectionChange.Inserted, index, item);
        }

        public bool Contains(SceneObject item)
        {
            if (null == item)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return InnerList.Contains(item);
        }

        public void CopyTo(SceneObject[] array, int arrayIndex)
        {
            InnerList.CopyTo(array, arrayIndex);
        }

        public bool Remove(SceneObject item)
        {
            if (null == item)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var index = InnerList.IndexOf(item);

            if (0 > index)
            {
                return false;
            }

            InnerList.RemoveAt(index);

            DoCollectionChange(SceneObjectCollectionChange.Removed, index, item);

            return true;
        }

        public int IndexOf(SceneObject item)
        {
            return InnerList.IndexOf(item);
        }

        public void Insert(int index, SceneObject item)
        {
            if (0 > index || index > Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, "");
            }

            if (null == item)
            {
                throw new ArgumentNullException(nameof(item));
            }

            InnerList.Insert(index, item);

            DoCollectionChange(SceneObjectCollectionChange.Inserted, index, item);
        }

        IEnumerator<SceneObject> IEnumerable<SceneObject>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        IDisposable IObservable<ISceneObjectCollectionObserver>.Subscribe(ISceneObjectCollectionObserver observer)
        {
            if (null == observer)
            {
                throw new ArgumentNullException(nameof(observer));
            }

            if (false == observers.Contains(observer))
            {
                observers.Add(observer);
            }

            return new DisposableToken<ISceneObjectCollectionObserver>(observer, UnsubscribeObserver);
        }

        private void UnsubscribeObserver(ISceneObjectCollectionObserver observer)
        {
            if (observers.Remove(observer))
            {
                ;
            }
        }

        private void DoCollectionChange(SceneObjectCollectionChange action, int index, SceneObject item, SceneObject source = null)
        {
            revision++;

            foreach (var observer in observers)
            {
                observer.OnChildCollectionChanged(action, index, item, source);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private sealed class Enumerator : IEnumerator<SceneObject>
        {
            private SceneObjectCollection collection;
            private int index;
            private bool disposed;
            private readonly int revision;

            public SceneObject Current
            {
                get
                {
                    EnsureNotDisposed();
                    EnsureNotModified();

                    return collection[index];
                }
            }

            object IEnumerator.Current => Current;

            public Enumerator(SceneObjectCollection collection)
            {
                this.collection = collection;
                revision = collection.revision;
            }

            public bool MoveNext()
            {
                EnsureNotDisposed();
                EnsureNotModified();

                if (0 > index)
                {
                    if (1 > collection.Count)
                    {
                        return false;
                    }

                    index = 0;

                    return true;
                }

                if (index >= (collection.Count - 1))
                {
                    return false;
                }

                index++;

                return true;
            }

            public void Reset()
            {
                EnsureNotDisposed();
                EnsureNotModified();
                index = -1;
            }

            public void Dispose()
            {
                Dispose(true);
            }

            private void Dispose(bool dispose)
            {
                EnsureNotDisposed();

                try
                {
                    if (dispose)
                    {
                        collection = null;
                    }
                }
                finally
                {
                    disposed = true;
                }
            }

            private void EnsureNotDisposed()
            {
                if (disposed)
                {
                    throw new ObjectDisposedException(GetType().FullName);
                }
            }

            private void EnsureNotModified()
            {
                if (collection.revision != revision)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}