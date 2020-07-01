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
    /// A converter that takes a item and a list as bindings and compare the index of the item with a <see cref="Position"/>
    /// </summary>
    public class PositionInListConverter : IMarkupExtension, IMultiValueConverter
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

        /// <summary>
        /// The position to compare with.
        /// Default value is last
        /// </summary>
        /// <remarks>This can be a index (0, 1, 2 ...) or the strings (First, Last)</remarks>
        public string Position { get; set; } = "last";

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

                if (!int.TryParse(Position, out var indexToCompare))
                {
                    indexToCompare = (Position.ToLower()) switch
                    {
                        "first" => 0,
                        "last" => list.Count - 1,
                        _ => throw new XamlParseException("Position attribute has to be a valid integer or string: First, Middle, Last").WithXmlLineInfo(m_serviceProvider)
                    };
                };
                if (indexToCompare >= list.Count || indexToCompare < 0)
                    return FalseObject;
                return item.Equals(list[indexToCompare]) ? (Inverted ? FalseObject : TrueObject) : (Inverted ? TrueObject : FalseObject);
            }
            catch (Exception e)
            {
                throw new XamlParseException($"ItemIsListConverter : Something went wrong while converting: {e.Message}").WithXmlLineInfo(m_serviceProvider);
            }
        }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException($"{nameof(PositionInListConverter)} does not support convert back");
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
