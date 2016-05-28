using System.Threading.Tasks;
using Windows.UI.Core;

namespace FunnyBees.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUIThreadDispatcher
    {
        /// <summary>
        /// 
        /// </summary>
        Task ExecuteAsync(DispatchedHandler action, CoreDispatcherPriority priority = CoreDispatcherPriority.Normal);

        /// <summary>
        /// 
        /// </summary>
        void ThrowIfWrongThread();
    }
}