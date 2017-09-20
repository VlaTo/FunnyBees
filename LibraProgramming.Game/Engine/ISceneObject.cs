
namespace LibraProgramming.Game.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISceneObject
    {
        /// <summary>
        /// 
        /// </summary>
        SceneObject Parent
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="child"></param>
        void AddChild(ISceneObject child);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        int GetChildIndex(ISceneObject child);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="child"></param>
        void RemoveChild(ISceneObject child);
    }
}