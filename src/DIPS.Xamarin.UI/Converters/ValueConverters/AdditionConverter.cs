using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Converters.ValueConverters
{
    /// <summary>
    /// Adds the provided value (a term) with a <see cref="Addend"/> to create a sum
    /// </summary>
    public class AdditionConverter : IMarkupExtension, IValueConverter
    {
        /// <inheritdoc />
        public object ProvideValue(IServiceProvider serviceProvider) => this;

        /// <summary>
        /// A number which is added to the provided value
        /// </summary>
        public double? Addend { get; set; }

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentException("Value is null");
            if (Addend == null)
                throw new ArgumentException("Addend is null, it has to be a double");
            if (!double.TryParse(value.ToString(), out var term))
                throw new ArgumentException("Value is not a number");
            return term + Addend;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
