using System.Diagnostics.CodeAnalysis;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.DatePicker
{
    /// <inheritdoc />
    /// This has extended properties that is not available in the standard Xamarin.Forms.DatePicker
    [ExcludeFromCodeCoverage]
    public class DatePicker : global::Xamarin.Forms.DatePicker
    {
        /// <summary>
        /// Bindale property for <see cref="HasBorder"/>
        /// </summary>
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
