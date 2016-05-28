using System;
using System.ComponentModel;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;

namespace LibraProgramming.Windows.Interactivity
{
    [ContentProperty(Name = "Actions")]
    public abstract class TriggerBase : InteractivityBase
    {
        public static readonly DependencyProperty ActionsProperty;

        public TriggerActionCollection Actions
        {
            get
            {
                return (TriggerActionCollection) GetValue(ActionsProperty);
            }
        }

        public event EventHandler<CancelEventArgs> PreviewInvoke;

        static TriggerBase()
        {
            ActionsProperty = DependencyProperty.Register("Actions",
                typeof (TriggerActionCollection),
                typeof (TriggerBase),
                null);
        }

        internal TriggerBase(Type constraint)
        {
            AttachedObjectTypeConstraint = constraint;
            
            var actionCollection = new TriggerActionCollection();
            
            SetValue(ActionsProperty, actionCollection);
        }

        public override void Attach(FrameworkElement element)
        {
            if (element == AttachedObject)
            {
                return;
            }

            if (null != AttachedObject)
            {
                throw new InvalidOperationException("Cannot Host Trigger Multiple Times");
            }

            if (null != element && !AttachedObjectTypeConstraint.GetTypeInfo().IsAssignableFrom(element.GetType().GetTypeInfo()))
            {
                throw new InvalidOperationException("Type Constraint Violated");
            }

            AttachedObject = element;

            OnAssociatedObjectChanged();

            //Attach handles the DataContext
            base.Attach(element);
            
            Actions.Attach(element);
            
            OnAttached();
        }

        protected void InvokeActions(object parameter)
        {
            var handler = PreviewInvoke;

            if (null != handler)
            {
                var e = new CancelEventArgs(false);

                handler(this, e);

                if (e.Cancel)
                {
                    return;
                }
            }

            foreach (var trigger in Actions)
            {
                trigger.Call(parameter);
            }
        }
    }

    public abstract class TriggerBase<T> : TriggerBase where T : FrameworkElement
    {
        protected new T AttachedObject
        {
            get
            {
                return (T) base.AttachedObject;
            }
        }

        protected override sealed Type AttachedObjectTypeConstraint
        {
            get
            {
                return base.AttachedObjectTypeConstraint;
            }
        }

        protected TriggerBase(Type constraint)
            : base(constraint)
        {
        }
    }
}
