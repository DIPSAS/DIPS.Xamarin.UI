using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Converters.ValueConverters
{
    /// <summary>
    /// Converts an DateTime object to an format that will only include the date on with an <see cref="Format"/>
    /// </summary>
    public class DateConverter : IValueConverter, IMarkupExtension
    {
        /// <summary>
        /// The format to choose between, see <see cref="DateConverterFormat"/>
        /// </summary>
        public DateConverterFormat Format { get; set; }

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateTime dateTimeInput))
                throw new ArgumentException("The input has to be of type DateTime");
            var formattedDateTime = (Format) switch
            {
                DateConverterFormat.Default => dateTimeInput.ToString("dd. MMM yyyy", culture),
                _ => string.Empty
            };

            return formattedDateTime.ToLower();
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public object ProvideValue(IServiceProvider serviceProvider) => this;

        /// <summary>
        /// The formats to choose between during conversion
        /// </summary>
        public enum DateConverterFormat
        {
            /// <summary>
            /// Default date format
            /// </summary>
            /// <example>12. dec 2019</example>
            Default = 0,
        }
    }
}