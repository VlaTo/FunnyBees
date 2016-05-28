using System.Threading.Tasks;

namespace LibraProgramming.Windows.UI.Xaml.Awaitable
{
    public static class CustomDialogExtension
    {
        public static Task WaitForCloseAsync(this CustomDialog dialog)
        {
            return EventAsync.FromEvent(
                handler => dialog.Closed += handler,
                handler => dialog.Closed -= handler
                );
        }

        public static Task<DialogActionExecuteEventArgs> WaitForActionAsync(this CustomDialog dialog)
        {
            return EventAsync.FromEvent<DialogActionExecuteEventArgs>(
                handler => dialog.ExecuteAction += handler,
                handler => dialog.ExecuteAction -= handler
                );
        }
    }
}