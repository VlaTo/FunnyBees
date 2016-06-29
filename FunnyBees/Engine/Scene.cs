using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FunnyBees.Engine
{
    public class Scene
    {
        private readonly ISceneBuilder builder;
        private readonly ICollection<SceneObject> objects;
        private readonly ICollection<GameObject> updatable;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public Scene(ISceneBuilder builder)
        {
            if (null == builder)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            this.builder = builder;

            objects = new Collection<SceneObject>();
            updatable = new Collection<GameObject>();
        }

        public Task Initialize()
        {
            return builder.CreateScene(this);
        }

        public void AddObject(GameObject obj)
        {
            if (obj is SceneObject)
            {
                InsertDrawable(objects.Count, (SceneObject) obj);
                return;
            }

            InsertUpdatable(updatable.Count, obj);
        }

        private void InsertDrawable(int index, SceneObject sceneObject)
        {
            throw new NotImplementedException();
        }

        private void InsertUpdatable(int index, GameObject gameObject)
        {
            throw new NotImplementedException();
        }
    }
}