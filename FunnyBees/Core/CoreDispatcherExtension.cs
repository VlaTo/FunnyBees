using System;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace FunnyBees.Core
{
    public static class CoreDispatcherExtension
    {
        public static Task ExecuteAsync(this CoreDispatcher dispatcher, DispatchedHandler action, CoreDispatcherPriority priority = CoreDispatcherPriority.Normal)
        {
            if (null == dispatcher)
            {
                throw new Exception();
            }

            if (dispatcher.HasThreadAccess)
            {
                action.Invoke();
                return Task.FromResult(true);
            }

            return dispatcher.RunAsync(priority, action).AsTask();
        }

        public static void ThrowIfWrongThread(this CoreDispatcher dispatcher)
        {
            if (null == dispatcher)
            {
                throw new Exception();
            }

            if (!dispatcher.HasThreadAccess)
            {
                throw new Exception("Wrong Thread!");
            }
        }
    }
}