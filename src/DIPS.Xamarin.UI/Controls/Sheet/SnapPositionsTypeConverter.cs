using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.Sheet
{
    [TypeConversion(typeof(double[]))]
    public class SnapPositionsTypeConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                {
                    return new double[] { };
                }

                if (value.Contains(","))
                {
                    return value.Split(',').Select(d => double.Parse(d, CultureInfo.InvariantCulture)).ToArray();
                }

                return value.Split(' ').Select(d => double.Parse(d, CultureInfo.InvariantCulture)).ToArray();
            }
            catch(Exception e)
            {
                throw new XamlParseException($"{nameof(SheetBehavior)} SnapPositions has to be of format: 0.2, 0.3, 0.5");
            }
        }
    }
}
