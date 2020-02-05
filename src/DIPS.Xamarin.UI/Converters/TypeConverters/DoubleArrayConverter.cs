using System;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Converters.TypeConverters {
    public class DoubleArrayConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            var array = value.Split(',');
            return array;
        }
    }
}