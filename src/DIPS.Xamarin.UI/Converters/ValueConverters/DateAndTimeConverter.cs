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
    ///     Converters a DateTime object with a format and convert it to a readable string in local time zone
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

        /// <summary>
        ///     Ignores the conversion to local timezone
        /// </summary>
        public bool IgnoreLocalTime { get; set; }

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
            if (value == null)
            {
                return string.Empty;
            }

            if (value is not DateTime dateTimeInput)
            {
                throw new XamlParseException("The input has to be of type DateTime").WithXmlLineInfo(m_serviceProvider);
            }

            return DateTimeFormatter.FormatDateAndTime(dateTimeInput, culture, IgnoreLocalTime, Format);
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}