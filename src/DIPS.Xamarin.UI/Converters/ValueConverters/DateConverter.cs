using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using DIPS.Xamarin.UI.Extensions;
using DIPS.Xamarin.UI.Internal.Utilities;
using DIPS.Xamarin.UI.Resources.LocalizedStrings;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Converters.ValueConverters
{
    /// <summary>
    ///     Converts an DateTime object to an format and convert it to a readable string
    /// </summary>
    public class DateConverter : IValueConverter, IMarkupExtension
    {
        /// <summary>
        ///     The formats to choose between during conversion
        /// </summary>
        public enum DateConverterFormat
        {
            /// <summary>
            ///     Short date format
            /// </summary>
            /// <example>12 Dec 2019</example>
            Short = 0,

            /// <summary>
            ///     Default date format, <see cref="Short" />
            /// </summary>
            /// <example>12 Dec 2019</example>
            Default = Short,

            /// <summary>
            ///     Shows only the day + month
            /// </summary>
            /// <remarks>
            ///     If the date is today, tomorrow or yesterday it will show a localized string instead of day + month
            /// </remarks>
            Text,
        }

        private const string Space = " ";
        private IServiceProvider m_serviceProvider;

        /// <summary>
        ///     The format to choose between, see <see cref="DateConverterFormat" />
        /// </summary>
        public DateConverterFormat Format { get; set; }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            m_serviceProvider = serviceProvider;
            return this;
        }

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;
            if (!(value is DateTime dateTimeInput))
                throw new XamlParseException("The input has to be of type DateTime").WithXmlLineInfo(m_serviceProvider);
            return Format switch
            {
                DateConverterFormat.Short => ConvertToDefaultDateTime(dateTimeInput, culture),
                DateConverterFormat.Text =>
                    ConvertDateTimeAsText(dateTimeInput, culture),
                _ => string.Empty
            };
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static string ConvertToDefaultDateTime(DateTime dateTime, CultureInfo culture)
        {
            var day = GetDayBasedOnCulture(dateTime, culture);

            var month = GetMonthBasedOnCulture(dateTime, culture);
            var year = dateTime.ToString("yyyy", culture);
            if (culture.ThreeLetterWindowsLanguageName.Equals("ENU"))
            {
                return $"{month}{Space}{day}{Space}{year}";
            }

            return $"{day}{Space}{month}{Space}{year}";
        }

        private static string GetDayBasedOnCulture(DateTime dateTime, CultureInfo culture)
        {
            var day = dateTime.ToString("dd", culture);
            if (culture.TwoLetterISOLanguageName.Contains("en"))
            {
                day = day.TrimStart('0');
                day += dateTime.GetEnglishDaySuffix();
            }

            if (culture.ThreeLetterWindowsLanguageName.Equals("ENU"))
            {
                day += ",";
            }

            if (culture.IsNorwegian())
            {
                day += ".";
            }

            return day;
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

            var month = GetMonthBasedOnCulture(dateTime, culture);
            var day = GetDayBasedOnCulture(dateTime, culture);

            if (culture.ThreeLetterWindowsLanguageName.Equals("ENU"))
            {
                return $"{month}{Space}{day}";
            }

            return $"{day}{Space}{month}";
        }

        private static string GetMonthBasedOnCulture(DateTime dateTime, CultureInfo culture)
        {
            var month = dateTime.ToString("MMM", culture);
            if (culture.TwoLetterISOLanguageName.Contains("en"))
            {
                month = month[0].ToString().ToUpper() + month.Substring(1);
            }

            return month;
        }
    }
}