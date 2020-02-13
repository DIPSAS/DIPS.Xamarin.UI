using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Tag
{
    /// <summary>
    ///     A <c>Frame</c> wrapping a <c>Label</c> to allow setting border radius for the <c>Label</c>.
    /// </summary>
    public class Tag : Frame
    {
        /// <summary>
        ///     Bindable property for <see cref="Text" />
        /// </summary>
        public static readonly BindableProperty TextProperty = Label.TextProperty;

        /// <summary>
        ///     Bindable property for <see cref="TextColor" />
        /// </summary>
        public static readonly BindableProperty TextColorProperty = Label.TextColorProperty;

        /// <summary>
        ///     Bindable property for <see cref="FontFamily" />
        /// </summary>
        public static readonly BindableProperty FontFamilyProperty = Label.FontFamilyProperty;

        /// <summary>
        ///     Bindable property for <see cref="FontAttributes" />
        /// </summary>
        public static readonly BindableProperty FontAttributesProperty = Label.FontAttributesProperty;

        /// <summary>
        ///     Bindable property for <see cref="FontSize" />
        /// </summary>
        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize), typeof(double), typeof(Tag), -1.0);

        /// <summary>
        ///     Bindable property for <see cref="LineBreakMode" />
        /// </summary>
        public static readonly BindableProperty LineBreakModeProperty = Label.LineBreakModeProperty;

        /// <summary>
        ///     Bindable property for <see cref="MaxLines" />
        /// </summary>
        public static readonly BindableProperty MaxLinesProperty = Label.MaxLinesProperty;

        /// <summary>
        ///     Bindable property for <see cref="FormattedText" />
        /// </summary>
        public static readonly BindableProperty FormattedTextProperty = Label.FormattedTextProperty;

        /// <summary>
        ///     Bindable property for <see cref="CharacterSpacing" />
        /// </summary>
        public static readonly BindableProperty CharacterSpacingProperty = Label.CharacterSpacingProperty;

        /// <summary>
        ///     Bindable property for <see cref="VerticalTextAlignment" />
        /// </summary>
        public static readonly BindableProperty VerticalTextAlignmentProperty = Label.VerticalTextAlignmentProperty;

        /// <summary>
        ///     Bindable property for <see cref="HorizontalTextAlignment" />
        /// </summary>
        public static readonly BindableProperty HorizontalTextAlignmentProperty = Label.HorizontalTextAlignmentProperty;

        /// <inheritdoc />
        public Tag()
        {
            Initialize();
        }

        /// <summary>
        ///     Gets or sets the text for the Tag. This is a bindable property.
        ///     Setting Text to a non-null value will set the FormattedText property to null.
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
        ///     Setting FormattedText to a non-null value will set the Text property to null.
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

        private void Initialize()
        {
            HasShadow = false;

            var label = new Label
            {
                BackgroundColor = Color.Transparent,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            label.SetBinding(Label.TextProperty, new Binding(nameof(Text), source: this));
            label.SetBinding(Label.TextColorProperty, new Binding(nameof(TextColor), source: this));
            label.SetBinding(Label.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
            label.SetBinding(Label.FontAttributesProperty, new Binding(nameof(FontAttributes), source: this));
            label.SetBinding(Label.FontSizeProperty, new Binding(nameof(FontSize), source: this));
            label.SetBinding(Label.LineBreakModeProperty, new Binding(nameof(LineBreakMode), source: this));
            label.SetBinding(Label.MaxLinesProperty, new Binding(nameof(MaxLines), source: this));
            label.SetBinding(Label.FormattedTextProperty, new Binding(nameof(FormattedText), source: this));
            label.SetBinding(Label.CharacterSpacingProperty, new Binding(nameof(CharacterSpacing), source: this));
            label.SetBinding(Label.VerticalTextAlignmentProperty,
                new Binding(nameof(VerticalTextAlignment), source: this));
            label.SetBinding(Label.HorizontalTextAlignmentProperty,
                new Binding(nameof(HorizontalTextAlignment), source: this));

            Content = label;
        }
    }
}