using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls
{
    public class DatePicker : global::Xamarin.Forms.DatePicker
    {
        public static readonly BindableProperty HasBorderProperty = BindableProperty.Create(
            nameof(HasBorder),
            typeof(bool),
            typeof(DatePicker),
            true);

        /// <summary>
        ///     Gets or sets a value that determines whether the datepicker should have it's native borders. This is a
        ///     bindable property.
        /// </summary>
        public bool HasBorder
        {
            get => (bool)GetValue(HasBorderProperty);
            set => SetValue(HasBorderProperty, value);
        }
    }
}
