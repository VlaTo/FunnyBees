using System;
using System.Collections;
using System.Collections.Specialized;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using LibraProgramming.Windows.Infrastructure;
using LibraProgramming.Windows.UI.Xaml.Primitives;
using LibraProgramming.Windows.UI.Xaml.Primitives.Commanding;

namespace LibraProgramming.Windows.UI.Xaml
{
    /// <summary>
    /// 
    /// </summary>
    public class DialogActionExecuteEventArgs : EventArgs
    {
        public IDialogAction Action
        {
            get;
        }

        public DialogActionExecuteEventArgs(IDialogAction action)
        {
            Action = action;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [TemplatePart(Type = typeof(Popup), Name = RootPopupPartName)]
    [TemplatePart(Type = typeof(Grid), Name = RootOverlayPartName)]
    [TemplatePart(Type = typeof(Border), Name = RootDialogPartName)]
    [TemplatePart(Type = typeof(Grid), Name = ActionsPanelContainerPartName)]
    [TemplatePart(Type = typeof(ItemsControl), Name = ActionsHostPartName)]
    [ContentProperty(Name = "Content")]
    public sealed class CustomDialog : ContentControlPrimitive
    {
        private const string RootPopupPartName = "PART_RootPopup";
        private const string RootOverlayPartName = "PART_RootOverlay";
        private const string RootDialogPartName = "PART_RootDialog";
        private const string ActionsPanelContainerPartName = "PART_ActionsGrid";
        private const string ActionsHostPartName = "PART_ActionsHost";

        public static readonly DependencyProperty DialogWidthProperty;
        public static readonly DependencyProperty DialogHorizontalAlignmentProperty;
        public static readonly DependencyProperty DialogMarginProperty;
        public static readonly DependencyProperty IsOpenProperty;
        public static readonly DependencyProperty MinDialogWidthProperty;
        public static readonly DependencyProperty MaxDialogWidthProperty;
        public static readonly DependencyProperty OverlayProperty;
        public static readonly DependencyProperty TitleProperty;
        public static readonly DependencyProperty TitleTemplateProperty;
        public static readonly DependencyProperty ActionsPanelProperty;
        public static readonly DependencyProperty ActionTemplateProperty;
        public static readonly DependencyProperty ActionDispatcherProperty;

        private Popup popup;
        private Grid overlayGrid;
        private Border dialogContainer;
        private Grid actionsGrid;
        private ItemsControl actionsHost;
        private readonly WeakEventHandler opened;
        private readonly WeakEventHandler closed;
        private readonly WeakEventHandler<DialogActionExecuteEventArgs> executeAction;

        /// <summary>
        /// 
        /// </summary>
        public DialogActionCollection Actions
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTemplate ActionTemplate
        {
            get
            {
                return (DataTemplate)GetValue(ActionTemplateProperty);
            }
            set
            {
                SetValue(ActionTemplateProperty, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ItemsPanelTemplate ActionsPanel
        {
            get
            {
                return (ItemsPanelTemplate) GetValue(ActionsPanelProperty);
            }
            set
            {
                SetValue(ActionsPanelProperty, value);
            }
        }

        public ActionDispatcher ActionDispatcher
        {
            get
            {
                return (ActionDispatcher) GetValue(ActionDispatcherProperty);
            }
            set
            {
                SetValue(ActionDispatcherProperty, value);
            }
        }

        public double MinDialogWidth
        {
            get
            {
                return (double) GetValue(MinDialogWidthProperty);
            }
            set
            {
                SetValue(MinDialogWidthProperty, value);
            }
        }

        public double MaxDialogWidth
        {
            get
            {
                return (double) GetValue(MaxDialogWidthProperty);
            }
            set
            {
                SetValue(MaxDialogWidthProperty, value);
            }
        }

        public double DialogWidth
        {
            get
            {
                return (double) GetValue(DialogWidthProperty);
            }
            set
            {
                SetValue(DialogWidthProperty, value);
            }
        }

        public HorizontalAlignment DialogHorizontalAlignment
        {
            get
            {
                return (HorizontalAlignment)GetValue(DialogHorizontalAlignmentProperty);
            }
            set
            {
                SetValue(DialogHorizontalAlignmentProperty, value);
            }
        }

        public Thickness DialogMargin
        {
            get
            {
                return (Thickness)GetValue(DialogMarginProperty);
            }
            set
            {
                SetValue(DialogMarginProperty, value);
            }
        }

        public object Title
        {
            get
            {
                return GetValue(TitleProperty);
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        public DataTemplate TitleTemplate
        {
            get
            {
                return (DataTemplate)GetValue(TitleTemplateProperty);
            }
            set
            {
                SetValue(TitleTemplateProperty, value);
            }
        }

        public bool IsOpen
        {
            get
            {
                return (bool)GetValue(IsOpenProperty);
            }
            set
            {
                SetValue(IsOpenProperty, value);
            }
        }

        public Brush Overlay
        {
            get
            {
                return (Brush)GetValue(OverlayProperty);
            }
            set
            {
                SetValue(OverlayProperty, value);
            }
        }

        public event EventHandler Opened
        {
            add
            {
                opened.AddHandler(value);
            }
            remove
            {
                opened.RemoveHandler(value);
            }
        }

        public event EventHandler Closed
        {
            add
            {
                closed.AddHandler(value);
            }
            remove
            {
                closed.RemoveHandler(value);
            }
        }

        public event EventHandler<DialogActionExecuteEventArgs> ExecuteAction
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

        public CustomDialog()
        {
            DefaultStyleKey = typeof(CustomDialog);
            opened = new WeakEventHandler();
            closed = new WeakEventHandler();
            executeAction = new WeakEventHandler<DialogActionExecuteEventArgs>();
            Actions = new DialogActionCollection();

            Loaded += OnControlLoaded;
            Unloaded += OnControlUnloaded;
            ((INotifyCollectionChanged) Actions).CollectionChanged += OnActionsCollectionChanged;
        }

        static CustomDialog()
        {
            ActionsPanelProperty = DependencyProperty
                .Register(
                    "ActionsPanel",
                    typeof(ItemsPanelTemplate),
                    typeof(CustomDialog),
                    new PropertyMetadata(DependencyProperty.UnsetValue, OnActionsPanelPropertyChanged)
                );
            ActionTemplateProperty = DependencyProperty
                .Register(
                    "ActionTemplate",
                    typeof(DataTemplate),
                    typeof(CustomDialog),
                    new PropertyMetadata(null, OnActionTemplatePropertyChanged)
                );
            ActionDispatcherProperty = DependencyProperty
                .Register(
                    "ActionDispatcher",
                    typeof (ActionDispatcher),
                    typeof (CustomDialog),
                    new PropertyMetadata(DependencyProperty.UnsetValue, OnActionDispatcherPropertyChanged)
                );
            MinDialogWidthProperty = DependencyProperty
                .Register(
                    "MinDialogWidth",
                    typeof(double),
                    typeof(CustomDialog),
                    new PropertyMetadata(0.0d)
                );
            MaxDialogWidthProperty = DependencyProperty
                .Register(
                    "MaxDialogWidth",
                    typeof(double),
                    typeof(CustomDialog),
                    new PropertyMetadata(Double.PositiveInfinity)
                );
            DialogWidthProperty = DependencyProperty
                .Register(
                    "DialogWidth",
                    typeof(double),
                    typeof(CustomDialog),
                    new PropertyMetadata(System.Double.NaN)
                );
            DialogHorizontalAlignmentProperty = DependencyProperty
                .Register(
                    "DialogHorizontalAlignment",
                    typeof(HorizontalAlignment),
                    typeof(CustomDialog),
                    new PropertyMetadata(HorizontalAlignment.Center, OnDialogHorizontalAlignmentPropertyChanged)
                );
            DialogMarginProperty = DependencyProperty
                .Register(
                    "DialogMargin",
                    typeof(Thickness),
                    typeof(CustomDialog),
                    new PropertyMetadata(default(Thickness))
                );
            TitleProperty = DependencyProperty
                .Register(
                    "Title",
                    typeof(object),
                    typeof(CustomDialog),
                    new PropertyMetadata(DependencyProperty.UnsetValue)
                );
            TitleTemplateProperty = DependencyProperty
                .Register(
                    "TitleTemplate",
                    typeof(DataTemplate),
                    typeof(CustomDialog),
                    new PropertyMetadata(DependencyProperty.UnsetValue)
                );
            OverlayProperty = DependencyProperty
                .Register(
                    "Overlay",
                    typeof(Brush),
                    typeof(CustomDialog),
                    new PropertyMetadata(DependencyProperty.UnsetValue)
                );
            IsOpenProperty = DependencyProperty
                .Register(
                    "IsOpen",
                    typeof(bool),
                    typeof(CustomDialog),
                    new PropertyMetadata(false, OnIsOpenPropertyChanged)
                );
        }

        public void Close()
        {
            if (IsOpen)
            {
                IsOpen = false;
            }
        }

        protected override void OnApplyTemplate()
        {
            if (null != popup)
            {
                popup.Opened -= OnPopupOpened;
                popup.Closed -= OnPopupClosed;
            }

            popup = GetTemplatePart<Popup>(RootPopupPartName);

            popup.HorizontalOffset = 0.0d;
            popup.VerticalOffset = 0.0d;
            popup.Opened += OnPopupOpened;
            popup.Closed += OnPopupClosed;

            overlayGrid = GetTemplatePart<Grid>(RootOverlayPartName);
            dialogContainer = GetTemplatePart<Border>(RootDialogPartName);
            actionsGrid = GetTemplatePart<Grid>(ActionsPanelContainerPartName);
            actionsHost = GetTemplatePart<ItemsControl>(ActionsHostPartName);

            actionsHost.ItemsSource = Actions;

            base.OnApplyTemplate();

            ResizeContainers();
        }

        private void OnPopupOpened(object sender, object e)
        {
            /*var element = actionsHost.Items[0] as Control;
            element?.Focus(FocusState.Programmatic);*/

            opened.Invoke(this, EventArgs.Empty);
        }

        private void OnPopupClosed(object sender, object e)
        {
            closed.Invoke(this, EventArgs.Empty);
        }

        private void OnControlLoaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged += OnWindowSizeChanged;
        }

        private void OnControlUnloaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged -= OnWindowSizeChanged;
        }

        private void OnWindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            ResizeContainers();
        }

        private void OnActionsCollectionChanged(object source, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                case NotifyCollectionChangedAction.Reset:
                {
                    DoDialogActionsAdded(e.NewStartingIndex, e.NewItems);
                    break;
                }

                case NotifyCollectionChangedAction.Remove:
                {
                    DoDialogActionsRemoved(e.OldStartingIndex, e.OldItems);
                    break;
                }

/*
                case NotifyCollectionChangedAction.Reset:
                {
                    DoDialogActionsAdded(e.NewStartingIndex, e.NewItems);
                    break;
                }
*/
            }
        }

        private void DoDialogActionsAdded(int index, IList actions)
        {
            foreach (DialogAction action in actions)
            {
                action.OwnerDialog = this;
            }
        }

        private void DoDialogActionsRemoved(int index, IList actions)
        {
            foreach (DialogAction action in actions)
            {
                action.OwnerDialog = null;
            }
        }

        private void ResizeContainers()
        {
            var bounds = Window.Current.Bounds;

            popup.Width = bounds.Width;
            popup.Height = bounds.Height;
            overlayGrid.Width = bounds.Width;
            overlayGrid.Height = bounds.Height;
        }

        /*
                private void ArrangeDialog()
                {
                    dialogContainer.HorizontalAlignment = HorizontalAlignment.Left;
                    dialogContainer.VerticalAlignment = VerticalAlignment.Top;
                    dialogContainer.Margin = new Thickness(10, 10, 0, 0);
                }
        */

        private void OnIsOpenChanged(bool value)
        {
            if (value)
            {
                ApplyTemplate();
            }
        }

        private void OnDialogHorizontalAlignmentChanged()
        {
        }

        private void OnCommandsPanelChanged(ItemsPanelTemplate current)
        {
            if (!IsTemplateApplied)
            {
                return;
            }

            ApplyTemplate();
        }


        private void OnCommandTemplateChanged()
        {
            if (!IsTemplateApplied)
            {
                return;
            }

            ;
        }

        private void UpdateActionDispatcher(ActionDispatcher current, ActionDispatcher previous)
        {
            if (null != previous)
            {
                previous.ExecuteAction -= OnActionDispatcherExecuteAction;
            }

            if (null != current)
            {
                current.ExecuteAction += OnActionDispatcherExecuteAction;
            }

            foreach (DialogAction action in Actions)
            {
                action.ActionDispatcher = current;
            }
        }

        private void OnActionDispatcherExecuteAction(ActionDispatcher sender, DispatcherActionExecuteEventArgs e)
        {
            if (sender != ActionDispatcher)
            {
                return;
            }

            var wellKnownActions = Enum.GetValues(typeof (WellKnownActions));

            foreach (WellKnownActions action in wellKnownActions)
            {
                var knownAction = ActionDispatcher.GetKnownAction(action);

                if (e.Action == knownAction)
                {
                    Close();
                }
            }

            executeAction.Invoke(this, new DialogActionExecuteEventArgs(e.Payload as IDialogAction));
        }

        private static void OnActionDispatcherPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            ((CustomDialog) source).UpdateActionDispatcher((ActionDispatcher) e.NewValue, (ActionDispatcher) e.OldValue);
        }

        private static void OnIsOpenPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            ((CustomDialog) source).OnIsOpenChanged((bool) e.NewValue);
        }

        private static void OnDialogHorizontalAlignmentPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            ((CustomDialog) source).OnDialogHorizontalAlignmentChanged();
        }

        private static void OnActionTemplatePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            ((CustomDialog) source).OnCommandTemplateChanged();
        }

        private static void OnActionsPanelPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            ((CustomDialog) source).OnCommandsPanelChanged((ItemsPanelTemplate) e.NewValue);
        }
    }
}
