using Windows.UI.Core;

namespace FunnyBees.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDispatcherProvider
    {
        /// <summary>
        /// 
        /// </summary>
        CoreDispatcher Dispatcher
        {
            get;
        }
    }
}