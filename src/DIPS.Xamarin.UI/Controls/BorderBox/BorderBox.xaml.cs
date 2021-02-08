using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.BorderBox
{
    /// <summary>
    ///     <c>BoxView</c> with the support for Borders
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BorderBox : ContentView
    {
        /// <summary>
        ///     Bindable property for <see cref="FillColor" />
        /// </summary>
        public static readonly BindableProperty FillColorProperty = BindableProperty.Create(
            nameof(FillColor),
            typeof(Color),
            typeof(BorderBox),
            Color.LightGray);

        /// <summary>
        ///     Bindable property for <see cref="BorderColor" />
        /// </summary>
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
            nameof(BorderColor),
            typeof(Color),
            typeof(BorderBox),
            Color.Black);

        /// <summary>
        ///     Bindable property for <see cref="WidthRequest" />
        /// </summary>
        public static new readonly BindableProperty WidthRequestProperty = BindableProperty.Create(
            nameof(WidthRequest),
            typeof(double),
            typeof(BorderBox),
            -1d,
            propertyChanging: OnWidthRequestChanging);

        /// <summary>
        ///     Bindable property for <see cref="HeightRequest" />
        /// </summary>
        public static new readonly BindableProperty HeightRequestProperty = BindableProperty.Create(
            nameof(HeightRequest),
            typeof(double),
            typeof(BorderBox),
            -1d,
            propertyChanging: OnHeightRequestChanging);

        /// <summary>
        ///     Bindable property for <see cref="BorderThickness" />
        /// </summary>
        public static readonly BindableProperty BorderThicknessProperty = BindableProperty.Create(
            nameof(BorderThickness),
            typeof(double),
            typeof(BorderBox),
            0d,
            propertyChanging: OnBorderThicknessChanging);

        /// <summary>
        ///     Bindable property for <see cref="CornerRadius" />
        /// </summary>
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
            nameof(CornerRadius),
            typeof(CornerRadius),
            typeof(BorderBox),
            default(CornerRadius),
            propertyChanging: OnCornerRadiusChanging);

        /// <inheritdoc />
        public BorderBox()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Gets or sets the filling color of the BorderBox. This is a bindable property.
        /// </summary>
        public Color FillColor
        {
            get => (Color)GetValue(FillColorProperty);
            set => SetValue(FillColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the border color of the BorderBox. This is a bindable property.
        /// </summary>
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the desired width override of the BorderBox. This is a bindable property.
        ///     <remarks>Border thickness will be added when rendering the final width of the BorderBox</remarks>
        /// </summary>
        public new double WidthRequest
        {
            get => (double)GetValue(WidthRequestProperty);
            set => SetValue(WidthRequestProperty, value);
        }

        /// <summary>
        ///     Gets or sets the desired height override of the BorderBox. This is a bindable property.
        ///     <remarks>Border thickness will be added when rendering the final height of the BorderBox</remarks>
        /// </summary>
        public new double HeightRequest
        {
            get => (double)GetValue(HeightRequestProperty);
            set => SetValue(HeightRequestProperty, value);
        }

        /// <summary>
        ///     Thickness of the BorderBox border. This is a bindable property.
        /// </summary>
        public double BorderThickness
        {
            get => (double)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        /// <summary>
        ///     Gets or sets the corner radius for the BorderBox. This is a bindable property.
        /// </summary>
        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        private static void OnWidthRequestChanging(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is BorderBox borderBox)
            {
                borderBox.OuterBoxView.WidthRequest = (double)newvalue + (borderBox.BorderThickness * 2);
            }
        }

        private static void OnHeightRequestChanging(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is BorderBox borderBox)
            {
                borderBox.OuterBoxView.HeightRequest = (double)newvalue + (borderBox.BorderThickness * 2);
            }
        }

        private static void OnBorderThicknessChanging(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is BorderBox borderBox)
            {
                borderBox.OuterBoxView.WidthRequest = borderBox.InnerBoxView.WidthRequest + ((double)newvalue * 2);
                borderBox.OuterBoxView.HeightRequest = borderBox.InnerBoxView.HeightRequest + ((double)newvalue * 2);

                var topLeft = borderBox.InnerBoxView.CornerRadius.TopLeft == 0
                    ? 0
                    : borderBox.InnerBoxView.CornerRadius.TopLeft + (double)newvalue;
                var topRight = borderBox.InnerBoxView.CornerRadius.TopRight == 0
                    ? 0
                    : borderBox.InnerBoxView.CornerRadius.TopRight + (double)newvalue;
                var bottomLeft = borderBox.InnerBoxView.CornerRadius.BottomLeft == 0
                    ? 0
                    : borderBox.InnerBoxView.CornerRadius.BottomLeft + (double)newvalue;
                var bottomRight = borderBox.InnerBoxView.CornerRadius.BottomRight == 0
                    ? 0
                    : borderBox.InnerBoxView.CornerRadius.BottomRight + (double)newvalue;
                borderBox.OuterBoxView.CornerRadius = new CornerRadius(topLeft, topRight, bottomLeft, bottomRight);
            }
        }

        private static void OnCornerRadiusChanging(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is BorderBox borderBox)
            {
                var topLeft = ((CornerRadius)newvalue).TopLeft == 0
                    ? 0
                    : ((CornerRadius)newvalue).TopLeft + borderBox.BorderThickness;
                var topRight = ((CornerRadius)newvalue).TopRight == 0
                    ? 0
                    : ((CornerRadius)newvalue).TopRight + borderBox.BorderThickness;
                var bottomLeft = ((CornerRadius)newvalue).BottomLeft == 0
                    ? 0
                    : ((CornerRadius)newvalue).BottomLeft + borderBox.BorderThickness;
                var bottomRight = ((CornerRadius)newvalue).BottomRight == 0
                    ? 0
                    : ((CornerRadius)newvalue).BottomRight + borderBox.BorderThickness;
                borderBox.OuterBoxView.CornerRadius = new CornerRadius(topLeft, topRight, bottomLeft, bottomRight);
            }
        }
    }
}