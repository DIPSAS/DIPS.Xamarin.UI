using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.Sheet.CheatSheet
{
    [TypeConversion(typeof(IList<double>))]
    public class SnapPointConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            var strings = value.Split(',');

            var doubles = new List<double>();
            foreach (var t in strings)
            {
                if (double.TryParse(t, out var d))
                {
                    d = Math.Abs(d);
                    doubles.Add(d > 1.0 ? 1 : d);
                }
            }

            return doubles;
        }
    }
}