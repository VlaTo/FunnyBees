using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace LibraProgramming.Windows.Interactivity
{
    public class PageOrientationTrigger : TriggerBase<FrameworkElement>
    {
        public PageOrientationTrigger()
            : base(typeof(FrameworkElement))
        {
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            Window.Current.SizeChanged += WindowSizeChanged;
            InvokeActions(ApplicationView.Value);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            Window.Current.SizeChanged -= WindowSizeChanged;
        }

        private void WindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            InvokeActions(ApplicationView.Value);
        }
    }
}
