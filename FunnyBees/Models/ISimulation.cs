using System.Threading.Tasks;

namespace FunnyBees.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISimulation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<ISimulationSession> RunAsync();
    }
}