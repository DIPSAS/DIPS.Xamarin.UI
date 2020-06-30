using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Converters.ValueConverters
{
    /// <summary>
    /// Converter that takes different input input types and returns a boolean value to indicate if it is empty or not.
    /// </summary>
    public class IsEmptyConverter : IValueConverter, IMarkupExtension
    {
        private IServiceProvider m_serviceProvider;

        /// <summary>
        /// Property to set if we want to return a inverted output value from the converter.
        /// </summary>
        public bool Inverted { get; set; }
        /// <summary>
        /// Checks if the input value is empty and returns a boolean value to indicate if it is.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type to which to convert the value.</param>
        /// <param name="parameter">A parameter to use during the conversion.</param>
        /// <param name="culture">The culture to use during the conversion.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = false;
            switch (value) {
                case null:
                    result = true;
                    break;
                case int intValue:
                    result = intValue == 0;
                    break;
                case double doubleValue:
                    result = doubleValue == 0.0;
                    break;
                case float floatValue:
                    result = floatValue == 0.0f;
                    break;
                case string stringValue:
                    result = string.IsNullOrEmpty(stringValue);
                    break;
                case IList listValue:
                    result = listValue.Count == 0;
                    break;
            }

            return !Inverted ? result : !result;
        }

        /// <summary>
        /// Converts back from <see cref="Convert"/> and returns a inverted value
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type to which to convert the value.</param>
        /// <param name="parameter">A parameter to use during the conversion.</param>
        /// <param name="culture">The culture to use during the conversion.</param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)Convert(value, targetType, parameter, culture);
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            m_serviceProvider = serviceProvider;
            return this;
        }
    }
}