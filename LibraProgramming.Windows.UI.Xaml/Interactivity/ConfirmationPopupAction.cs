using System;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using LibraProgramming.Windows.Interaction;
using LibraProgramming.Windows.Interactivity;

namespace LibraProgramming.Windows.UI.Xaml.Interactivity
{
    public sealed class ConfirmationPopupAction : TargetTriggerAction<CustomDialog>
    {
        protected override void Invoke(object value)
        {
            var e = value as InteractionRequestedEventArgs;

            if (null == e)
            {
                return;
            }

            var dialog = Target;

            if (null == dialog)
            {
                return;
            }

            var confirmation = (Confirmation)e.Context;
            var dispatcher = dialog.ActionDispatcher;
            EventHandler onclosed = null;
            TypedEventHandler<ActionDispatcher, DispatcherActionExecuteEventArgs> onexecute = (source, args) =>
            {
                foreach (var knownaction in new[]{WellKnownActions.Ok, WellKnownActions.Yes})
                {
                    var action = dispatcher.GetKnownAction(knownaction);

                    if (args.Action == action)
                    {
                        confirmation.Confirmed = true;
                        break;
                    }
                }
            };

            onclosed = (source, notused) =>
            {
                dialog.Closed -= onclosed;
                dispatcher.ExecuteAction -= onexecute;

                e.Callback?.Invoke();
            };

            dialog.Closed += onclosed;
            dispatcher.ExecuteAction += onexecute;

            if (false == String.IsNullOrEmpty(confirmation.Title))
            {
                dialog.Title = confirmation.Title;
            }

            if (false == String.IsNullOrEmpty(confirmation.Content))
            {
                dialog.Content = confirmation.Content;
            }

            dialog.IsOpen = true;
        }
    }
}