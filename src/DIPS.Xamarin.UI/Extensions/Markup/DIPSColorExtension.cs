using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPS.Xamarin.UI.Resources.Colors;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Extensions.Markup
{
    public class DIPSColorExtension : IMarkupExtension
    {
        public ThemeEnum Theme { get; set; }
        public StatusColorPaletteEnum StatusColorPalette { get; set; }
        public ColorPaletteEnum ColorPalette { get; set; }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            var listOfBools = new List<bool>() { Theme != ThemeEnum.None, StatusColorPalette != StatusColorPaletteEnum.None, ColorPalette != ColorPaletteEnum.None };

            if(listOfBools.Count(b => b) > 1)//More than one color family is set
            {

            }
            else if(listOfBools.Count(b => b) == 0) //Need to set at least one color family
            {

            }
            else //Happy case
            {
                if(Theme != ThemeEnum.None)
                {
                    return Theme switch
                    {
                        ThemeEnum.TealPrimary => Resources.Colors.Theme.TealPrimary,
                        ThemeEnum.TealPrimaryLight => Resources.Colors.Theme.TealPrimaryLight,
                        ThemeEnum.TealPrimaryAir => Resources.Colors.Theme.TealPrimaryAir,
                        ThemeEnum.TealSecondary => Resources.Colors.Theme.TealSecondary,
                        ThemeEnum.TealSecondaryLight => Resources.Colors.Theme.TealSecondaryLight,
                        ThemeEnum.TealSecondaryAir => Resources.Colors.Theme.TealSecondaryAir,
                    };
                }

                if(StatusColorPalette != StatusColorPaletteEnum.None)
                {
                    return StatusColorPalette switch
                    {
                        StatusColorPaletteEnum.DangerDark => Resources.Colors.StatusColorPalette.DangerDark,
                        StatusColorPaletteEnum.Danger => Resources.Colors.StatusColorPalette.Danger,
                        StatusColorPaletteEnum.DangerLight => Resources.Colors.StatusColorPalette.DangerLight,
                        StatusColorPaletteEnum.DangerAir => Resources.Colors.StatusColorPalette.DangerAir,
                        StatusColorPaletteEnum.WarningDark => Resources.Colors.StatusColorPalette.WarningDark,
                        StatusColorPaletteEnum.Warning => Resources.Colors.StatusColorPalette.Warning,
                        StatusColorPaletteEnum.WarningLight => Resources.Colors.StatusColorPalette.WarningLight,
                        StatusColorPaletteEnum.WarningAir => Resources.Colors.StatusColorPalette.WarningAir,
                        StatusColorPaletteEnum.SuccessDark => Resources.Colors.StatusColorPalette.SuccessDark,
                        StatusColorPaletteEnum.Success => Resources.Colors.StatusColorPalette.Success,
                        StatusColorPaletteEnum.SuccessLight => Resources.Colors.StatusColorPalette.SuccessLight,
                        StatusColorPaletteEnum.SuccessAir => Resources.Colors.StatusColorPalette.SuccessAir,
                        StatusColorPaletteEnum.InfoDark => Resources.Colors.StatusColorPalette.InfoDark,
                        StatusColorPaletteEnum.Info => Resources.Colors.StatusColorPalette.Info,
                        StatusColorPaletteEnum.InfoLight => Resources.Colors.StatusColorPalette.InfoLight,
                        StatusColorPaletteEnum.InfoAir => Resources.Colors.StatusColorPalette.InfoAir,
                        StatusColorPaletteEnum.IdleDark => Resources.Colors.StatusColorPalette.IdleDark,
                        StatusColorPaletteEnum.Idle => Resources.Colors.StatusColorPalette.Idle,
                        StatusColorPaletteEnum.IdleLight => Resources.Colors.StatusColorPalette.IdleLight,
                        StatusColorPaletteEnum.IdleAir => Resources.Colors.StatusColorPalette.IdleAir,
                    };
                }

                if(ColorPalette != ColorPaletteEnum.None)
                {
                    return ColorPalette switch
                    {
                        ColorPaletteEnum.Dark => Resources.Colors.ColorPalette.Dark,
                        ColorPaletteEnum.DarkLight => Resources.Colors.ColorPalette.DarkLight,
                        ColorPaletteEnum.DarkAir => Resources.Colors.ColorPalette.DarkAir,
                        ColorPaletteEnum.Tertiary => Resources.Colors.ColorPalette.Tertiary,
                        ColorPaletteEnum.TertiaryLight => Resources.Colors.ColorPalette.TertiaryLight,
                        ColorPaletteEnum.TertiaryAir => Resources.Colors.ColorPalette.TertiaryAir,
                        ColorPaletteEnum.Quaternary => Resources.Colors.ColorPalette.Quaternary,
                        ColorPaletteEnum.QuaternaryLight => Resources.Colors.ColorPalette.QuaternaryLight,
                        ColorPaletteEnum.QuaternaryAir => Resources.Colors.ColorPalette.QuaternaryAir,
                        ColorPaletteEnum.Quinary => Resources.Colors.ColorPalette.Quinary,
                        ColorPaletteEnum.QuinaryLight => Resources.Colors.ColorPalette.QuinaryLight,
                        ColorPaletteEnum.QuinaryAir => Resources.Colors.ColorPalette.QuinaryAir,
                        ColorPaletteEnum.Light => Resources.Colors.ColorPalette.Light,
                        ColorPaletteEnum.LightLight => Resources.Colors.ColorPalette.LightLight,
                        ColorPaletteEnum.LightAir => Resources.Colors.ColorPalette.LightAir,
                        ColorPaletteEnum.Accent => Resources.Colors.ColorPalette.Accent,
                        ColorPaletteEnum.AccentLight => Resources.Colors.ColorPalette.AccentLight,
                        ColorPaletteEnum.AccentAir => Resources.Colors.ColorPalette.AccentAir
                    };
                }
            }
           
            return Color.FromHex("FFFFFF");
         }
    }
}
