using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace LibraProgramming.Windows.UI.Xaml.Awaitable
{
    public static class FrameworkElementExtension
    {
        public static Task WaitForLoadedAsync(this FrameworkElement element)
        {
            return EventAsync.FromRoutedEvent(
                handler => element.Loaded += handler,
                handler => element.Loaded -= handler
                );
        }

        public static Task WaitForUnload(this FrameworkElement element)
        {
            return EventAsync.FromRoutedEvent(
                handler => element.Unloaded += handler,
                handler => element.Unloaded -= handler
                );
        }
    }
}