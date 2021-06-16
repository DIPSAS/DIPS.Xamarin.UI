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
    ///     Converts an DateTime or TimeSpan object with a format and convert it to a readable time string in local
    ///     timezone
    /// </summary>
    public class TimeConverter : IMarkupExtension, IValueConverter
    {
        private IServiceProvider m_serviceProvider;

        /// <summary>
        ///     The converter format that is used to change the format of the time <see cref="TimeConverter" />
        /// </summary>
        public enum TimeConverterFormat
        {
            /// <summary>
            ///     The default time converter format
            /// </summary>
            /// <example>13:00</example>
            Default = 0,
        }

        /// <summary>
        ///     The format to use during conversion
        /// </summary>
        public TimeConverterFormat Format { get; set; }

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
            var dateTimeInput = DateTime.MinValue;
            if (value == null) return string.Empty;
            if (!(value is DateTime) && !(value is TimeSpan))
                throw new XamlParseException("The input has to be of type DateTime or TimeSpan").WithXmlLineInfo(
                    m_serviceProvider);

            switch (value)
            {
                case TimeSpan timeSpanInput:
                    dateTimeInput += timeSpanInput;
                    break;
                case DateTime dateTimeValue:
                    dateTimeInput = IgnoreLocalTime ? dateTimeValue : dateTimeValue.ToLocalTime();
                    break;
            }

            return Format switch
            {
                TimeConverterFormat.Default => ConvertToDefaultFormat(dateTimeInput, culture),
                _ => string.Empty
            };
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static string ConvertToDefaultFormat(DateTime dateTimeInput, CultureInfo culture)
        {
            var time = dateTimeInput.ToString("HH:mm", culture);
            if (culture.IsNorwegian())
            {
                var hour = dateTimeInput.ToString("HH", culture);
                var minutes = dateTimeInput.ToString("mm", culture);
                time = $"{hour}:{minutes}";
            }

            if (culture.ThreeLetterWindowsLanguageName.Equals("ENU"))
            {
                time = dateTimeInput.ToString("hh:mm tt", culture);
            }

            return time;
        }
    }
}