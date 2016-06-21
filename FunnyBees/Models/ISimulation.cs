using System.Collections.Generic;
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
        ICollection<IBeehive> Beehives
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<ISimulationToken> RunAsync();
    }
}