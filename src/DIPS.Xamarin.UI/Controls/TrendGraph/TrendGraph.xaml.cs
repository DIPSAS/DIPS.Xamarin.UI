using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.TrendGraph
{
    /// <summary>
    ///     Bar graph showing a trend
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class TrendGraph : ContentView
    {
        /// <summary>
        ///     <see cref="MaxValue" />
        /// </summary>
        public static readonly BindableProperty MaxValueProperty =
            BindableProperty.Create(nameof(MaxValue), typeof(double), typeof(TrendGraph), 100.0,
                propertyChanged: OnAnyPropertyChanged);

        /// <summary>
        ///     <see cref="MinValue" />
        /// </summary>
        public static readonly BindableProperty MinValueProperty =
            BindableProperty.Create(nameof(MinValue), typeof(double), typeof(TrendGraph), 0.0,
                propertyChanged: OnAnyPropertyChanged);

        /// <summary>
        ///     <see cref="GraphMargin" />
        /// </summary>
        public static readonly BindableProperty GraphMarginProperty =
            BindableProperty.Create(nameof(GraphMargin), typeof(double), typeof(TrendGraph), 0.5,
                propertyChanged: OnAnyPropertyChanged);

        /// <summary>
        ///     <see cref="ItemsSource" />
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(TrendGraph),
                propertyChanged: OnAnyPropertyChanged);

        /// <summary>
        ///     <see cref="ValueMemberPath" />
        /// </summary>
        public static readonly BindableProperty ValueMemberPathProperty =
            BindableProperty.Create(nameof(ValueMemberPath), typeof(string), typeof(TrendGraph), string.Empty,
                propertyChanged: OnAnyPropertyChanged);

        /// <summary>
        ///     <see cref="LowerBound" />
        /// </summary>
        public static readonly BindableProperty LowerBoundProperty =
            BindableProperty.Create(nameof(LowerBound), typeof(double?), typeof(TrendGraph), null,
                propertyChanged: OnAnyPropertyChanged);

        /// <summary>
        ///     <see cref="UpperBound" />
        /// </summary>
        public static readonly BindableProperty UpperBoundProperty =
            BindableProperty.Create(nameof(UpperBound), typeof(double?), typeof(TrendGraph), null,
                propertyChanged: OnAnyPropertyChanged);

        private VisualElement? m_previousParent;

        /// <summary>
        ///     Constructor to create the graph.
        /// </summary>
        public TrendGraph()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     If set to <c>True</c>, animates the graph bars on displaying
        ///     <remarks>Default is True</remarks>
        /// </summary>
        public TrendAnimation Animate { get; set; } = TrendAnimation.GrowAll;

        /// <summary>
        ///     Color of each graph
        /// </summary>
        public Color GraphColor { get; set; } = Color.FromHex("#a26eba");

        /// <summary>
        ///     Background color of each graph
        /// </summary>
        public Color GraphBackgroundColor { get; set; } = Color.FromHex("#edf3f4");

        /// <summary>
        ///     Color to draw the graph with if the value is outside the reference area.
        /// </summary>
        public Color OutOfBoundsColor { get; set; } = Color.Red;

        /// <summary>
        ///     Max value of the trend
        /// </summary>
        public double MaxValue
        {
            get => (double)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        /// <summary>
        ///     Min value of the trend
        /// </summary>
        public double MinValue
        {
            get => (double)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }

        /// <summary>
        ///     Margin between each item
        /// </summary>
        public double GraphMargin
        {
            get => (double)GetValue(GraphMarginProperty);
            set => SetValue(GraphMarginProperty, value);
        }

        /// <summary>
        ///     Items used to draw the trends. Use <see cref="ValueMemberPath" /> to expose path to item values
        /// </summary>
        public IList ItemsSource
        {
            get => (IList)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        ///     The property path to use as a value. The value is used to determine the height of the graph.
        /// </summary>
        public string ValueMemberPath
        {
            get => (string)GetValue(ValueMemberPathProperty);
            set => SetValue(ValueMemberPathProperty, value);
        }

        /// <summary>
        ///     Lower bound of the trend values
        /// </summary>
        public double? LowerBound
        {
            get => (double?)GetValue(LowerBoundProperty);
            set => SetValue(LowerBoundProperty, value);
        }

        /// <summary>
        ///     Upper bound of the trend values
        /// </summary>
        public double? UpperBound
        {
            get => (double?)GetValue(UpperBoundProperty);
            set => SetValue(UpperBoundProperty, value);
        }

        /// <summary>
        ///     Starts listening to parent size changes
        /// </summary>
        protected override void OnParentSet()
        {
            if (m_previousParent != null)
            {
                m_previousParent.SizeChanged -= StartRedraw;
                m_previousParent = null;
            }

            if (Parent is VisualElement element)
            {
                element.SizeChanged += StartRedraw;
                m_previousParent = element;
                StartRedraw(this, EventArgs.Empty);
            }

            base.OnParentSet();
        }

        private void StartRedraw(object _, EventArgs __)
        {
            Redraw();
        }

        private void Redraw()
        {
            graphContainer.Children.Clear();
            if (ItemsSource == null ||
                ItemsSource.Count == 0 ||
                MinValue >= MaxValue ||
                !IsVisible ||
                GraphBackgroundColor == GraphColor ||
                Width < 1)
            {
                return;
            }

            if (ItemsSource is INotifyCollectionChanged collectionChanged)
            {
                collectionChanged.CollectionChanged -= CollectionChanged_CollectionChanged;
                collectionChanged.CollectionChanged += CollectionChanged_CollectionChanged;
            }

            var widthPerItem = Width / (ItemsSource.Count + ((ItemsSource.Count - 1) * GraphMargin));
            var margin = widthPerItem * GraphMargin;
            var x = 0.0;
            foreach (var item in ItemsSource)
            {
                DrawGraph(x, item, widthPerItem);
                x += widthPerItem + margin;
            }

            AnimateTrendBarsIfNeeded();
        }

        private void DrawGraph(double x, object item, double widthPerItem)
        {
            var value = item.ExtractDouble(ValueMemberPath, MinValue);
            var color = GraphColor;
            if ((UpperBound != null && value > UpperBound.Value) || (LowerBound != null && value < LowerBound.Value))
            {
                color = OutOfBoundsColor;
            }

            var backBar = CreateGraphBar(GraphBackgroundColor);
            var barHeight = value.CalculateRelativePosition(MinValue, MaxValue);
            var graphBar = CreateGraphBar(color);

            if (Device.RuntimePlatform == Device.iOS)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    graphContainer.Children.Add(backBar,
                        Constraint.RelativeToParent(r => x),
                        Constraint.RelativeToParent(r => 0),
                        Constraint.RelativeToParent(r => widthPerItem),
                        Constraint.RelativeToParent(r => r.Height));

                    graphContainer.Children.Add(graphBar,
                        Constraint.RelativeToParent(r => backBar.X),
                        Constraint.RelativeToParent(r =>
                            backBar.Y + backBar.Height - (barHeight * backBar.Height)),
                        Constraint.RelativeToParent(r => backBar.Width),
                        Constraint.RelativeToParent(r => barHeight * backBar.Height));
                });
            }
            else
            {
                graphContainer.Children.Add(backBar,
                    Constraint.RelativeToParent(r => x),
                    Constraint.RelativeToParent(r => 0),
                    Constraint.RelativeToParent(r => widthPerItem),
                    Constraint.RelativeToParent(r => r.Height));

                graphContainer.Children.Add(graphBar,
                    Constraint.RelativeToParent(r => backBar.X),
                    Constraint.RelativeToParent(r => backBar.Y + backBar.Height - (barHeight * backBar.Height)),
                    Constraint.RelativeToParent(r => backBar.Width),
                    Constraint.RelativeToParent(r => barHeight * backBar.Height));
            }
        }

        private void AnimateTrendBarsIfNeeded()
        {
            switch (Animate)
            {
                case TrendAnimation.GrowAll:
                    AnimateWithGrowAll();
                    return;
                case TrendAnimation.GrowAndFade:
                    AnimateWithGrowAndFade();
                    return;
                case TrendAnimation.GrowAndNone:
                    AnimateWithGrowAndNone();
                    return;
                default:
                    return;
            }
        }

        private void AnimateWithGrowAndNone()
        {
            for (var i = 0; i < graphContainer.Children.Count; i += 2)
            {
                var frontBar = graphContainer.Children[i + 1];
                frontBar.TranslationY += frontBar.Height;
                _ = frontBar.TranslateTo(0, frontBar.TranslationY - frontBar.Height, 750, Easing.Linear);
            }
        }

        private void AnimateWithGrowAndFade()
        {
            for (var i = 0; i < graphContainer.Children.Count; i += 2)
            {
                var behindBar = graphContainer.Children[i];
                behindBar.Opacity = 0;
                _ = behindBar.FadeTo(1, 250, Easing.Linear);

                var frontBar = graphContainer.Children[i + 1];
                frontBar.TranslationY += frontBar.Height;
                _ = frontBar.TranslateTo(0, frontBar.TranslationY - frontBar.Height, 750, Easing.Linear);
            }
        }

        private void AnimateWithGrowAll()
        {
            for (var i = 0; i < graphContainer.Children.Count; i += 2)
            {
                var behindBar = graphContainer.Children[i];
                behindBar.TranslationY += behindBar.Height;
                _ = behindBar.TranslateTo(0, behindBar.TranslationY - behindBar.Height, 500, Easing.Linear);

                var frontBar = graphContainer.Children[i + 1];
                frontBar.TranslationY += frontBar.Height;
                _ = frontBar.TranslateTo(0, frontBar.TranslationY - frontBar.Height, 750, Easing.Linear);
            }
        }

        private static BoxView CreateGraphBar(Color background)
        {
            return new BoxView {BackgroundColor = background, CornerRadius = 0};
        }

        private void CollectionChanged_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            StartRedraw(this, EventArgs.Empty);
        }

        private static void OnAnyPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is TrendGraph trendGraph))
            {
                return;
            }

            trendGraph.StartRedraw(trendGraph, EventArgs.Empty);
        }
    }
}