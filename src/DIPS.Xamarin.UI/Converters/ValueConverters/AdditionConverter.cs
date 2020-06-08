using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using DIPS.Xamarin.UI.Internal.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Converters.ValueConverters
{
    /// <summary>
    /// Adds the provided value (a term) with a <see cref="Addend"/> to create a sum
    /// </summary>
    public class AdditionConverter : IMarkupExtension, IValueConverter
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
        /// A number which is added to the provided value
        /// </summary>
        public double? Addend { get; set; }

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new XamlParseException("Value is null").WithXmlLineInfo(m_serviceProvider);
            if (Addend == null)
                throw new XamlParseException("Addend is null, it has to be a double").WithXmlLineInfo(m_serviceProvider);
            if (!double.TryParse(value.ToString(), out var term))
                throw new XamlParseException("Value is not a number").WithXmlLineInfo(m_serviceProvider);
            return term + Addend;
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
