using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Tag
{
    public partial class Tag
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
    }
}