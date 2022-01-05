using System;
using System.Collections.Generic;
using DIPS.Xamarin.UI.Internal.Xaml.Sheet;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.Sheet
{
    
    /// <summary>
    /// Converts a comma-separated string to list of doubles. Values will be clamped between 0 and 1. 
    /// </summary>
    [TypeConversion(typeof(IList<double>))]
    public class SnapPointConverter : TypeConverter
    {
        /// <inheritdoc />
        public override object ConvertFromInvariantString(string value)
        {
            var strings = value.Split(',');

            var doubles = new List<double>();
            foreach (var t in strings)
            {

                if (double.TryParse(t.Trim(),
                    System.Globalization.NumberStyles.Number,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out var d))
                {
                    d = Math.Abs(d);
                    doubles.Add(SheetViewUtility.CoerceRatio(d));
                }
            }
            return doubles;
        }
    }
}