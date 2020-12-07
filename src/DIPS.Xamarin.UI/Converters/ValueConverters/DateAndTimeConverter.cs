using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using DIPS.Xamarin.UI.Extensions;
using DIPS.Xamarin.UI.Internal.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Converters.ValueConverters
{
    /// <summary>
    ///     Converters a DateTime object with an format and convert it to a readable string
    /// </summary>
    public class DateAndTimeConverter : IMarkupExtension, IValueConverter
    {
        /// <summary>
        ///     An date and time format to use during conversion
        /// </summary>
        public enum DateAndTimeConverterFormat
        {
            /// <summary>
            ///     The short format, which is the same as <see cref="Default" /> to use during conversion
            /// </summary>
            /// <example>12 Dec 1990 13:00</example>
            Short = 0,

            /// <summary>
            ///     The default format to use, <see cref="Short" />
            /// </summary>
            Default = Short,

            /// <summary>
            ///     A text format to use during conversion
            /// </summary>
            /// <example>Today 13:00</example>
            Text,
        }

        private const string Space = " ";
        private IServiceProvider m_serviceProvider;

        /// <summary>
        ///     The format to use during conversion
        /// </summary>
        public DateAndTimeConverterFormat Format { get; set; }

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
                DateAndTimeConverterFormat.Short => ConvertToShortFormat(dateTimeInput, culture), 
                DateAndTimeConverterFormat.Text
                => ConvertToTextFormat(dateTimeInput, culture), _ => string.Empty
            };
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static string ConvertToTextFormat(DateTime dateTimeInput, CultureInfo culture)
        {
            var date = new DateConverter { Format = DateConverter.DateConverterFormat.Text }.Convert(dateTimeInput, null, null, culture);
            var time = new TimeConverter { Format = TimeConverter.TimeConverterFormat.Default }.Convert(dateTimeInput, null, null, culture);

            if (culture.IsNorwegian())
            {
                if (dateTimeInput.IsToday() || dateTimeInput.IsTomorrow() || dateTimeInput.IsYesterday())
                {
                    return $"{date},{Space}kl{Space}{time}";
                }
                return $"{date}{Space}kl{Space}{time}";
            }
            return $"{date}{Space}{time}";
        }

        private static string ConvertToShortFormat(DateTime dateTimeInput, CultureInfo culture)
        {
            var date = new DateConverter { Format = DateConverter.DateConverterFormat.Short }.Convert(dateTimeInput, null, null, culture);
            var time = new TimeConverter { Format = TimeConverter.TimeConverterFormat.Default }.Convert(dateTimeInput, null, null, culture);

            return culture.IsNorwegian() ? $"{date}{Space}kl{Space}{time}" : $"{date}{Space}{time}";
        }
    }
}