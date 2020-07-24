using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.Toast
{
    /// <summary>
    ///     Toaster control that would appear on top of the presented view
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToastView : ContentView
    {
        /// <inheritdoc />
        public ToastView()
        {
            InitializeComponent();
        }

        #region Bindable Properties

        /// <summary>
        ///     Bindable property for <see cref="Text" />
        /// </summary>
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(ToastView), Label.TextProperty.DefaultValue);

        /// <summary>
        ///     Bindable property for <see cref="TextColor" />
        /// </summary>
        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(ToastView),
                Label.TextColorProperty.DefaultValue);

        /// <summary>
        ///     Bindable property for <see cref="FontFamily" />
        /// </summary>
        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(ToastView),
                Label.FontFamilyProperty.DefaultValue);

        /// <summary>
        ///     Bindable property for <see cref="FontSize" />
        /// </summary>
        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize), typeof(double), typeof(ToastView),
                Label.FontSizeProperty.DefaultValue,
                defaultValueCreator: FontSizeDefaultValueCreator);

        /// <summary>
        ///     Bindable property for <see cref="LineBreakMode" />
        /// </summary>
        public static readonly BindableProperty LineBreakModeProperty = BindableProperty.Create(nameof(LineBreakMode),
            typeof(LineBreakMode), typeof(ToastView), Label.LineBreakModeProperty.DefaultValue);

        /// <summary>
        ///     Bindable property for <see cref="MaxLines" />
        /// </summary>
        public static readonly BindableProperty MaxLinesProperty =
            BindableProperty.Create(nameof(MaxLines), typeof(int), typeof(ToastView),
                Label.MaxLinesProperty.DefaultValue);

        /// <summary>
        ///     Bindable property for <see cref="BackgroundColor" />
        /// </summary>
        public static new readonly BindableProperty BackgroundColorProperty =
            BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(ToastView), Color.Default);

        /// <summary>
        ///     Bindable property for <see cref="CornerRadius" />
        /// </summary>
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius),
            typeof(float), typeof(ToastView), -1f,
            validateValue: OnCornerRadiusValidate);

        /// <summary>
        ///     Bindable property for <see cref="Padding" />
        /// </summary>
        public static new readonly BindableProperty PaddingProperty =
            BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(ToastView), new Thickness(5, 5, 5, 5));

        /// <summary>
        ///     Bindable property for <see cref="PositionY" />
        /// </summary>
        public static readonly BindableProperty PositionYProperty =
            BindableProperty.Create(nameof(PositionY), typeof(double), typeof(ToastView), 10d);

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the text for the Toast. This is a bindable property.
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        ///     Gets or sets the size of the font for the Toast. This is a bindable property.
        /// </summary>
        [TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        /// <summary>
        ///     Gets or sets the maximum number of lines allowed in the Toast. This is a bindable property.
        /// </summary>
        public int MaxLines
        {
            get => (int)GetValue(MaxLinesProperty);
            set => SetValue(MaxLinesProperty, value);
        }

        /// <summary>
        ///     Gets or sets the LineBreakMode for the Toast. This is a bindable property.
        /// </summary>
        public LineBreakMode LineBreakMode
        {
            get => (LineBreakMode)GetValue(LineBreakModeProperty);
            set => SetValue(LineBreakModeProperty, value);
        }

        /// <summary>
        ///     Gets or sets the font family to which the font for the Toast belongs. This is a bindable property.
        /// </summary>
        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        /// <summary>
        ///     Gets or sets the Color for the text of this Toast. This is a bindable property.
        /// </summary>
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the color which will fill the background of the Toast. This is a bindable property.
        /// </summary>
        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the corner radius of the Toast. This is a bindable property.
        /// </summary>
        public float CornerRadius
        {
            get => (float)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        ///     Gets or sets the inner padding of the Toast text.
        ///     <remarks>
        ///         The padding is the space between the bounds of a Toast and the bounding region into which its Text property
        ///         should be arranged into.
        ///     </remarks>
        /// </summary>
        public new Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }

        /// <summary>
        ///     The vertical positioning of the toaster in a percentage of the Main Page relative to the top margin of the Main
        ///     page. This is a bindable property.
        /// </summary>
        public double PositionY
        {
            get => (double)GetValue(PositionYProperty);
            set => SetValue(PositionYProperty, value);
        }

        private static object FontSizeDefaultValueCreator(BindableObject bindable)
        {
            return Device.GetNamedSize(NamedSize.Default, (ToastView)bindable);
        }

        private static bool OnCornerRadiusValidate(BindableObject bindable, object value)
        {
            if (value is float f)
            {
                return (int)f == -1 || f >= 0f;
            }

            return false;
        }

        #endregion
    }
}