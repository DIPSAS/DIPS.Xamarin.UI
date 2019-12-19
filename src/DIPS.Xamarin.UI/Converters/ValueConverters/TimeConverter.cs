using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Converters.ValueConverters
{
    public class TimeConverter : IMarkupExtension, IValueConverter
    {
        public TimeConverterFormat Format { get; set; }
        /// <inheritdoc />
        public object ProvideValue(IServiceProvider serviceProvider) => this;

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dateTimeInput = DateTime.MinValue;
            if(value == null || (!(value is DateTime) && !(value is TimeSpan))) throw new ArgumentException("The input has to be of type DateTime or TimeSpan");

            switch (value) {
                case TimeSpan timeSpanInput:
                    dateTimeInput += timeSpanInput;
                    break;
                case DateTime dateTimeValue:
                    dateTimeInput = dateTimeValue;
                    break;
            }

            return (Format) switch {
                TimeConverterFormat.Default => ConvertToDefaultFormat(dateTimeInput, culture),
                _=> string.Empty
            };
        }

        private static string ConvertToDefaultFormat(DateTime dateTimeInput, CultureInfo culture)
        {
            var time = dateTimeInput.ToString("hh:mm tt", culture);
            if (culture.IsNorwegian())
            {
                time = dateTimeInput.ToString("hh:mm", culture);
            }

            return time;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public enum TimeConverterFormat
        {
            Default = 0,
        }
    }
}
