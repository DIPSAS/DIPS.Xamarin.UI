using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Resources.Colors
{
    /// <summary>
    ///     Status colors palette colors should be used to indicate different statuses.
    /// </summary>
    public static class StatusColorPalette
    {
#pragma warning disable 1591
        public static Color DangerDark = Color.FromHex("#C9524D");
        public static Color Danger = Color.FromHex("#F76D6D");
        public static Color DangerLight = Color.FromHex("#E19D9A");
        public static Color DangerAir = Color.FromHex("#FFDEDE");

        public static Color WarningDark = Color.FromHex("#D29F0F");
        public static Color Warning = Color.FromHex("#FFDC52");
        public static Color WarningLight = Color.FromHex("#EDD5A6");
        public static Color WarningAir = Color.FromHex("#F3E9BC");

        public static Color SuccessDark = Color.FromHex("#006700");
        public static Color Success = Color.FromHex("#5CB85C");
        public static Color SuccessLight = Color.FromHex("#9FD99F");
        public static Color SuccessAir = Color.FromHex("#CCEBCC");

        public static Color InfoDark = Color.FromHex("#266B89");
        public static Color Info = Color.FromHex("#337AB7");
        public static Color InfoLight = Color.FromHex("#7FAFD8");
        public static Color InfoAir = Color.FromHex("#C4DBEF");

        public static Color IdleDark = Color.FromHex("#697577");
        public static Color Idle = Color.FromHex("#92A1A3");
        public static Color IdleLight = Color.FromHex("#B1BEBF");
        public static Color IdleAir = Color.FromHex("#D3DDDE");
#pragma warning restore 1591

        /// <summary>
        /// Returns a <see cref="Theme"/> Color from it's enum
        /// </summary>
        /// <param name="statusColorPaletteEnum">The enum to get the color from</param>
        /// <returns>A theme color</returns>
        /// <remarks>Returns <see cref="Color.Black"/> if input is null</remarks>
        public static Color FromIdentifier(this Identifier? statusColorPaletteEnum)
        {
            if (statusColorPaletteEnum == null)
            {
                return Color.Black;
            }

            return statusColorPaletteEnum switch
            {
                Identifier.DangerDark => DangerDark,
                Identifier.Danger => Danger,
                Identifier.DangerLight => DangerLight,
                Identifier.DangerAir => DangerAir,
                Identifier.WarningDark => WarningDark,
                Identifier.Warning => Warning,
                Identifier.WarningLight => WarningLight,
                Identifier.WarningAir => WarningAir,
                Identifier.SuccessDark => SuccessDark,
                Identifier.Success => Success,
                Identifier.SuccessLight => SuccessLight,
                Identifier.SuccessAir => SuccessAir,
                Identifier.InfoDark => InfoDark,
                Identifier.Info => Info,
                Identifier.InfoLight => InfoLight,
                Identifier.InfoAir => InfoAir,
                Identifier.IdleDark => IdleDark,
                Identifier.Idle => Idle,
                Identifier.IdleLight => IdleLight,
                Identifier.IdleAir => IdleAir,
                _ => throw new System.NotImplementedException(),
            };
        }
        /// <summary>
        /// Enum representation of <see cref="StatusColorPalette"/> color
        /// </summary>
        public enum Identifier
        {
#pragma warning disable 1591
            DangerDark,
            Danger,
            DangerLight,
            DangerAir,
            WarningDark,
            Warning,
            WarningLight,
            WarningAir,
            SuccessDark,
            Success,
            SuccessLight,
            SuccessAir,
            InfoDark,
            Info,
            InfoLight,
            InfoAir,
            IdleDark,
            Idle,
            IdleLight,
            IdleAir
#pragma warning restore 1591
        }
    }

    
}