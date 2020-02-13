using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Tag
{
    /// <summary>
    /// A <c>Frame</c> wrapping a <c>Label</c> to allow setting border radius for the <c>Label</c>.
    /// </summary>
    public partial class Tag : Frame
    {
        /// <inheritdoc />
        public Tag()
        {
            Initialize();
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
            label.SetBinding(Label.VerticalTextAlignmentProperty, new Binding(nameof(VerticalTextAlignment), source: this));
            label.SetBinding(Label.HorizontalTextAlignmentProperty, new Binding(nameof(HorizontalTextAlignment), source: this));

            Content = label;
        }
    }
}