using System;
using System.Globalization;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Converters.ValueConverters
{
    /// <summary>
    ///     Converters a DateTime to an readable date and time string
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
            /// <example>12 Dec 1990 12:00 PM</example>
            Short = 0,

            /// <summary>
            ///     The default format to use, <see cref="Short" />
            /// </summary>
            Default = Short,

            /// <summary>
            ///     A text format to use during conversion
            /// </summary>
            /// <example>Today 12:00 PM</example>
            Text,
        }

        private const string Space = " ";
        public DateAndTimeConverterFormat Format { get; set; }

        /// <inheritdoc />
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is DateTime dateTimeInput))
                throw new ArgumentException("The input has to be of type DateTime");
            return Format switch { DateAndTimeConverterFormat.Short => ConvertToShortFormat(dateTimeInput, culture), DateAndTimeConverterFormat.Text
                => ConvertToTextFormat(dateTimeInput, culture), _ => string.Empty };
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static string ConvertToTextFormat(DateTime dateTimeInput, CultureInfo culture)
        {
            var date = new DateConverter { Format = DateConverter.DateConverterFormat.Text }.Convert(dateTimeInput, null, null, culture);
            var time = new TimeConverter { Format = TimeConverter.TimeConverterFormat.Default }.Convert(dateTimeInput, null, null, culture);

            return culture.IsNorwegian() ? $"{date}{Space}kl{Space}{time}" : $"{date}{Space}{time}";
        }

        private static string ConvertToShortFormat(DateTime dateTimeInput, CultureInfo culture)
        {
            var date = new DateConverter { Format = DateConverter.DateConverterFormat.Short }.Convert(dateTimeInput, null, null, culture);
            var time = new TimeConverter { Format = TimeConverter.TimeConverterFormat.Default }.Convert(dateTimeInput, null, null, culture);

            return culture.IsNorwegian() ? $"{date}{Space}kl{Space}{time}" : $"{date}{Space}{time}";
        }
    }
}