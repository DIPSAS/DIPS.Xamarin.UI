using System;
using System.Collections;
using Xamarin.Forms;
using DIPS.Xamarin.UI.Extensions;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;

namespace DIPS.Xamarin.UI.Controls.TrendGraph
{
    /// <summary>
    /// Bar graph showing a trend
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class TrendGraph : ContentView
    {
        private VisualElement? m_previousParent;

        /// <summary>
        /// Constructor to create the graph.
        /// </summary>
        public TrendGraph()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Starts listening to parent size changes
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

            var widthPerItem = Width / (ItemsSource.Count + (ItemsSource.Count - 1) * GraphMargin);
            var margin = widthPerItem * GraphMargin;
            var x = 0.0;
            foreach (var item in ItemsSource)
            {
                DrawGraph(x, item, widthPerItem);
                x += widthPerItem + margin;
            }

            if (Animate)
            {
                AnimateTrendBars();
            }
        }

        private void DrawGraph(double x, object item, double widthPerItem)
        {
            var value = item.ExtractDouble(ValueMemberPath, MinValue);
            var color = GraphColor;
            if(UpperBound != null && value > UpperBound.Value || LowerBound != null && value < LowerBound.Value)
            {
                color = OutOfBoundsColor;
            }

            var itemHeight = value.CalculateRelativePosition(MinValue, MaxValue);
            var backFrame = CreateBoxView(GraphBackgroundColor);

            graphContainer.Children.Add(backFrame,
                xConstraint: Constraint.RelativeToParent(r => x),
                yConstraint: Constraint.RelativeToParent(r => 0),
                widthConstraint: Constraint.RelativeToParent(r => widthPerItem),
                heightConstraint: Constraint.RelativeToParent(r => r.Height));

            graphContainer.Children.Add(CreateBoxView(color),
                xConstraint: Constraint.RelativeToParent(r => backFrame.X),
                yConstraint: Constraint.RelativeToParent(r => backFrame.Y + backFrame.Height - (itemHeight * backFrame.Height)),
                widthConstraint: Constraint.RelativeToParent(r => backFrame.Width),
                heightConstraint: Constraint.RelativeToParent(r => itemHeight * backFrame.Height));
        }

        private void AnimateTrendBars()
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

        private BoxView CreateBoxView(Color background) => new BoxView { BackgroundColor = background, CornerRadius = 0 };

        private void CollectionChanged_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            StartRedraw(this, EventArgs.Empty);
        }

        private static void OnAnyPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is TrendGraph trendGraph)) return;
            trendGraph.StartRedraw(trendGraph, EventArgs.Empty);
        }

        /// <summary>
        ///     If set to <c>True</c>, animates the graph bars on displaying
        ///     <remarks>Default is True</remarks>
        /// </summary>
        public bool Animate { get; set; } = true;

        /// <summary>
        /// Color of each graph
        /// </summary>
        public Color GraphColor { get; set; } = Color.FromHex("#a26eba");

        /// <summary>
        /// Background color of each graph
        /// </summary>
        public Color GraphBackgroundColor { get; set; } = Color.FromHex("#edf3f4");

        /// <summary>
        /// Color to draw the graph with if the value is outside the reference area.
        /// </summary>
        public Color OutOfBoundsColor { get; set; } = Color.Red;


        /// <summary>
        ///  <see cref="MaxValue" />
        /// </summary>
        public static readonly BindableProperty MaxValueProperty =
            BindableProperty.Create(nameof(MaxValue), typeof(double), typeof(TrendGraph), 100.0, propertyChanged: OnAnyPropertyChanged);

        /// <summary>
        /// Max value of the trend
        /// </summary>
        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        /// <summary>
        ///  <see cref="MinValue" />
        /// </summary>
        public static readonly BindableProperty MinValueProperty =
            BindableProperty.Create(nameof(MinValue), typeof(double), typeof(TrendGraph), 0.0, propertyChanged: OnAnyPropertyChanged);

        /// <summary>
        /// Min value of the trend
        /// </summary>
        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        /// <summary>
        ///  <see cref="GraphMargin" />
        /// </summary>
        public static readonly BindableProperty GraphMarginProperty =
            BindableProperty.Create(nameof(GraphMargin), typeof(double), typeof(TrendGraph), 0.5, propertyChanged: OnAnyPropertyChanged);

        /// <summary>
        /// Margin between each item
        /// </summary>
        public double GraphMargin
        {
            get { return (double)GetValue(GraphMarginProperty); }
            set { SetValue(GraphMarginProperty, value); }
        }

        /// <summary>
        ///  <see cref="ItemsSource" />
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(TrendGraph), propertyChanged: OnAnyPropertyChanged);

        /// <summary>
        /// Items used to draw the trends. Use <see cref="ValueMemberPath" /> to expose path to item values
        /// </summary>
        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        ///  <see cref="ValueMemberPath" />
        /// </summary>
        public static readonly BindableProperty ValueMemberPathProperty =
            BindableProperty.Create(nameof(ValueMemberPath), typeof(string), typeof(TrendGraph), string.Empty, propertyChanged: OnAnyPropertyChanged);

        /// <summary>
        /// The property path to use as a value. The value is used to determine the height of the graph.
        /// </summary>
        public string ValueMemberPath
        {
            get { return (string)GetValue(ValueMemberPathProperty); }
            set { SetValue(ValueMemberPathProperty, value); }
        }

        /// <summary>
        ///  <see cref="LowerBound" />
        /// </summary>
        public static readonly BindableProperty LowerBoundProperty =
            BindableProperty.Create(nameof(LowerBound), typeof(double?), typeof(TrendGraph), null, propertyChanged: OnAnyPropertyChanged);

        /// <summary>
        /// Lower bound of the trend values
        /// </summary>
        public double? LowerBound
        {
            get { return (double?)GetValue(LowerBoundProperty); }
            set { SetValue(LowerBoundProperty, value); }
        }

        /// <summary>
        ///  <see cref="UpperBound" />
        /// </summary>
        public static readonly BindableProperty UpperBoundProperty =
            BindableProperty.Create(nameof(UpperBound), typeof(double?), typeof(TrendGraph), null, propertyChanged: OnAnyPropertyChanged);

        /// <summary>
        /// Upper bound of the trend values
        /// </summary>
        public double? UpperBound
        {
            get { return (double?)GetValue(UpperBoundProperty); }
            set { SetValue(UpperBoundProperty, value); }
        }
    }
}
