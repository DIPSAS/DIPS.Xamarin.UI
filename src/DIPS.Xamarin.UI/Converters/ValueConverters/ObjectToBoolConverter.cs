using System;
using System.Globalization;
using DIPS.Xamarin.UI.Internal.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Converters.ValueConverters
{
    /// <summary>
    ///     Attempts to convert an object into its respective bool value based on its own equality implementation
    /// </summary>
    public class ObjectToBoolConverter : IValueConverter, IMarkupExtension
    {
        private IServiceProvider m_serviceProvider;
        
        /// <summary>
        /// The object value that equals a true boolean value
        /// </summary>
        public object? TrueObject { get; set; }
        /// <summary>
        /// A boolean value to set if the output value should be inverted
        /// </summary>
        public bool Inverted { get; set; }
        
        /// <inheritdoc />
        public object Convert(object inputValue, Type targetType, object parameter, CultureInfo culture)
        {
            if (TrueObject == null) throw new XamlParseException($"{nameof(TrueObject)} can not be null").WithXmlLineInfo(m_serviceProvider);

            var result = inputValue.Equals(TrueObject);

            return Inverted ? !result : result;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            m_serviceProvider = serviceProvider;
            return this;
        }
    }
}