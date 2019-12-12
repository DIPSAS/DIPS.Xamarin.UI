using System;
using System.Globalization;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Converters.ValueConverters
{
    /// <summary>
    /// Converts an DateTime object to an format that will include the date and the time with an <see cref="Format"/>
    /// </summary>
    public class DateTimeConverter : IMarkupExtension, IValueConverter
    {

        /// <summary>
        /// The format to choose between, see <see cref="DateTimeFormat"/>
        /// </summary>
        public DateTimeFormat Format { get; set; }

        /// <inheritdoc />
        public object ProvideValue(IServiceProvider serviceProvider) => this;

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateTime dateTimeInput)) throw new ArgumentException("The input has to be of type DateTime");

            const string NorwegianTimePrefix = "kl ";
            var fullFormat = "dd. MMM yyyy {0}HH:mm";
            var formattedDateTime = (Format, culture.IsNorwegian()) switch 
                {
                    (DateTimeFormat.Default, true) => dateTimeInput.ToString(string.Format(fullFormat, NorwegianTimePrefix), culture),
                    (DateTimeFormat.Default, false) => dateTimeInput.ToString(string.Format(fullFormat, string.Empty), culture),
                    (_,_) => string.Empty
                };

            return formattedDateTime.ToLower();
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The formats to choose between during conversion
        /// </summary>
        public enum DateTimeFormat
        {
            /// <summary>
            /// Default date time format
            /// </summary>
            /// <example>12. dec 2019 11:00</example>
            Default = 0
        }
    }
}
