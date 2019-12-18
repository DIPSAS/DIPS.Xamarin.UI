using System;
using System.Globalization;
using DIPS.Xamarin.UI.Extensions;
using DIPS.Xamarin.UI.Resources.LocalizedStrings;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Converters.ValueConverters
{
    /// <summary>
    /// Converts an DateTime object to an format that will only include the date on with an <see cref="Format"/>
    /// </summary>
    public class DateConverter : IValueConverter, IMarkupExtension
    {
        private const string Space = " ";

        /// <summary>
        /// The format to choose between, see <see cref="DateConverterFormat"/>
        /// </summary>
        public DateConverterFormat Format { get; set; }

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateTime dateTimeInput))
                throw new ArgumentException("The input has to be of type DateTime");
            return (Format) switch { 
                DateConverterFormat.Default => ConvertToDefaultDateTime(dateTimeInput, culture),
                DateConverterFormat.Text => ConvertDateTimeAsText(dateTimeInput, culture),
                _ => string.Empty
            };
        }

        private static string ConvertToDefaultDateTime(DateTime dateTime, CultureInfo culture)
        {
            var day = dateTime.ToString("dd", culture);
            var month = dateTime.ToString("MMM", culture);
            var year = dateTime.ToString("yyyy", culture);
            return $"{day}.{Space}{month}{Space}{year}".ToLower();

        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static string ConvertDateTimeAsText(DateTime dateTime, CultureInfo culture)
        {

            if (dateTime.IsToday())
            {
                return InternalLocalizedStrings.Today;
            }
            if (dateTime.IsYesterday())
            {
                return InternalLocalizedStrings.Yesterday;
            }
            if (dateTime.IsTomorrow())
            {
                return InternalLocalizedStrings.Tomorrow;
            }

            var month = dateTime.ToString("MMM", culture);
            var day = dateTime.ToString("dd");
            return $"{day}.{Space}{month}".ToLower();
        }

        /// <inheritdoc />
        public object ProvideValue(IServiceProvider serviceProvider) => this;

        /// <summary>
        /// The formats to choose between during conversion
        /// </summary>
        public enum DateConverterFormat
        {
            /// <summary>
            /// Short date format
            /// </summary>
            /// <example>12. dec 2019</example>
            Short = 0,
            /// <summary>
            /// Default date format, <see cref="Short"/>
            /// </summary>
            /// <example>12. dec 2019</example>
            Default = Short,
            /// <summary>
            /// Shows only the day + month
            /// </summary>
            /// <remarks>
            /// If the date is today, tomorrow or yesterday it will show a localized string instead of day + month
            /// </remarks>
            Text,
        }
    }
}