using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static DIPS.Xamarin.UI.Library;

namespace DIPS.Xamarin.UI.Controls.Skeleton
{
    /// <summary>
    /// Skeleton view to show skeleton structure when data is loading.
    /// </summary>
    [ContentProperty(nameof(MainContent))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SkeletonView : ContentView
    {
        private const string AnimationName = "SkeletonBounce";
        private Grid m_skeletongrid;
        private Grid? m_skeletonLayout;
        private List<BoxView> m_skeletons = new List<BoxView>();
        private View? m_mainContent;

        /// <summary>
        /// Creates a new instance of skeleton view
        /// </summary>
        public SkeletonView()
        {
            if (!PreviewFeatures.SkeletonView)
            {
                throw new Exception("Enable SkeletonView by using DIPS.Xamarin.UI.Library.PreviewFeatures.EnableFeature(\"SkeletonView\");");
            }
            Content = m_skeletongrid = new Grid();
            InitializeComponent();
            BindingContextChanged += SkeletonView_BindingContextChanged;
        }

        private void SkeletonView_BindingContextChanged(object sender, EventArgs e)
        {
            if (MainContent == null)
                return;
            MainContent.BindingContext = this.BindingContext;
            OnLoadingChanged();
        }

        private async void OnLoadingChanged()
        {
            if (MainContent == null)
                return;

            if (!m_skeletongrid.Children.Contains(MainContent))
            {
                m_skeletongrid.Children.Add(MainContent);
                MainContent.Opacity = 0;
            }

            if (!IsLoading)
            {
                if (m_skeletonLayout != null)
                {
                    _ = m_skeletonLayout.FadeTo(0, FadeTime);
                    StopAnimation();
                }
                await MainContent.FadeTo(1.0, FadeTime * 2);
            }
            else
            {
                if (m_skeletonLayout == null)
                {
                    m_skeletonLayout = CreateSkeleton();
                    m_skeletonLayout.Opacity = 0.0;
                    m_skeletongrid.Children.Add(m_skeletonLayout);
                }

                _ = MainContent.FadeTo(0.0, FadeTime);
                _ = m_skeletonLayout.FadeTo(1.0, FadeTime);
                StartAnimation();
            }
        }

        private Grid CreateSkeleton()
        {
            if (m_skeletonLayout != null)
                return m_skeletonLayout;
            if (Shapes == null || Shapes.Count == 0)
            {
                Shapes = new List<SkeletonShape> { new SkeletonShape() };
            }

            var grid = new Grid();
            foreach (var shape in Shapes)
            {
                var box = CreateBox(shape);
                grid.Children.Add(box);
                m_skeletons.Add(box);
            }
            var maxRow = Shapes.Max(s => s.Row + s.RowSpan);
            var maxCol = Shapes.Max(s => s.Column + s.ColumnSpan);
            for (var i = 0; i < maxRow; i++)
            {
                var shape = Shapes.FirstOrDefault(s => s.Row == i && s.Height > -1);
                if (shape != null)
                    grid.RowDefinitions.Add(new RowDefinition { Height = shape.Height });
                else
                    grid.RowDefinitions.Add(new RowDefinition());
            }

            for (var i = 0; i < maxCol; i++)
            {
                var shape = Shapes.FirstOrDefault(s => s.Column == i && s.Width > -1);
                if (shape != null)
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = shape.Width });
                else
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            return grid;
        }

        private BoxView CreateBox(SkeletonShape shape)
        {
            var box = new BoxView()
            {
                HorizontalOptions = shape.HorizontalAlignment,
                VerticalOptions = shape.VerticalAlignment,
                CornerRadius = shape.CornerRadius,
                Margin = new Thickness(shape.Margin),
                Color = SkeletonColor,
            };

            Grid.SetRow(box, shape.Row);
            Grid.SetColumn(box, shape.Column);
            Grid.SetRowSpan(box, shape.RowSpan);
            Grid.SetColumnSpan(box, shape.ColumnSpan);
            if (shape.Height > -1)
            {
                box.HeightRequest = shape.Height;
            }
            if (shape.Width > -1)
            {
                box.WidthRequest = shape.Width;
            }
            return box;
        }

        private void StartAnimation()
        {
            StopAnimation();
            var animation = new Animation
            {
                {0.0, 0.5, new Animation(a => { foreach(var box in m_skeletons) box.Scale = a; }, 0.99, 1.01, Easing.BounceOut) },
                {0.5, 1.0, new Animation(a => { foreach(var box in m_skeletons) box.Scale = a; }, 1.01, 0.99, Easing.BounceOut) },
            };
            animation.Commit(this, AnimationName, 16, 1000, Easing.BounceOut, (a, c) => { }, () => IsLoading);
        }

        private void StopAnimation() => this.AbortAnimation(AnimationName);

        /// <summary>
        /// Time used to fade inn and out content
        /// </summary>
        public uint FadeTime { get; set; } = 400;

        /// <summary>
        /// Color used on skeletons. Defaults to LightGray
        /// </summary>
        public Color SkeletonColor { get; set; } = Color.LightGray;

        /// <summary>
        /// Content shown when loading is done
        /// </summary>
        public View? MainContent
        {
            get => m_mainContent;
            set
            {
                m_mainContent = value;
                OnLoadingChanged();
            }
        }

        /// <summary>
        /// Shapes used when content is loading
        /// </summary>
        public List<SkeletonShape> Shapes { get; set; } = new List<SkeletonShape>();

        /// <summary>
        ///  <see cref="SelectorItem" />
        /// </summary>
        public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(
            nameof(IsLoading),
            typeof(bool),
            typeof(SkeletonView),
            false,
            BindingMode.OneWay,
            propertyChanged: (s, o, n) => ((SkeletonView)s).OnLoadingChanged());

        /// <summary>
        /// Used to select the template in the selector
        /// </summary>
        public bool IsLoading
        {
            get => (bool)GetValue(IsLoadingProperty);
            set => SetValue(IsLoadingProperty, value);
        }
    }
}
