using System.Windows.Input;

namespace LibraProgramming.Windows.UI.Xaml.Primitives.Commanding
{
    public interface IDialogAction : ICommand
    {
        ActionDispatcher ActionDispatcher
        {
            get;
        }
    }
}