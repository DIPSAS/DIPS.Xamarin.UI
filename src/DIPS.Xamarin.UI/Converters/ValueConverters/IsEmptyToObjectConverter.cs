using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using DIPS.Xamarin.UI.Internal.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Converters.ValueConverters
{
    /// <summary>
    /// Converter that takes different input input types and returns a true/false object to indicate if it is empty or not.
    /// <see cref="IsEmptyConverter"/> for its input types
    /// </summary>
    public class IsEmptyToObjectConverter : IMarkupExtension, IValueConverter
    {
        private IServiceProvider m_serviceProvider;

        /// <summary>
        /// Property to set if we want to return a inverted output value from the converter.
        /// </summary>
        public bool Inverted { get; set; }

        /// <summary>
        /// The value that will return if the boolean input is true
        /// <remarks>Will be the return value if <see cref="Inverted"/> is set to true</remarks>
        /// </summary>
        public object? TrueObject { get; set; }
        /// <summary>
        /// The value that will return if the boolean input is false
        /// <remarks>Will be the return value if <see cref="Inverted"/> is set to false</remarks>
        /// </summary>
        public object? FalseObject { get; set; }

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
            var isEmptyConverter = new IsEmptyConverter(){Inverted = Inverted};
            if (isEmptyConverter == null) throw new XamlParseException($"Something went wrong when constructing {nameof(IsEmptyConverter)}").WithXmlLineInfo(m_serviceProvider);

            var boolToObjectConverter = new BoolToObjectConverter(){TrueObject = TrueObject, FalseObject = FalseObject};
            if(boolToObjectConverter == null) throw new XamlParseException($"Something went wrong when constructing {nameof(BoolToObjectConverter)}").WithXmlLineInfo(m_serviceProvider);

            var booleanOutput = (bool)isEmptyConverter.Convert(value, targetType, parameter, culture);
            return boolToObjectConverter.Convert(booleanOutput, targetType, parameter, culture);
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}