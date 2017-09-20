using System;
using Windows.UI.Xaml;
using LibraProgramming.Windows.Infrastructure;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace FunnyBees.Controls
{

    /// <summary>
    /// 
    /// </summary>
    [TemplatePart(Name = CanvasPartName, Type = typeof(GameControl))]
    public sealed class GameControl : ControlPrimitive
    {
        private const string CanvasPartName = "PART_Canvas";

        public static readonly DependencyProperty MapProperty;

        private readonly WeakEventHandler mapChanged;
        private CanvasAnimatedControl canvas;

        public MapDefinition Map
        {
            get
            {
                return (MapDefinition) GetValue(MapProperty);
            }
            set
            {
                SetValue(MapProperty, value);
            }
        }

        public event EventHandler MapChanged
        {
            add
            {
                mapChanged.AddHandler(value);
            }
            remove
            {
                mapChanged.RemoveHandler(value);
            }
        }

        public GameControl()
        {
            DefaultStyleKey = typeof(GameControl);
            mapChanged = new WeakEventHandler();
        }

        static GameControl()
        {
            MapProperty = DependencyProperty
                .Register(
                    nameof(Map),
                    typeof(MapDefinition),
                    typeof(GameControl),
                    new PropertyMetadata(DependencyProperty.UnsetValue, OnMapPropertyChanged)
                );
        }

        protected override void OnApplyTemplate()
        {
            if (null != canvas)
            {
                canvas.CreateResources -= OnCanvasCreateResources;
                canvas.Draw -= OnCanvasDraw;
                canvas.RemoveFromVisualTree();
            }

            canvas = GetTemplatePart<CanvasAnimatedControl>(CanvasPartName);

            canvas.CreateResources += OnCanvasCreateResources;
            canvas.Draw += OnCanvasDraw;

            base.OnApplyTemplate();

            UpdateVisualState(IsLoaded);
        }

        protected override void OnUnloaded(object sender, RoutedEventArgs e)
        {
            if (null != canvas)
            {
                canvas.RemoveFromVisualTree();
            }

            base.OnUnloaded(sender, e);
        }

        private void OnCanvasCreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
//            throw new System.NotImplementedException();
        }

        private void OnCanvasDraw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
//            throw new System.NotImplementedException();
        }

        private void ApplyMap(MapDefinition map)
        {
//            throw new System.NotImplementedException();
        }

        private void OnMapChanged(MapDefinition value)
        {
            if (IsLoaded == false || IsTemplateApplied == false)
            {
                return;
            }

            ApplyMap(value);

            mapChanged.Invoke(this, EventArgs.Empty);
        }

        private static void OnMapPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            ((GameControl) source).OnMapChanged((MapDefinition) e.NewValue);
        }
    }
}