namespace DIPS.Xamarin.UI.Extensions
{
    /// <summary>
    /// Object extension methods
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Get an property value using only an string as an identifier
        /// </summary>
        /// <param name="obj">The object to try to get the value from</param>
        /// <param name="propertyName">The property name to try to get an value from</param>
        /// <returns></returns>
        public static string GetPropertyValue(this object obj, string propertyName)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(propertyName))
            {
                return obj.ToString();
            }

            var displayMember = obj.GetType().GetProperty(propertyName);

            var value = displayMember?.GetValue(obj, null);
            return value == null ? string.Empty : value.ToString();
        }

        /// <summary>
        /// Tries to extract a double value from a property on an object. If the property is not a double value, it will use the ToString(). If the ToString is not a double value it will use the defaultValue parameter.
        /// </summary>
        /// <param name="obj">The object to try to get the value from</param>
        /// <param name="propertyName">The property to extract the value from</param>
        /// <param name="defaultValue">The default value if no value is found</param>
        /// <returns></returns>
        public static double ExtractDouble(this object obj, string propertyName, double defaultValue)
        {
            var value = obj.GetPropertyValue(propertyName);
            var isDouble = double.TryParse(value, out double dValue);
            return isDouble ? dValue : defaultValue;
        }
    }
}
