using Windows.Foundation;
using FunnyBees.Engine;

namespace FunnyBees.Game
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMap
    {
    }

    /// <summary>
    /// 
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