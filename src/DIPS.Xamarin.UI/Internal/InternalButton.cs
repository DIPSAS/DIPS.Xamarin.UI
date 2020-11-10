using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Internal
{
    internal class InternalButton : Button
    {
        /// <summary>
        /// Bindable property for horizontal text alignment of a button.
        /// </summary>
        public static BindableProperty HorizontalTextAlignmentProperty =
            BindableProperty.Create(nameof(HorizontalTextAlignment),
                typeof(TextAlignment),
                typeof(InternalButton),
                TextAlignment.Center);

        /// <summary>
        /// Bindable property for vertical text alignment of a button.
        /// </summary>
        public static BindableProperty VerticalTextAlignmentProperty =
            BindableProperty.Create(nameof(VerticalTextAlignment),
                typeof(TextAlignment),
                typeof(InternalButton),
                TextAlignment.Center);

        /// <summary>
        /// Gets or sets the horizontal text alignment.
        /// </summary>
        public TextAlignment HorizontalTextAlignment
        {
            get => (TextAlignment)GetValue(HorizontalTextAlignmentProperty);
            set => SetValue(HorizontalTextAlignmentProperty, value);
        }

        /// <summary>
        /// Gets or sets the vertical text alignment.
        /// </summary>
        public TextAlignment VerticalTextAlignment
        {
            get => (TextAlignment)GetValue(VerticalTextAlignmentProperty);
            set => SetValue(VerticalTextAlignmentProperty, value);
        }
    }
}