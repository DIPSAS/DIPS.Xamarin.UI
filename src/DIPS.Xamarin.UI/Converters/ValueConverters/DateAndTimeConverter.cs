using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Converters.ValueConverters
{
    public class DateAndTimeConverter : IMarkupExtension, IValueConverter
    {
        private const string Space = " ";
        public DateAndTimeConverterFormat Format { get; set; }
        /// <inheritdoc />
        public object ProvideValue(IServiceProvider serviceProvider) => this;

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is DateTime dateTimeInput))
                throw new ArgumentException("The input has to be of type DateTime");
            return (Format) switch { 
                DateAndTimeConverterFormat.Short => ConvertToShortFormat(dateTimeInput, culture),
                DateAndTimeConverterFormat.Text => ConvertToTextFormat(dateTimeInput, culture),
                _ => string.Empty
                };
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

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public enum DateAndTimeConverterFormat
        {
            Short = 0,
            Default = Short,
            Text,
        }
    }
}
