using System.Threading.Tasks;

namespace FunnyBees.Engine
{
    public interface ISceneBuilder
    {
        Task CreateScene(Scene scene);
    }
}