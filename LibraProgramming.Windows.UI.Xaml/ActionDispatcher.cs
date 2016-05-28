using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml;
using LibraProgramming.Windows.Infrastructure;
using LibraProgramming.Windows.UI.Xaml.Primitives.Commanding;

namespace LibraProgramming.Windows.UI.Xaml
{
    /// <summary>
    /// 
    /// </summary>
    public enum WellKnownActions
    {
        /// <summary>
        /// 
        /// </summary>
        Yes,

        /// <summary>
        /// 
        /// </summary>
        No,

        /// <summary>
        /// 
        /// </summary>
        Ok,

        /// <summary>
        /// 
        /// </summary>
        Cancel,

        /// <summary>
        /// 
        /// </summary>
        Close
    }

    public abstract class DispatchedAction
    {
        private readonly ActionDispatcher dispatcher;

        protected DispatchedAction(ActionDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        public virtual void Execute(object payload)
        {
            dispatcher.RunAction(this, payload);
        }
    }

    public class DispatcherActionExecuteEventArgs : EventArgs
    {
        public DispatchedAction Action
        {
            get;
        }

        public object Payload
        {
            get;
        }

        public DispatcherActionExecuteEventArgs(DispatchedAction action, object payload)
        {
            Action = action;
            Payload = payload;
        }
    }

    public class WellKnownAction : DispatchedAction
    {
        public WellKnownActions Action
        {
            get;
        }

        public WellKnownAction(ActionDispatcher dispatcher, WellKnownActions action)
            : base(dispatcher)
        {
            Action = action;
        }
    }

    public class ActionDispatcher : DependencyObject
    {
        private readonly Dictionary<WellKnownActions, WellKnownAction> actions;
        private readonly TypedWeakEventHandler<ActionDispatcher, DispatcherActionExecuteEventArgs> executeAction; 

        public event TypedEventHandler<ActionDispatcher, DispatcherActionExecuteEventArgs> ExecuteAction
        {
            add
            {
                executeAction.AddHandler(value);
            }
            remove
            {
                executeAction.RemoveHandler(value);
            }
        }

        public ActionDispatcher()
        {
            actions = new Dictionary<WellKnownActions, WellKnownAction>();
            executeAction = new TypedWeakEventHandler<ActionDispatcher, DispatcherActionExecuteEventArgs>();
        }

        public WellKnownAction GetKnownAction(WellKnownActions action)
        {
            WellKnownAction knownAction;

            if (!actions.TryGetValue(action, out knownAction))
            {
                knownAction = new WellKnownAction(this, action);
                actions.Add(action, knownAction);
            }

            return knownAction;
        }

        public bool CanExecuteCommand(IDialogAction command)
        {
            return true;
        }

        public void RunAction(DispatchedAction action, object payload)
        {
            executeAction.Invoke(this, new DispatcherActionExecuteEventArgs(action, payload));
        }
    }
}