using System;
using Windows.UI.Xaml;
using LibraProgramming.Windows.Infrastructure;

namespace LibraProgramming.Windows.UI.Xaml.Primitives.Commanding
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class DialogAction : DependencyObject, IDialogAction
    {
        public static readonly DependencyProperty TitleProperty;
        public static readonly DependencyProperty IsEnabledProperty;

        private ActionDispatcher dispatcher;
        private CustomDialog ownerDialog;
        private readonly WeakEventHandler canExecuteChanged;

        public ActionDispatcher ActionDispatcher
        {
            get
            {
                return dispatcher;
            }
            set
            {
                if (ReferenceEquals(dispatcher, value))
                {
                    return;
                }

                SetActionDispatcher(value);
            }
        }

        public bool IsEnabled
        {
            get
            {
                return (bool) GetValue(IsEnabledProperty);
            }
            set
            {
                SetValue(IsEnabledProperty, value);
            }
        }

        public string Title
        {
            get
            {
                return (string) GetValue(TitleProperty);
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        public CustomDialog OwnerDialog
        {
            get
            {
                return ownerDialog;
            }
            set
            {
                if (ReferenceEquals(value, ownerDialog))
                {
                    return;
                }

                SetOwnerDialog(value);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                canExecuteChanged.AddHandler(value);
            }
            remove
            {
                canExecuteChanged.RemoveHandler(value);
            }
        }

        protected DialogAction()
        {
            canExecuteChanged = new WeakEventHandler();
        }

        static DialogAction()
        {
            TitleProperty = DependencyProperty
                .Register(
                    "Title",
                    typeof (string),
                    typeof (DialogAction),
                    new PropertyMetadata(DependencyProperty.UnsetValue, OnTitlePropertyChanged)
                );
            IsEnabledProperty = DependencyProperty
                .Register(
                    "IsEnabled",
                    typeof (bool),
                    typeof (DialogAction),
                    new PropertyMetadata(true, OnIsEnabledPropertyChanged)
                );
        }

        public virtual bool CanExecute(object parameter)
        {
            return IsEnabled;
        }

        public void Execute(object parameter)
        {
            if (false == CanExecute(parameter))
            {
                return;
            }

            ExecuteInternal();
        }

        protected abstract void ExecuteInternal();

        protected virtual void SetActionDispatcher(ActionDispatcher value)
        {
            dispatcher = value;
        }

        protected virtual void SetOwnerDialog(CustomDialog value)
        {
            ownerDialog = value;
        }

        private void OnIsEnabledChanged()
        {
            canExecuteChanged.Invoke(this, EventArgs.Empty);
        }

        private static void OnTitlePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
        }

        private static void OnIsEnabledPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            ((DialogAction) source).OnIsEnabledChanged();
        }
    }
}