using System.Threading.Tasks;
using Windows.UI.Core;
using FunnyBees.Core;

namespace FunnyBees.Services
{
    public class UIThreadDispatcher : IUIThreadDispatcher
    {
        private readonly CoreDispatcher dispatcher;

        public UIThreadDispatcher(CoreDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        public Task ExecuteAsync(DispatchedHandler action, CoreDispatcherPriority priority = CoreDispatcherPriority.Normal)
        {
            return CoreDispatcherExtension.ExecuteAsync(dispatcher, action, priority);
        }

        public void ThrowIfWrongThread()
        {
            CoreDispatcherExtension.ThrowIfWrongThread(dispatcher);
        }
    }
}