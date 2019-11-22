using System;
using System.Collections;
using Xamarin.Forms;
using DIPS.Xamarin.UI.Extensions;
using System.Collections.Specialized;

namespace DIPS.Xamarin.UI.Controls.TrendGraph
{
    public partial class TrendGraph : ContentView
    {
        private VisualElement? m_previousParent;
        public TrendGraph()
        {
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;
            SizeChanged += (s, e) => StartRedraw(this, EventArgs.Empty);
            InitializeComponent();
        }

        protected override void OnParentSet()
        {
            var parentSize = ((VisualElement)Parent).Width;
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
            if (MinValue >= MaxValue ||
                ItemsSource == null ||
                ItemsSource.Count == 0 ||
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

            var amount = ItemsSource.Count;
            var totalWidth = Width;
            var height = Height;
            var margin = GraphMargin;
            var widthPerItem = totalWidth / (amount + (amount - 1) * margin);
            margin = widthPerItem * GraphMargin;
            var x = 0.0;
            foreach (var item in ItemsSource)
            {
                var itemx = x;
                var itemHeight = CalculateYPosition(item.ExtractDouble(ValueMemberPath, MinValue));
                var backFrame = new BoxView
                {
                    BackgroundColor = GraphBackgroundColor,
                    CornerRadius = 0,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };

                var graph = new BoxView
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = GraphColor,
                    CornerRadius = 0,
                    HeightRequest = itemHeight * height
                };

                graphContainer.Children.Add(backFrame,
                    xConstraint: Constraint.RelativeToParent(r => itemx),
                    yConstraint: Constraint.RelativeToParent(r => 0),
                    widthConstraint: Constraint.RelativeToParent(r => widthPerItem),
                    heightConstraint: Constraint.RelativeToParent(r => r.Height));

                graphContainer.Children.Add(graph,
                    xConstraint: Constraint.RelativeToParent(r => backFrame.X),
                    yConstraint: Constraint.RelativeToParent(r => backFrame.Y + backFrame.Height - (itemHeight * backFrame.Height)),
                    widthConstraint: Constraint.RelativeToParent(r => backFrame.Width),
                    heightConstraint: Constraint.RelativeToParent(r => itemHeight * backFrame.Height));
                x += widthPerItem + margin;
            }
        }

        private void CollectionChanged_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            StartRedraw(this, EventArgs.Empty);
        }

        private double CalculateYPosition(double value)
        {
            if (value >= MaxValue) return 1.0;
            if (value <= MinValue) return 0.0;
            var totalDiff = MaxValue - MinValue;
            var valDiff = value - MinValue;
            return valDiff / totalDiff;
        }

        private static void OnAnyPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is TrendGraph trendGraph)) return;
            trendGraph.StartRedraw(trendGraph, EventArgs.Empty);
        }

        /// <summary>
        ///  <see cref="GraphColor" />
        /// </summary>
        public static readonly BindableProperty GraphColorProperty =
            BindableProperty.Create(nameof(GraphColor), typeof(Color), typeof(TrendGraph), Color.FromHex("#a26eba"), propertyChanged: OnAnyPropertyChanged);

        /// <summary>
        /// Color of the filled graph
        /// </summary>
        public Color GraphColor
        {
            get { return (Color)GetValue(GraphColorProperty); }
            set { SetValue(GraphColorProperty, value); }
        }

        /// <summary>
        ///  <see cref="GraphBackgroundColor" />
        /// </summary>
        public static readonly BindableProperty GraphBackgroundColorProperty =
            BindableProperty.Create(nameof(GraphBackgroundColor), typeof(Color), typeof(TrendGraph), Color.FromHex("#edf3f4"), propertyChanged: OnAnyPropertyChanged);

        /// <summary>
        /// Color behind the graph
        /// </summary>
        public Color GraphBackgroundColor
        {
            get { return (Color)GetValue(GraphBackgroundColorProperty); }
            set { SetValue(GraphBackgroundColorProperty, value); }
        }

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
        /// To expose path to item values. Set this if the items are not of type int, double or float.
        /// </summary>
        public string ValueMemberPath
        {
            get { return (string)GetValue(ValueMemberPathProperty); }
            set { SetValue(ValueMemberPathProperty, value); }
        }


    }
}
