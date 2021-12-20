using System;
using System.Globalization;
using DIPS.Xamarin.UI.Internal.xaml;
using DIPS.Xamarin.UI.Internal.Xaml.Sheet;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.Sheet
{
    [TypeConversion(typeof(int))]
    public class FlingSensitivityConverter : TypeConverter, IExtendedTypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            if (int.TryParse(value, out var i))
            {
                return i;
            }
            
            if (Enum.TryParse<FlingSensitivity>(value, true, out var result))
            {
                switch (result)
                {
                    case FlingSensitivity.Low:
                        return 2500;
                    case FlingSensitivity.Medium:
                        return 1000;
                    case FlingSensitivity.High:
                        return 500;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return 1000;
        }

        public object ConvertFrom(CultureInfo culture, object value, IServiceProvider serviceProvider)
        {
            return ConvertFromInvariantString(value as string);
        }

        public object ConvertFromInvariantString(string value, IServiceProvider serviceProvider)
        {
            return ConvertFromInvariantString(value);
        }
    }
}