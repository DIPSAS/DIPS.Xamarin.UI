using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Tag
{
    public partial class Tag
    {
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
    }
}