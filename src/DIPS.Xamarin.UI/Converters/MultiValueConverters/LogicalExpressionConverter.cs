using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using DIPS.Xamarin.UI.Internal.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Converters.MultiValueConverters
{
    /// <summary>
    /// A converter to run a logical gate on multiple boolean values <see cref="LogicalGate"/>.
    /// </summary>
    public class LogicalExpressionConverter : IMarkupExtension, IMultiValueConverter
    {
        private IServiceProvider? m_serviceProvider;

        /// <summary>
        /// <inheritdoc cref="LogicalGate"/>
        /// </summary>
        public LogicalGate LogicalGate { get; set; }
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

            if (values.Any(o => o == null))
                return FalseObject;

            try
            {
                var bools = values.Cast<bool>().ToList();

                var logcalExpression = false;
                logcalExpression = LogicalGate switch
                {
                    LogicalGate.And => bools.All(b => b),
                    LogicalGate.Nand => !bools.All(b => b),
                    LogicalGate.Or => bools.Any(b => b),
                    LogicalGate.Nor => !bools.Any(b => b),
                    LogicalGate.Xor => bools.Count(b => b) == 1,
                    LogicalGate.Xand => bools.All(b => b) || bools.All(b => !b),
                    _ => throw new ArgumentOutOfRangeException(),
                };
                return logcalExpression ? !Inverted ? TrueObject : FalseObject : !Inverted ? FalseObject : TrueObject;

            }
            catch (Exception e)
            {
                throw new XamlParseException($"LogicalExpressionConverter : Something went wrong while converting: {e.Message}").WithXmlLineInfo(m_serviceProvider);
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

    /// <summary>
    /// A logical gate
    /// </summary>
    public enum LogicalGate
    {
        /// <summary>
        /// And <see href="https://en.wikipedia.org/wiki/NAND_gate"/>
        /// </summary>
        And,
        /// <summary>
        /// Nand <see href="https://en.wikipedia.org/wiki/NAND_gate"/>
        /// </summary>
        Nand,
        /// <summary>
        /// Or <see href="https://en.wikipedia.org/wiki/OR_gate"/>
        /// </summary>
        Or,
        /// <summary>
        /// Nor <see href="https://en.wikipedia.org/wiki/NOR_gate"/>
        /// </summary>
        Nor,
        /// <summary>
        ///Exclusive Or <see href="https://en.wikipedia.org/wiki/Exclusive_or"/>
        /// </summary>
        Xor,
        /// <summary>
        /// Exclusive And <see href="https://deepai.org/machine-learning-glossary-and-terms/xand"/>
        /// </summary>
        Xand,
    }
}