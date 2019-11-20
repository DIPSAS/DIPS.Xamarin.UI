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
            if (string.IsNullOrEmpty(propertyName))
            {
                return obj == null ? string.Empty : obj.ToString();
            }

            var displayMember = obj.GetType().GetProperty(propertyName);

            var value = displayMember?.GetValue(obj, null);
            return value == null ? string.Empty : value.ToString();
        }
    }
}
