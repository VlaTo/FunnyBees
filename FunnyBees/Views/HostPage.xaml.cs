using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using LibraProgramming.Windows.Commands;
using LibraProgramming.Windows.Navigations;

namespace FunnyBees.Views
{
    /// <summary>
    /// </summary>
    public sealed partial class HostPage : IPageNavigationProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public bool CanGoBack => ContentFrame.CanGoBack;

        /// <summary>
        /// 
        /// </summary>
        public bool CanGoForward => ContentFrame.CanGoForward;

        /// <summary>
        /// 
        /// </summary>
        public HostPage()
        {
            PageNavigation.RegisterProvider(this);
            InitializeComponent();
        }

        private static IEnumerable<RadioButton> GetAllNavigationButtons(UIElementCollection elements)
        {
            return elements.OfType<RadioButton>();
        }

        private void OnFrameNavigated(object sender, NavigationEventArgs e)
        {
            foreach (var button in GetAllNavigationButtons(MenuHostPanel.Children))
            {
                var command = button.Command as NavigateToPageCommand;

                if (null == command)
                {
                    continue;
                }

                button.IsChecked = e.SourcePageType == command.TargetPage;
            }

            MenuSplitView.IsPaneOpen = false;
        }

        public void GoBack()
        {
            ContentFrame.GoBack();
        }

        public void GoForward()
        {
            ContentFrame.GoForward();
        }

        public bool NavigateToPage(Type targetPage, object parameter)
        {
            return ContentFrame.Navigate(targetPage, parameter);
        }
    }
}
