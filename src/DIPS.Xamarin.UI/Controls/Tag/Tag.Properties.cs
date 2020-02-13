using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Tag
{
    public partial class Tag
    {
        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public Color TextColor
        {
            get => (Color) GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public string FontFamily
        {
            get => (string) GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public FontAttributes FontAttributes
        {
            get => (FontAttributes) GetValue(FontAttributesProperty);
            set => SetValue(FontAttributesProperty, value);
        }

        public double FontSize
        {
            get => (double) GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public LineBreakMode LineBreakMode
        {
            get => (LineBreakMode) GetValue(LineBreakModeProperty);
            set => SetValue(LineBreakModeProperty, value);
        }

        public int MaxLines
        {
            get => (int) GetValue(MaxLinesProperty);
            set => SetValue(MaxLinesProperty, value);
        }

        public FormattedString FormattedText
        {
            get => (FormattedString) GetValue(FormattedTextProperty);
            set => SetValue(FormattedTextProperty, value);
        }

        public double CharacterSpacing
        {
            get => (double) GetValue(CharacterSpacingProperty);
            set => SetValue(CharacterSpacingProperty, value);
        }

        public TextAlignment VerticalTextAlignment
        {
            get => (TextAlignment) GetValue(VerticalTextAlignmentProperty);
            set => SetValue(VerticalTextAlignmentProperty, value);
        }

        public TextAlignment HorizontalTextAlignment
        {
            get => (TextAlignment) GetValue(HorizontalTextAlignmentProperty);
            set => SetValue(HorizontalTextAlignmentProperty, value);
        }
    }
}