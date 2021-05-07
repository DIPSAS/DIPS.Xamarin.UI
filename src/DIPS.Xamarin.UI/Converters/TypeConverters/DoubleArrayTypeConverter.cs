using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Converters.TypeConverters {
    public class DoubleArrayConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            var array = value.Split(',');

            var doubleArray = new List<double>();
            foreach (var xamlString in array)
            {
                if (double.TryParse(xamlString.Replace(" ", ""), out var doubleValue))
                {
                    doubleArray.Add(doubleValue);
                }
            }
            return doubleArray.ToArray();
        }
    }
}