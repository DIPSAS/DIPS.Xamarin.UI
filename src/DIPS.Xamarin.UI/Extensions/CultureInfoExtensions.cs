using System.Globalization;

namespace DIPS.Xamarin.UI.Extensions
{
    /// <summary>
    /// Extension methods for the <see cref="CultureInfo"/> class
    /// </summary>
    public static class CultureInfoExtensions
    {
        /// <summary>
        /// Checks if the current culture is norwegian
        /// <remarks>https://en.wikipedia.org/wiki/List_of_ISO_639-2_codes</remarks>
        /// </summary>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public static bool IsNorwegian(this CultureInfo cultureInfo) =>
            cultureInfo.TwoLetterISOLanguageName.Equals("nb") || cultureInfo.TwoLetterISOLanguageName.Equals("nn");
    }
}
