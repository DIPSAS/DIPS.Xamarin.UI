using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Converters
{
    /// <summary>
    /// Pipes input through a group of converters.
    /// </summary>
    public class ValueConverterGroup : List<IValueConverter>, IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return this.Aggregate(value, (current, converter) => converter.Convert(current, targetType, parameter, culture));
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}