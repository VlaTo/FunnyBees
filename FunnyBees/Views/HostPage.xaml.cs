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

            var page = e.Content as ContentPage;

/*
            if (null != page)
            {
                var title = GetPageTitle(page);
                var content = GetHeaderContent(page);
                var element = content as FrameworkElement;

                element?.SetBinding(
                    DataContextProperty,
                    new Binding
                    {
                        Source = page,
                        Path = new PropertyPath("DataContext"),
                        Mode = BindingMode.OneWay
                    });

                PageTitle.Text = title ?? String.Empty;
                PageHeaderCustomContent.Content = content;
            }
*/

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
