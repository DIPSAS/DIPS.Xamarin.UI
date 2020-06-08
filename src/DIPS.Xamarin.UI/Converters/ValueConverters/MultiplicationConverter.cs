using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using DIPS.Xamarin.UI.Internal.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Converters.ValueConverters
{
    /// <summary>
    /// Multiplies the input value with a provide factor.
    /// </summary>
    public class MultiplicationConverter : IMarkupExtension, IValueConverter
    {
        private IServiceProvider m_serviceProvider;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            m_serviceProvider = serviceProvider;
            return this;
        }

        /// <summary>
        /// The factor to multiply with
        /// </summary>
        public double? Factor { get; set; }

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) throw new XamlParseException("Value is null").WithXmlLineInfo(m_serviceProvider);
            if (Factor == null) throw new XamlParseException("Factor is null, it has to be a double").WithXmlLineInfo(m_serviceProvider);
            if (!double.TryParse(value.ToString(), out var number)) throw new XamlParseException("Value is not a number").WithXmlLineInfo(m_serviceProvider);

            return number * Factor;
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
