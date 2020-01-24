using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TypeConverter = Xamarin.Forms.TypeConverter;

namespace DIPS.Xamarin.UI.Internal.Utilities
{
    /// <summary>
    /// A font size converter facade that makes sure that the font size type converter always runs as a label and returns the correct named size value every time
    /// </summary>
    [TypeConversion(typeof(double))]
    public class LabelFontSizeTypeConverter : TypeConverter
    {
        private readonly FontSizeConverter m_fontSizeConverter;

        /// <inheritdoc />
        public LabelFontSizeTypeConverter()
        {
            m_fontSizeConverter = new FontSizeConverter();    
        }

        /// <inheritdoc />
        public override object ConvertFromInvariantString(string value)
        {
            return m_fontSizeConverter.ConvertFromInvariantString(value);
        }
    }
}
