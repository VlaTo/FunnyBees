using System;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace LibraProgramming.Windows.UI.Xaml.Primitives.Commanding
{
    public class PredefinedDialogAction : DialogAction
    {
        public static readonly DependencyProperty ActionProperty;

        private WellKnownAction knownAction;
        private bool hasSubscription;

        public WellKnownActions Action
        {
            get
            {
                return (WellKnownActions) GetValue(ActionProperty);
            }
            set
            {
                SetValue(ActionProperty, value);
            }
        }

        static PredefinedDialogAction()
        {
            ActionProperty = DependencyProperty
                .Register(
                    "Action",
                    typeof (WellKnownActions),
                    typeof (PredefinedDialogAction),
                    new PropertyMetadata(WellKnownActions.Cancel, OnActionPropertyChanged)
                );
        }

        protected override void ExecuteInternal()
        {
            EnsureAction();
            knownAction.Execute(this);
        }

        protected override void SetActionDispatcher(ActionDispatcher value)
        {
            base.SetActionDispatcher(value);

            UpdateAction();
        }

        protected override void SetOwnerDialog(CustomDialog value)
        {
            if (null != OwnerDialog)
            {
                OnOwnerDialogClosed(OwnerDialog, EventArgs.Empty);

                OwnerDialog.Opened -= OnOwnerDialogOpened;
                OwnerDialog.Closed -= OnOwnerDialogClosed;
                OwnerDialog.Loaded -= OnOwnerDialogLoaded;
            }

            base.SetOwnerDialog(value);

            if (null != OwnerDialog)
            {
                OwnerDialog.Opened += OnOwnerDialogOpened;
                OwnerDialog.Closed += OnOwnerDialogClosed;
                OwnerDialog.Loaded += OnOwnerDialogLoaded;
            }
        }

        private void EnsureAction()
        {
            if (null == knownAction)
            {
                throw new InvalidOperationException();
            }
        }

        private void UpdateAction()
        {
            if (null == ActionDispatcher)
            {
                return;
            }

            knownAction = ActionDispatcher.GetKnownAction(Action);

            var suffix = Enum.GetName(typeof(WellKnownActions), Action);
            var key = $"CustomDialog/Action/{suffix}";

            Title = PrimitivesLocalizationManager.Current.GetString(key);
        }

        private void OnOwnerDialogOpened(object sender, EventArgs e)
        {
            if (sender != OwnerDialog)
            {
                return;
            }

            if (WellKnownActions.Cancel == Action || WellKnownActions.Close == Action)
            {
                Window.Current.CoreWindow.KeyDown += OnCoreWindowKeyDown;
                hasSubscription = true;
            }
        }

        private void OnOwnerDialogClosed(object sender, EventArgs e)
        {
            if (sender != OwnerDialog)
            {
                return;
            }

            if (hasSubscription)
            {
                Window.Current.CoreWindow.KeyDown -= OnCoreWindowKeyDown;
                hasSubscription = false;
            }
        }

        private void OnOwnerDialogLoaded(object sender, RoutedEventArgs e)
        {
            if (OwnerDialog.IsOpen)
            {
                OnOwnerDialogOpened(sender, EventArgs.Empty);
            }
        }

        private void OnCoreWindowKeyDown(CoreWindow sender, KeyEventArgs args)
        {
            if (!hasSubscription)
            {
                return;
            }

            if (VirtualKey.Escape == args.VirtualKey)
            {
                args.Handled = true;
                Execute(null);
            }
        }

        private static void OnActionPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            ((PredefinedDialogAction) source).UpdateAction();
        }
    }
}