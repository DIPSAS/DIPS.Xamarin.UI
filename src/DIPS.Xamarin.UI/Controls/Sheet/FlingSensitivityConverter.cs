using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.Sheet
{
    /// <summary>
    ///     Converts string to int.
    /// </summary>
    [TypeConversion(typeof(int))]
    public class FlingSensitivityConverter : TypeConverter, IExtendedTypeConverter
    {
        /// <inheritdoc />
        public object ConvertFrom(CultureInfo culture, object value, IServiceProvider serviceProvider)
        {
            return ConvertFromInvariantString(value as string ?? string.Empty);
        }

        /// <inheritdoc />
        public object ConvertFromInvariantString(string value, IServiceProvider serviceProvider)
        {
            return ConvertFromInvariantString(value);
        }

        /// <inheritdoc />
        public override object ConvertFromInvariantString(string value)
        {
            if (int.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out var i))
            {
                return i;
            }

            if (Enum.TryParse<FlingSensitivity>(value, true, out var result))
            {
                switch (result)
                {
                    case FlingSensitivity.Low:
                        return FlingConstants.s_low;
                    case FlingSensitivity.Medium:
                        return FlingConstants.s_medium;
                    case FlingSensitivity.High:
                        return FlingConstants.s_high;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            return FlingConstants.s_medium;
        }
    }
}