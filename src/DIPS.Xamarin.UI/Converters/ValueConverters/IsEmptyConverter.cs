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
    /// <remarks>Input types: <see cref="string"/>, <see cref="IEnumerable"/></remarks>
    /// </summary>
    public class IsEmptyConverter : IValueConverter, IMarkupExtension
    {
        /// <summary>
        /// Static property to get a new instance of the converter
        /// </summary>
        public static IsEmptyConverter Instance => new IsEmptyConverter();
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
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)Convert(value, targetType, parameter, culture);
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}