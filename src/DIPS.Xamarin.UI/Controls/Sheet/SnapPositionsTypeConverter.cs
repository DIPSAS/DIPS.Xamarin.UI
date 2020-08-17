using System;
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
            if (string.IsNullOrEmpty(value))
            {
                return new double[] { };
            }

            return value.Split(',').Select(double.Parse).ToArray();
        }
    }
}
