using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DIPS.Xamarin.UI.Extensions.Markup;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Converters.ValueConverters
{
    /// <summary>
    /// Uses <see cref="StringCaseExtension"/> to converter a string to a <see cref="StringCase"/>
    /// </summary>
    public class StringCaseConverter : IMarkupExtension, IValueConverter
    {
        /// <summary>
        /// <see cref="StringCase"/>
        /// </summary>
        public StringCase StringCase { get; set; }

        /// <inheritdoc/>
        public object ProvideValue(IServiceProvider serviceProvider) => this;

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string stringValue))
                throw new Exception("Input has to be of type string");
            if (stringValue == string.Empty)
                return string.Empty;

            var stringCaseExtension = new StringCaseExtension() { Input = stringValue, StringCase = StringCase };
#nullable disable
            return stringCaseExtension.ProvideValue(null);
#nullable restore
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
