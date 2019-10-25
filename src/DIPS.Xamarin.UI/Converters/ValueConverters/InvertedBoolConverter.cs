using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Converters.ValueConverters
{
    /// <summary>
    /// A converter that accepts a boolean value as a value and inverts the boolean value as output.
    /// </summary>
    public class InvertedBoolConverter : IMarkupExtension, IValueConverter
    {
        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public object ProvideValue(IServiceProvider serviceProvider) => this;

        /// <summary>
        /// Converts a boolean value to the inverted value of the boolean value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type to which to convert the value.</param>
        /// <param name="parameter">A parameter to use during the conversion.</param>
        /// <param name="culture">The culture to use during the conversion.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !bool.TryParse(value.ToString(), out var booleanValue))
            {
                throw new ArgumentException("Value has to be of type boolean");
            }

            return !booleanValue;
        }

        /// <summary>
        /// Converts back from to the original value of <see cref="Convert"/>
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type to which to convert the value.</param>
        /// <param name="parameter">A parameter to use during the conversion.</param>
        /// <param name="culture">The culture to use during the conversion.</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)Convert(value, targetType, parameter, culture);
        }
    }
}
