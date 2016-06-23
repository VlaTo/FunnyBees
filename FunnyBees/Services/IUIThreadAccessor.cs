using System;
using System.Threading.Tasks;

namespace FunnyBees.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUIThreadAccessor
    {
        /// <summary>
        /// 
        /// </summary>
        Task ExecuteAsync(Action action);

        Task ExecuteAsync(Func<Task> action);
    }
}