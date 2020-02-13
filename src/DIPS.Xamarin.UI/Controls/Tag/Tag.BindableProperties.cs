using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Tag
{
    public partial class Tag
    {
        public static readonly BindableProperty TextProperty = Label.TextProperty;

        public static readonly BindableProperty TextColorProperty = Label.TextColorProperty;

        public static readonly BindableProperty FontFamilyProperty = Label.FontFamilyProperty;

        public static readonly BindableProperty FontAttributesProperty = Label.FontAttributesProperty;

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(Tag), -1.0);

        public static readonly BindableProperty LineBreakModeProperty = Label.LineBreakModeProperty;

        public static readonly BindableProperty MaxLinesProperty = Label.MaxLinesProperty;

        public static readonly BindableProperty FormattedTextProperty = Label.FormattedTextProperty;

        public static readonly BindableProperty CharacterSpacingProperty = Label.CharacterSpacingProperty;

        public static readonly BindableProperty VerticalTextAlignmentProperty = Label.VerticalTextAlignmentProperty;

        public static readonly BindableProperty HorizontalTextAlignmentProperty = Label.HorizontalTextAlignmentProperty;
    }
}