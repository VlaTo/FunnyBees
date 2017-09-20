using System.Threading.Tasks;

namespace FunnyBees.Engine
{
    public interface ISceneCreator
    {
        Task CreateScene(Scene scene);
    }
}