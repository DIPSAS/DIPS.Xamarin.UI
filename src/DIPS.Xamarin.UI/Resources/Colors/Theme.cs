using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Resources.Colors
{
    /// <summary>
    ///     Theme colors should be used as an overall color for an app. The colors are suitable for navigation bars, splash
    ///     screen, action bars etc..
    /// </summary>
    public static class Theme
    {
#pragma warning disable 1591
        public static Color TealPrimary = Color.FromHex("#047F89");
        public static Color TealPrimaryLight = Color.FromHex("#65868F");
        public static Color TealPrimaryAir = Color.FromHex("#98B2AE");
        public static Color TealSecondary = Color.FromHex("#97C8CD");
        public static Color TealSecondaryLight = Color.FromHex("#ECF3F4");
        public static Color TealSecondaryAir = Color.FromHex("#F0F5F7");
#pragma warning restore 1591

        /// <summary>
        /// Returns a <see cref="Theme"/> Color from it's enum
        /// </summary>
        /// <param name="themeEnum">The enum to get the color from</param>
        /// <returns>A theme color</returns>
        /// <remarks>Returns <see cref="Color.Black"/> if input is null</remarks>
        public static Color FromIdentifier(this Theme.Identifier? themeEnum)
        {
            if (themeEnum == null)
            {
                return Color.Black;
            }

            return themeEnum switch
            {
                Identifier.TealPrimary => TealPrimary,
                Identifier.TealPrimaryLight => TealPrimaryLight,
                Identifier.TealPrimaryAir => TealPrimaryAir,
                Identifier.TealSecondary => TealSecondary,
                Identifier.TealSecondaryLight => TealSecondaryLight,
                Identifier.TealSecondaryAir => TealSecondaryAir,
                _ => throw new System.NotImplementedException("The enum your are trying to get a color from have not been mapped to a corresponding color."),
            };
        }
        /// <summary>
        /// Enum representation of <see cref="Theme"/> color
        /// </summary>
        public enum Identifier
        {
            TealPrimary,
            TealPrimaryLight,
            TealPrimaryAir,
            TealSecondary,
            TealSecondaryLight,
            TealSecondaryAir,
        }
    }
}