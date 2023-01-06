using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Resources.Colors
{
    /// <summary>
    ///     Color palette colors should be used for backgrounds, borders, buttons, checkbox or other controls that the app uses
    /// </summary>
    public static class ColorPalette
    {
#pragma warning disable 1591
        public static Color Dark = Color.FromHex("#000000");
        public static Color DarkLight = Color.FromHex("#111111");
        public static Color DarkAir = Color.FromHex("#3B3D3D");

        public static Color Tertiary = Color.FromHex("#404040");
        public static Color TertiaryLight = Color.FromHex("#4A4A4A");
        public static Color TertiaryAir = Color.FromHex("#646464");

        public static Color Quaternary = Color.FromHex("#76797A");
        public static Color QuaternaryLight = Color.FromHex("#7F7F7F");
        public static Color QuaternaryAir = Color.FromHex("#8C8C8C");

        public static Color Quinary = Color.FromHex("#ACACAC");
        public static Color QuinaryLight = Color.FromHex("#B2B2B2");
        public static Color QuinaryAir = Color.FromHex("#D9D9D9");

        public static Color Light = Color.FromHex("#EBEBEB");
        public static Color LightLight = Color.FromHex("#F9F9F9");
        public static Color LightAir = Color.FromHex("#FFFFFF");

        public static Color Accent = Color.FromHex("#AB69BF");
        public static Color AccentLight = Color.FromHex("#D297E3");
        public static Color AccentAir = Color.FromHex("#F4DDFA");

        public static Color Aqua = Color.FromHex("#129DDB");
#pragma warning restore 1591

        /// <summary>
        /// Returns a <see cref="Theme"/> Color from it's enum
        /// </summary>
        /// <param name="colorPaletteEnum">The enum to get the color from</param>
        /// <returns>A theme color</returns>
        public static Color FromIdentifier(this Identifier colorPaletteEnum)
        {
            return colorPaletteEnum switch
            {
                Identifier.Dark => Dark,
                Identifier.DarkLight => DarkLight,
                Identifier.DarkAir => DarkAir,
                Identifier.Tertiary => Tertiary,
                Identifier.TertiaryLight => TertiaryLight,
                Identifier.TertiaryAir => TertiaryAir,
                Identifier.Quaternary => Quaternary,
                Identifier.QuaternaryLight => QuaternaryLight,
                Identifier.QuaternaryAir => QuaternaryAir,
                Identifier.Quinary => Quinary,
                Identifier.QuinaryLight => QuinaryLight,
                Identifier.QuinaryAir => QuinaryAir,
                Identifier.Light => Light,
                Identifier.LightLight => LightLight,
                Identifier.LightAir => LightAir,
                Identifier.Accent => Accent,
                Identifier.AccentLight => AccentLight,
                Identifier.AccentAir => AccentAir,
                Identifier.Aqua => Aqua,
                _ => throw new System.NotImplementedException()
            };
        }
        /// <summary>
        /// Enum that can be used to identify a color
        /// </summary>
        public enum Identifier
        {
#pragma warning disable 1591
            Dark,
            DarkLight,
            DarkAir,
            Tertiary,
            TertiaryLight,
            TertiaryAir,
            Quaternary,
            QuaternaryLight,
            QuaternaryAir,
            Quinary,
            QuinaryLight,
            QuinaryAir,
            Light,
            LightLight,
            LightAir,
            Accent,
            AccentLight,
            AccentAir,
            Aqua
#pragma warning restore 1591
        }
    }
}