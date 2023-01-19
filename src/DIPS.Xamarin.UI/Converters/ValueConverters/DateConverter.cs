using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using DIPS.Xamarin.UI.Internal.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Converters.ValueConverters
{
    /// <summary>
    ///     Converts an DateTime object to a format and convert it to a readable string in local timezone
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
        private IServiceProvider m_serviceProvider;
        
        /// <summary>
        /// 
        /// </summary>
        public DateConverterFormat Format { get; set; }
        
        /// <summary>
        ///     Ignores the conversion to the local timezone
        /// </summary>
        public bool IgnoreLocalTime { get; set; }

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

            return DateTimeFormatter.FormatDate(dateTimeInput, culture, IgnoreLocalTime, Format);
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}