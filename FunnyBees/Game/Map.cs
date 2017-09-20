using Windows.Foundation;
using LibraProgramming.Game.Engine;

namespace FunnyBees.Game
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMap
    {
    }

    /// <summary>
    /// </summary>
    public class Map : DrawableObject, IMap
    {
        private Size size;

        public Map(Size size, int x, int y)
        {
            this.size = size;
        }
    }
}