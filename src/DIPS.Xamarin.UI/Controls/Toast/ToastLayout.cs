using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Toast
{
    /// <summary>
    ///     Toast L that would appear on top of the presented view
    /// </summary>
    public class ToastLayout : BindableObject
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the color which will fill the background of the Toast. This is a bindable property.
        /// </summary>
        public Color BackgroundColor
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
        ///     Gets or sets the font family to which the font for the Toast belongs. This is a bindable property.
        /// </summary>
        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
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
        ///     Gets or sets the LineBreakMode for the Toast. This is a bindable property.
        /// </summary>
        public LineBreakMode LineBreakMode
        {
            get => (LineBreakMode)GetValue(LineBreakModeProperty);
            set => SetValue(LineBreakModeProperty, value);
        }

        /// <summary>
        ///     Gets or sets a flag indicating if the Toast has a shadow displayed. This is a bindable property.
        /// </summary>
        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
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
        ///     Gets or sets the inner padding of the Toast text.
        ///     <remarks>
        ///         The padding is the space between the bounds of a Toast and the bounding region into which its Text property
        ///         should be arranged into.
        ///     </remarks>
        /// </summary>
        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }

        /// <summary>
        ///     Gets or sets the Color for the text of this Toast. This is a bindable property.
        /// </summary>
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        #endregion

        #region Bindable Properties

        /// <summary>
        ///     Bindable property for <see cref="BackgroundColor" />
        /// </summary>
        public static readonly BindableProperty BackgroundColorProperty =
            BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(Toast), Color.Default);

        /// <summary>
        ///     Bindable property for <see cref="CornerRadius" />
        /// </summary>
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius),
            typeof(float), typeof(Toast), -1f,
            validateValue: OnCornerRadiusValidate);

        /// <summary>
        ///     Bindable property for <see cref="FontFamily" />
        /// </summary>
        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(Toast),
                Label.FontFamilyProperty.DefaultValue);

        /// <summary>
        ///     Bindable property for <see cref="FontSize" />
        /// </summary>
        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize), typeof(double), typeof(Toast),
                Label.FontSizeProperty.DefaultValue,
                defaultValueCreator: FontSizeDefaultValueCreator);

        /// <summary>
        ///     Bindable property for <see cref="LineBreakMode" />
        /// </summary>
        public static readonly BindableProperty LineBreakModeProperty = BindableProperty.Create(nameof(LineBreakMode),
            typeof(LineBreakMode), typeof(Toast), Label.LineBreakModeProperty.DefaultValue);

        /// <summary>
        ///     Bindable property for <see cref="HasShadow" />
        /// </summary>
        public static readonly BindableProperty HasShadowProperty =
            BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(Toast), false);

        /// <summary>
        ///     Bindable property for <see cref="MaxLines" />
        /// </summary>
        public static readonly BindableProperty MaxLinesProperty =
            BindableProperty.Create(nameof(MaxLines), typeof(int), typeof(Toast),
                Label.MaxLinesProperty.DefaultValue);

        /// <summary>
        ///     Bindable property for <see cref="Padding" />
        /// </summary>
        public static readonly BindableProperty PaddingProperty =
            BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(Toast), new Thickness(5, 5, 5, 5));

        /// <summary>
        ///     Bindable property for <see cref="TextColor" />
        /// </summary>
        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(Toast),
                Label.TextColorProperty.DefaultValue);

        #endregion

        #region Private Methods

        private static object FontSizeDefaultValueCreator(BindableObject bindable)
        {
            return Device.GetNamedSize(NamedSize.Default, typeof(ToastView));
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