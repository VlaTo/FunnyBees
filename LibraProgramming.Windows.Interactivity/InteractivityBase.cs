using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace LibraProgramming.Windows.Interactivity
{
    public abstract class InteractivityBase : FrameworkElement, IAttachedObject
    {
        protected FrameworkElement AttachedObject
        {
            get; 
            set;
        }

        protected virtual Type AttachedObjectTypeConstraint
        {
            get;
            set;
        }

        internal event EventHandler AttachedObjectChanged;

        FrameworkElement IAttachedObject.AttachedObject => AttachedObject;

        public virtual void Attach(FrameworkElement element)
        {
            if (null != element)
            {
                element.AddDataContextChangedHandler(DoDataContextChanged);

                if (null != element.DataContext)
                {
                    DoDataContextChanged(element, EventArgs.Empty);
                }
            }
        }

        public virtual void Detach()
        {
            AttachedObject?.RemoveDataContextChangedHandler(DoDataContextChanged);
        }

        protected void OnAssociatedObjectChanged()
        {
            AttachedObjectChanged?.Invoke(this, new EventArgs());
        }
        
        protected virtual void OnAttached()
        {
        }
        
        protected virtual void OnDetaching()
        {
        }
        
        protected virtual void OnDataContextChanged(object oldValue, object newValue)
        {
        }    

        private void DoDataContextChanged(object sender, EventArgs e)
        {
            var element = sender as FrameworkElement;

            if (null == element)
            {
                return;
            }

            var dataContext = DataContext;

            SetBinding(DataContextProperty,
                new Binding
                {
                    Path = new PropertyPath("DataContext"),
                    Source = element
                });

            OnDataContextChanged(dataContext, DataContext);
        }
    }
}
