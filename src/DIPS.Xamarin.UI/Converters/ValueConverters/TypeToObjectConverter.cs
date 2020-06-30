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
    /// Converter that can be used to check the type of the binding against a <see cref="Type"/>
    /// </summary>
    public class TypeToObjectConverter : IMarkupExtension, IValueConverter
    {
        private IServiceProvider m_serviceProvider;

        /// <summary>
        /// The object to return when the binding and <see cref="Type"/> is the same type
        /// </summary>
        public object TrueObject { get; set; }
        /// <summary>
        /// THe object to return when the binding and <see cref="Type"/> is not the same
        /// </summary>
        public object FalseObject { get; set; }

        /// <summary>
        /// The type to check against, use {x:Type namespace:MyType}
        /// </summary>
        public Type Type { get; set; }

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new XamlParseException($"value can not be null").WithXmlLineInfo(m_serviceProvider);
            if (Type == null)
                throw new XamlParseException($"Type to check against can not be null").WithXmlLineInfo(m_serviceProvider);
            if (TrueObject == null)
                throw new XamlParseException($"{nameof(TrueObject)} can not be null").WithXmlLineInfo(m_serviceProvider);
            if (FalseObject == null)
                throw new XamlParseException($"{nameof(FalseObject)} can not be null").WithXmlLineInfo(m_serviceProvider);
            return (value.GetType() == Type) ? TrueObject : FalseObject;
        }
        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public object ProvideValue(IServiceProvider serviceProvider) 
        {
            m_serviceProvider = serviceProvider;
            return this;
        }
    }
}
