using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;
using Microsoft.Graphics.Canvas;

namespace FunnyBees.Engine
{
    public class Scene
    {
        private readonly ISceneBuilder builder;
        private readonly IList<DrawableObject> objects;
        private readonly ICollection<SceneObject> updatable;

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

            objects = new Collection<DrawableObject>();
            updatable = new Collection<SceneObject>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task Initialize()
        {
            return builder.CreateScene(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="object"></param>
        public void InsertObject(int index, DrawableObject @object)
        {
            if (null == @object)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            objects.Insert(index, @object);

            @object.Scene = this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="object"></param>
        public void RemoveObject(DrawableObject @object)
        {
            if (null == @object)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            if (objects.Remove(@object))
            {
                @object.Scene = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="object"></param>
        public void AddObject(DrawableObject @object)
        {
            InsertObject(objects.Count, @object);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elapsedTime"></param>
        public void Update(TimeSpan elapsedTime)
        {
            foreach (var sceneObject in objects)
            {
                sceneObject.Update(elapsedTime);
            }

            foreach (var gameObject in updatable)
            {
                gameObject.Update(elapsedTime);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="drawingSession"></param>
        public void Draw(CanvasDrawingSession drawingSession)
        {
            throw new NotImplementedException();
        }
    }
}