using System;
using System.IO;
using System.Reflection;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

namespace LibraProgramming.Windows.UI.Xaml.Primitives
{
    /// <summary>
    /// 
    /// </summary>
    public enum IndicatorAnimation
    {
        /// <summary>
        /// 
        /// </summary>
        Ring,

        /// <summary>
        /// 
        /// </summary>
        Bar
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ContentPosition
    {
        /// <summary>
        /// 
        /// </summary>
        Left,

        /// <summary>
        /// 
        /// </summary>
        Top,

        /// <summary>
        /// 
        /// </summary>
        Right,

        /// <summary>
        /// 
        /// </summary>
        Bottom
    }

    /// <summary>
    /// 
    /// </summary>
    [TemplateVisualState(Name = RunningStateName, GroupName = IndicatorStatesGroupName)]
    [TemplateVisualState(Name = StoppedStateName, GroupName = IndicatorStatesGroupName)]
    [TemplatePart(Type = typeof(ContentPresenter), Name = ContentPresenterPartName)]
    [TemplatePart(Type = typeof(BusyIndicatorAnimation), Name = AnimationPartName)]
    [StyleTypedProperty(Property = AnimationStylePropertyName, StyleTargetType = typeof(BusyIndicatorAnimation))]
    [ContentProperty(Name = AnimationStylePropertyName)]
    public class BusyIndicator : ContentControlPrimitive
    {
        public static readonly DependencyProperty AnimationStyleProperty;
        public static readonly DependencyProperty ContentPositionProperty;
        public static readonly DependencyProperty DelayProperty;
        public static readonly DependencyProperty IsActiveProperty;
        public static readonly DependencyProperty IndicatorAnimationProperty;
        public static readonly DependencyProperty OverlayBackgroundBrushProperty;

        private const string ContentPresenterPartName = "PART_Content";
        private const string AnimationPartName = "PART_Animation";

        private const string IndicatorStatesGroupName = "IndicatorStates";
        private const string RunningStateName = "Running";
        private const string StoppedStateName = "Stopped";

        private const string AnimationStylePropertyName = "AnimationStyle";

        private ContentPresenter content;
        private readonly DispatcherTimer timer;
        private BusyIndicatorAnimation animation;
        private readonly Style[] stylesCache;

        /// <summary>
        /// 
        /// </summary>
        public Style AnimationStyle
        {
            get
            {
                return (Style) GetValue(AnimationStyleProperty);
            }
            set
            {
                SetValue(AnimationStyleProperty, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ContentPosition ContentPosition
        {
            get
            {
                return (ContentPosition)GetValue(ContentPositionProperty);
            }
            set
            {
                SetValue(ContentPositionProperty, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan Delay
        {
            get
            {
                return (TimeSpan) GetValue(DelayProperty);
            }
            set
            {
                SetValue(DelayProperty, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsActive
        {
            get
            {
                return (bool) GetValue(IsActiveProperty);
            }
            set
            {
                SetValue(IsActiveProperty, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IndicatorAnimation IndicatorAnimation
        {
            get
            {
                return (IndicatorAnimation)GetValue(IndicatorAnimationProperty);
            }
            set
            {
                SetValue(IndicatorAnimationProperty, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Brush OverlayBackgroundBrush
        {
            get
            {
                return (Brush) GetValue(OverlayBackgroundBrushProperty);
            }
            set
            {
                SetValue(OverlayBackgroundBrushProperty, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public BusyIndicator()
        {
            DefaultStyleKey = typeof (BusyIndicator);

            stylesCache = new Style[2];
            timer = new DispatcherTimer();
            timer.Tick += OnTimerTick;

            SizeChanged += OnSizeChanged;
        }

        static BusyIndicator()
        {
            AnimationStyleProperty = DependencyProperty
                .Register(
                    AnimationStylePropertyName,
                    typeof (Style),
                    typeof (BusyIndicator),
                    new PropertyMetadata(null)
                );
            ContentPositionProperty = DependencyProperty
                .Register(
                    nameof(ContentPosition), //"ContentPosition",
                    typeof (ContentPosition),
                    typeof (BusyIndicator),
                    new PropertyMetadata(ContentPosition.Bottom, OnContentPositionPropertyChanged)
                );
            DelayProperty = DependencyProperty
                .Register(
                    nameof(Delay), // "Delay",
                    typeof (TimeSpan),
                    typeof (BusyIndicator),
                    new PropertyMetadata(TimeSpan.Zero)
                );
            IsActiveProperty = DependencyProperty
                .Register(
                    nameof(IsActive), // "IsActive",
                    typeof (bool),
                    typeof (BusyIndicator),
                    new PropertyMetadata(false, OnIsActivePropertyChanged)
                );
            IndicatorAnimationProperty = DependencyProperty
                .Register(
                    nameof(IndicatorAnimation), // "IndicatorAnimation",
                    typeof (IndicatorAnimation),
                    typeof (BusyIndicator),
                    new PropertyMetadata(IndicatorAnimation.Ring, OnIndicatorAnimationPropertyChanged)
                );
            OverlayBackgroundBrushProperty = DependencyProperty
                .Register(
                    nameof(OverlayBackgroundBrush), // "OverlayBackgroundBrush",
                    typeof (Brush),
                    typeof (BusyIndicator),
                    new PropertyMetadata(DependencyProperty.UnsetValue)
                );
        }

        protected override void OnApplyTemplate()
        {
            content = GetTemplatePart<ContentPresenter>(ContentPresenterPartName);

            if (DependencyProperty.UnsetValue == ReadLocalValue(ContentProperty) && null == GetValue(ContentProperty))
            {
                Content = PrimitivesLocalizationManager.Current.BusyIndicatorContent;
            }

            animation = GetTemplatePart<BusyIndicatorAnimation>(AnimationPartName);

            if (DependencyProperty.UnsetValue == ReadLocalValue(AnimationStyleProperty))
            {
                ApplyAnimationStyle(IndicatorAnimation);
            }

            SynchronizeContentPosition();

            base.OnApplyTemplate();

            UpdateVisualState(false);

            if (String.Equals(RunningStateName, CurrentVisualState))
            {
                animation.Start();
            }
            else
            {
                animation.Stop();
            }
        }

        protected override string GetCurrentVisualStateName()
        {
            return IsActive ? RunningStateName : StoppedStateName;
        }

        private void ApplyAnimationStyle(IndicatorAnimation value)
        {
            var index = (int)value;

            if (null == stylesCache[index])
            {
                var assembly = typeof(BusyIndicatorAnimation).GetTypeInfo().Assembly;
                var resourceName = "LibraProgramming.Windows.UI.Xaml.Primitives.BusyIndicatorAnimation." + index + ".xaml";

                using (var reader = new StreamReader(assembly.GetManifestResourceStream(resourceName)))
                {
                    var xaml = reader.ReadToEnd();
                    stylesCache[index] = XamlReader.Load(xaml) as Style;
                }
            }

            animation?.Stop();

            AnimationStyle = stylesCache[index];

            if (null != animation && String.Equals(RunningStateName, CurrentVisualState))
            {
                animation.Start();
            }
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.OnLoaded(sender, e);

            if (String.Equals(RunningStateName, CurrentVisualState))
            {
                animation.Start();
            }
            else
            {
                if (timer.IsEnabled)
                {
                    return;
                }

                animation.Stop();
            }
        }

        protected override void OnUnloaded(object sender, RoutedEventArgs e)
        {
            base.OnUnloaded(sender, e);
            timer.Stop();
            animation.Stop();
        }

        private void OnIsActiveChanged(bool value)
        {
            var delay = Delay;

            if (value)
            {
                if (TimeSpan.Zero == delay)
                {
                    UpdateVisualState(true);
                }
                else
                {
                    timer.Interval = delay;
                    timer.Start();
                }
            }
            else
            {
                timer.Stop();
                UpdateVisualState(true);
            }
        }
        
        private void OnTimerTick(object sender, object e)
        {
            timer.Stop();
            UpdateVisualState(true);
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Clip = new RectangleGeometry
            {
                Rect = new Rect(new Point(), e.NewSize)
            };
        }

        private void SynchronizeContentPosition()
        {
            switch (ContentPosition)
            {
                case ContentPosition.Left:
                case ContentPosition.Right:
                {
                    Grid.SetColumn(content, ContentPosition.Left == ContentPosition ? 0 : 1);
                    Grid.SetColumn(animation, ContentPosition.Right == ContentPosition ? 0 : 1);
                    Grid.SetRow(content, 0);
                    Grid.SetRow(animation, 0);
                    Grid.SetRowSpan(content, 2);
                    Grid.SetRowSpan(animation, 2);
                    content.ClearValue(Grid.ColumnSpanProperty);
                    animation.ClearValue(Grid.ColumnSpanProperty);
                    content.HorizontalContentAlignment = (ContentPosition.Left == ContentPosition)
                        ? HorizontalAlignment.Right
                        : HorizontalAlignment.Left;
                    content.VerticalContentAlignment = VerticalAlignment.Center;

                    break;
                }

                case ContentPosition.Top:
                case ContentPosition.Bottom:
                {
                    Grid.SetColumn(content, 0);
                    Grid.SetColumn(animation, 0);
                    Grid.SetRow(content, ContentPosition.Top == ContentPosition ? 0 : 1);
                    Grid.SetRow(animation, ContentPosition.Bottom == ContentPosition ? 0 : 1);
                    content.ClearValue(Grid.RowSpanProperty);
                    animation.ClearValue(Grid.RowSpanProperty);
                    Grid.SetColumnSpan(content, 2);
                    Grid.SetColumnSpan(animation, 2);
                    content.VerticalContentAlignment = (ContentPosition.Top == ContentPosition)
                        ? VerticalAlignment.Bottom
                        : VerticalAlignment.Top;
                    content.HorizontalContentAlignment = HorizontalAlignment.Center;

                    break;
                }
            }
        }

        private void OnIndicatorAnimationChanged(IndicatorAnimation value)
        {
            ApplyAnimationStyle(value);
        }

        private void OnContentPositionChanged()
        {
            if (!IsTemplateApplied)
            {
                return;
            }

            SynchronizeContentPosition();
        }

        private static void OnIndicatorAnimationPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            ((BusyIndicator) source).OnIndicatorAnimationChanged((IndicatorAnimation) e.NewValue);
        }

        private static void OnIsActivePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            ((BusyIndicator) source).OnIsActiveChanged((bool) e.NewValue);
        }

        private static void OnContentPositionPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            ((BusyIndicator) source).OnContentPositionChanged();
        }
    }
}
