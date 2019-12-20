using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Converters.ValueConverters
{
    /// <summary>
    /// Multiplies the input value with a provide factor.
    /// </summary>
    public class MultiplicationConverter : IMarkupExtension, IValueConverter
    {
        /// <inheritdoc />
        public object ProvideValue(IServiceProvider serviceProvider) => this;

        /// <summary>
        /// The factor to multiply with
        /// </summary>
        /// <remarks>This has to be number</remarks>
        public object? Factor { get; set; }

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) throw new ArgumentException("Value is null");
            if (Factor == null) throw new ArgumentException("Factor is null, it has to be a number");
            if (!double.TryParse(value.ToString(), out var number))
                throw new InvalidOperationException("Value is not a number.");
            if (!double.TryParse(Factor.ToString(), out var factor))
                throw new InvalidOperationException("Value is not a number.");

            return number * factor;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
