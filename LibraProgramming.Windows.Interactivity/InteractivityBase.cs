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

        FrameworkElement IAttachedObject.AttachedObject
        {
            get
            {
                return AttachedObject;
            }
        }
        
        public virtual void Attach(FrameworkElement element)
        {
            if (null != element)
            {
                element.AddDataContextChangedHandler(DataContextChanged);

                if (null != element.DataContext)
                {
                    DataContextChanged(element, EventArgs.Empty);
                }
            }
        }

        public virtual void Detach()
        {
            if (null != AttachedObject)
            {
                AttachedObject.RemoveDataContextChangedHandler(DataContextChanged);
            }
        }

        protected void OnAssociatedObjectChanged()
        {
            var handler = AttachedObjectChanged;

            if (null == handler)
            {
                return;
            }

            handler(this, new EventArgs());
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

        private void DataContextChanged(object sender, EventArgs e)
        {
            var element = sender as FrameworkElement;

            if (null != element)
            {
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
}
