using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.Tag
{
    /// <summary>
    ///     A <c>Frame</c> wrapping a <c>Label</c> to allow setting border radius for the <c>Label</c>.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tag
    {
        /// <inheritdoc />
        public Tag()
        {
            InitializeComponent();
        }

        #region Label Properties

        /// <summary>
        ///     Bindable property for <see cref="Text" />
        /// </summary>
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(Tag), default(string));

        /// <summary>
        ///     Bindable property for <see cref="TextColor" />
        /// </summary>
        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(Tag), Color.Default);

        /// <summary>
        ///     Bindable property for <see cref="FontFamily" />
        /// </summary>
        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(Tag), default(string));

        /// <summary>
        ///     Bindable property for <see cref="FontAttributes" />
        /// </summary>
        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes),
            typeof(FontAttributes), typeof(Tag), FontAttributes.None);

        /// <summary>
        ///     Bindable property for <see cref="FontSize" />
        /// </summary>
        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize), typeof(double), typeof(Tag), -1d);

        /// <summary>
        ///     Bindable property for <see cref="LineBreakMode" />
        /// </summary>
        public static readonly BindableProperty LineBreakModeProperty = BindableProperty.Create(nameof(LineBreakMode),
            typeof(LineBreakMode), typeof(Tag), LineBreakMode.WordWrap);

        /// <summary>
        ///     Bindable property for <see cref="MaxLines" />
        /// </summary>
        public static readonly BindableProperty MaxLinesProperty =
            BindableProperty.Create(nameof(MaxLines), typeof(int), typeof(Label), -1);

        /// <summary>
        ///     Bindable property for <see cref="FormattedText" />
        /// </summary>
        public static readonly BindableProperty FormattedTextProperty =
            BindableProperty.Create(nameof(FormattedText), typeof(FormattedString), typeof(Label),
                default(FormattedString),
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    if (oldValue != null)
                    {
                        var formattedString = (FormattedString) oldValue;
                        var tag = (Tag) bindable;

                        formattedString.Parent = null;
                        tag.tagLabel.FormattedText = null;
                    }
                }, propertyChanged: (bindable, oldValue, newValue) =>
                {
                    if (newValue != null)
                    {
                        var tag = (Tag) bindable;
                        var formattedString = (FormattedString) newValue;

                        formattedString.Parent = tag;
                        tag.tagLabel.FormattedText = formattedString;
                        tag.Text = null;
                    }
                });

        /// <summary>
        ///     Bindable property for <see cref="CharacterSpacing" />
        /// </summary>
        public static readonly BindableProperty CharacterSpacingProperty =
            BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(Tag), 0d);

        /// <summary>
        ///     Bindable property for <see cref="VerticalTextAlignment" />
        /// </summary>
        public static readonly BindableProperty VerticalTextAlignmentProperty =
            BindableProperty.Create(nameof(VerticalTextAlignment), typeof(TextAlignment), typeof(Tag),
                TextAlignment.Center);

        /// <summary>
        ///     Bindable property for <see cref="HorizontalTextAlignment" />
        /// </summary>
        public static readonly BindableProperty HorizontalTextAlignmentProperty =
            BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(Tag),
                TextAlignment.Center);

        /// <summary>
        ///     Gets or sets the text for the Tag. This is a bindable property.
        ///     <remarks>Setting Text to a non-null value will set the FormattedText property to null.</remarks>
        /// </summary>
        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        ///     Gets or sets the Color for the text of this Tag. This is a bindable property.
        /// </summary>
        public Color TextColor
        {
            get => (Color) GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the font family to which the font for the Tag belongs. This is a bindable property.
        /// </summary>
        public string FontFamily
        {
            get => (string) GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        /// <summary>
        ///     Gets or sets the value that indicates whether the font for the Tag is bold, italic, or neither. This is a bindable
        ///     property.
        /// </summary>
        public FontAttributes FontAttributes
        {
            get => (FontAttributes) GetValue(FontAttributesProperty);
            set => SetValue(FontAttributesProperty, value);
        }

        /// <summary>
        ///     Gets or sets the size of the font for the Tag. This is a bindable property.
        /// </summary>
        public double FontSize
        {
            get => (double) GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        /// <summary>
        ///     Gets or sets the LineBreakMode for the Tag. This is a bindable property.
        /// </summary>
        public LineBreakMode LineBreakMode
        {
            get => (LineBreakMode) GetValue(LineBreakModeProperty);
            set => SetValue(LineBreakModeProperty, value);
        }

        /// <summary>
        ///     Gets or sets the maximum number of lines allowed in the Tag.
        /// </summary>
        public int MaxLines
        {
            get => (int) GetValue(MaxLinesProperty);
            set => SetValue(MaxLinesProperty, value);
        }

        /// <summary>
        ///     Gets or sets the formatted text for the Tag. This is a bindable property.
        ///     <remarks>Setting FormattedText to a non-null value will set the Text property to null.</remarks>
        /// </summary>
        public FormattedString FormattedText
        {
            get => (FormattedString) GetValue(FormattedTextProperty);
            set => SetValue(FormattedTextProperty, value);
        }

        /// <summary>
        ///     Gets or sets the character spacing of the text in the Tag. This is a bindable property.
        /// </summary>
        public double CharacterSpacing
        {
            get => (double) GetValue(CharacterSpacingProperty);
            set => SetValue(CharacterSpacingProperty, value);
        }

        /// <summary>
        ///     Gets or sets the vertical alignment of the Text property. This is a bindable property.
        /// </summary>
        public TextAlignment VerticalTextAlignment
        {
            get => (TextAlignment) GetValue(VerticalTextAlignmentProperty);
            set => SetValue(VerticalTextAlignmentProperty, value);
        }

        /// <summary>
        ///     Gets or sets the horizontal alignment of the Text property. This is a bindable property.
        /// </summary>
        public TextAlignment HorizontalTextAlignment
        {
            get => (TextAlignment) GetValue(HorizontalTextAlignmentProperty);
            set => SetValue(HorizontalTextAlignmentProperty, value);
        }

        #endregion

        #region Frame Properties

        /// <summary>
        ///     Bindable property for <see cref="BackgroundColor" />
        /// </summary>
        public new static readonly BindableProperty BackgroundColorProperty =
            BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(Tag), Color.Default);

        /// <summary>
        ///     Bindable property for <see cref="BorderColor" />
        /// </summary>
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(Tag), Color.Default);

        /// <summary>
        ///     Bindable property for <see cref="CornerRadius" />
        /// </summary>
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius),
            typeof(float), typeof(Tag), -1f,
            validateValue: (bindable, value) =>
            {
                if (value is float f) return (int) f == -1 || f >= 0f;
                return false;
            });

        /// <summary>
        ///     Bindable property for <see cref="HasShadow" />
        /// </summary>
        public static readonly BindableProperty HasShadowProperty =
            BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(Tag), false);

        /// <summary>
        ///     Bindable property for <see cref="Padding" />
        /// </summary>
        public new static readonly BindableProperty PaddingProperty =
            BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(Tag), new Thickness(5, 5, 5, 5));

        /// <summary>
        ///     Gets or sets the color which will fill the background of a Tag. This is a bindable property.
        /// </summary>
        public new Color BackgroundColor
        {
            get => (Color) GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the border color for the Tag. This is a bindable property.
        /// </summary>
        public Color BorderColor
        {
            get => (Color) GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the corner radius of the Tag. This is a bindable property.
        /// </summary>
        public float CornerRadius
        {
            get => (float) GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        ///     Gets or sets a flag indicating if the Tag has a shadow displayed. This is a bindable property.
        /// </summary>
        public bool HasShadow
        {
            get => (bool) GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }

        /// <summary>
        ///     Gets or sets the inner padding of the Tag text.
        ///     <remarks>
        ///         The padding is the space between the bounds of a Tag and the bounding region into which its Text property
        ///         should be arranged into.
        ///     </remarks>
        /// </summary>
        public new Thickness Padding
        {
            get => (Thickness) GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }

        #endregion
    }
}