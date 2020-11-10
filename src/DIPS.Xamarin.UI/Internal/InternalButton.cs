using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Internal
{
    internal class InternalButton : Button
    {
        public static BindableProperty HorizontalTextAlignmentProperty =
            BindableProperty.Create(nameof(HorizontalTextAlignment),
                typeof(TextAlignment),
                typeof(InternalButton),
                TextAlignment.Center);

        public static BindableProperty VerticalTextAlignmentProperty =
            BindableProperty.Create(nameof(VerticalTextAlignment),
                typeof(TextAlignment),
                typeof(InternalButton),
                TextAlignment.Center);

        public TextAlignment HorizontalTextAlignment
        {
            get => (TextAlignment)GetValue(HorizontalTextAlignmentProperty);
            set => SetValue(HorizontalTextAlignmentProperty, value);
        }

        public TextAlignment VerticalTextAlignment
        {
            get => (TextAlignment)GetValue(VerticalTextAlignmentProperty);
            set => SetValue(VerticalTextAlignmentProperty, value);
        }
    }
}