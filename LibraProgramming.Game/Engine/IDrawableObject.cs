namespace LibraProgramming.Game.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDrawableObject : ISceneObject, ISceneUpdatable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void Draw(IDrawingContext context);
    }
}