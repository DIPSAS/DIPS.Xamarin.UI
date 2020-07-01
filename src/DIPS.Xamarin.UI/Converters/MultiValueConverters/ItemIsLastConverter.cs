using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using DIPS.Xamarin.UI.Internal.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Converters.MultiValueConverters
{
    /// <summary>
    /// A converter to check if an item is the last in a provided list/array/observablecollection.
    /// This converter will not check if the list gets more items added. For this to work, you will need to provide an IsLast property for your viewmodel and binding to this instead.
    /// </summary>
    public class ItemIsLastConverter : IMarkupExtension, IMultiValueConverter
    {
        private IServiceProvider? m_serviceProvider;

        /// <summary>
        /// A object to return when the converter returns true
        /// </summary>
        public object TrueObject { get; set; } = true;

        /// <summary>
        /// A object to return when the converter returns false
        /// </summary>
        public object FalseObject { get; set; } = false;

        /// <summary>
        /// A boolean value to tell the converter to invert the output
        /// </summary>
        public bool Inverted { get; set; }

        /// <inheritdoc/>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
            {
                return FalseObject;
            }

            if (values.Any(o => o == null) || values.Length != 2 || !values.OfType<IList>().Any())
            {
                return FalseObject;
            }

            try
            {
                var list = (values[0] as IList) ?? (IList)values[1];
                var item = list == values[0] ? values[1] : values[0];
                if(list.Count == 0)
                {
                    return FalseObject;
                }

                return item.Equals(list[list.Count - 1]) ? (Inverted ? FalseObject : TrueObject) : (Inverted ? TrueObject : FalseObject);
            }
            catch(Exception e)
            {
                throw new XamlParseException($"ItemIsListConverter : Something went wrong while converting: {e.Message}").WithXmlLineInfo(m_serviceProvider);
            }
        }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("LogicalExpressionConverter does not support convert back");
        }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            m_serviceProvider = serviceProvider;
            return this;
        }
    }
}
