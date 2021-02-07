using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.BorderBox
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BorderBox : ContentView
    {
        public static readonly BindableProperty FillColorProperty = BindableProperty.Create(
            nameof(FillColor),
            typeof(Color),
            typeof(BorderBox),
            Color.FromHex("D9C0C0C0"));

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
            nameof(BorderColor),
            typeof(Color),
            typeof(BorderBox),
            Color.Black);

        public static new readonly BindableProperty WidthRequestProperty = BindableProperty.Create(
            nameof(WidthRequest),
            typeof(double),
            typeof(BorderBox),
            -1d,
            propertyChanging: OnWidthRequestChanging);

        public static new readonly BindableProperty HeightRequestProperty = BindableProperty.Create(
            nameof(HeightRequest),
            typeof(double),
            typeof(BorderBox),
            -1d,
            propertyChanging: OnHeightRequestChanging);

        public static readonly BindableProperty BorderThicknessProperty = BindableProperty.Create(
            nameof(BorderThickness),
            typeof(double),
            typeof(BorderBox),
            0d,
            propertyChanging: OnBorderThicknessChanging);

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
            nameof(CornerRadius),
            typeof(CornerRadius),
            typeof(BorderBox),
            default(CornerRadius),
            propertyChanging: OnCornerRadiusChanging);

        public BorderBox()
        {
            InitializeComponent();
        }

        public Color FillColor
        {
            get => (Color)GetValue(FillColorProperty);
            set => SetValue(FillColorProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public new double WidthRequest
        {
            get => (double)GetValue(WidthRequestProperty);
            set => SetValue(WidthRequestProperty, value);
        }

        public new double HeightRequest
        {
            get => (double)GetValue(HeightRequestProperty);
            set => SetValue(HeightRequestProperty, value);
        }

        public double BorderThickness
        {
            get => (double)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        private static void OnWidthRequestChanging(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is BorderBox borderBox)
            {
                borderBox.containerView.WidthRequest = (double)newvalue + (borderBox.BorderThickness * 2);
                borderBox.outerBoxView.WidthRequest = (double)newvalue + (borderBox.BorderThickness * 2);
            }
        }

        private static void OnHeightRequestChanging(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is BorderBox borderBox)
            {
                borderBox.containerView.HeightRequest = (double)newvalue + (borderBox.BorderThickness * 2);
                borderBox.outerBoxView.HeightRequest = (double)newvalue + (borderBox.BorderThickness * 2);
            }
        }

        private static void OnBorderThicknessChanging(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is BorderBox borderBox)
            {
                borderBox.containerView.WidthRequest = borderBox.innerBoxView.WidthRequest + ((double)newvalue * 2);
                borderBox.outerBoxView.WidthRequest = borderBox.innerBoxView.WidthRequest + ((double)newvalue * 2);
                borderBox.containerView.HeightRequest = borderBox.innerBoxView.HeightRequest + ((double)newvalue * 2);
                borderBox.outerBoxView.HeightRequest = borderBox.innerBoxView.HeightRequest + ((double)newvalue * 2);

                var topLeft = borderBox.innerBoxView.CornerRadius.TopLeft == 0
                    ? 0
                    : borderBox.innerBoxView.CornerRadius.TopLeft + (double)newvalue;
                var topRight = borderBox.innerBoxView.CornerRadius.TopRight == 0
                    ? 0
                    : borderBox.innerBoxView.CornerRadius.TopRight + (double)newvalue;
                var bottomLeft = borderBox.innerBoxView.CornerRadius.BottomLeft == 0
                    ? 0
                    : borderBox.innerBoxView.CornerRadius.BottomLeft + (double)newvalue;
                var bottomRight = borderBox.innerBoxView.CornerRadius.BottomRight == 0
                    ? 0
                    : borderBox.innerBoxView.CornerRadius.BottomRight + (double)newvalue;
                borderBox.outerBoxView.CornerRadius = new CornerRadius(topLeft, topRight, bottomLeft, bottomRight);
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
                borderBox.outerBoxView.CornerRadius = new CornerRadius(topLeft, topRight, bottomLeft, bottomRight);
            }
        }
    }
}