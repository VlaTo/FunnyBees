using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Navigation;
using LibraProgramming.Windows.Commands;
using LibraProgramming.Windows.Navigations;

namespace FunnyBees.Views
{
    /// <summary>
    /// </summary>
    public sealed partial class HostPage : IPageNavigationProvider
    {
        public static readonly DependencyProperty HeaderProperty;

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

        public static FrameworkElement GetHeader(ContentPage page)
        {
            if (null == page)
            {
                throw new ArgumentNullException(nameof(page));
            }

            return (FrameworkElement) page.GetValue(HeaderProperty);
        }

        public static void SetHeader(ContentPage page, FrameworkElement value)
        {
            if (null == page)
            {
                throw new ArgumentNullException(nameof(page));
            }

            page.SetValue(HeaderProperty, value);
        }

        static HostPage()
        {
            HeaderProperty = DependencyProperty
                .RegisterAttached(
                    "Header",
                    typeof (FrameworkElement),
                    typeof (HostPage),
                    new PropertyMetadata(DependencyProperty.UnsetValue, OnHeaderPropertyChanged)
                );
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

            var header = PageHeaderCustomContent.Content as FrameworkElement;

            if (null != header)
            {
                header.ClearValue(DataContextProperty);
                PageHeaderCustomContent.Content = null;
            }

            var page = e.Content as ContentPage;

            if (null != page)
            {
//                var title = GetPageTitle(page);
                var content = GetHeader(page);

                content?.SetBinding(
                    DataContextProperty,
                    new Binding
                    {
                        Source = page,
                        Path = new PropertyPath(nameof(content.DataContext)),
                        Mode = BindingMode.OneWay
                    });

//                PageTitle.Text = title ?? String.Empty;
                PageHeaderCustomContent.Content = content;
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

        private static void OnHeaderPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
        }
    }
}
