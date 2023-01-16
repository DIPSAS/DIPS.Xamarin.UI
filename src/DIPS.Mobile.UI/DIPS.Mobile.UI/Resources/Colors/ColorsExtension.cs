using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Mobile.UI.Resources.Colors
{
    [ContentProperty(nameof(ColorName))]
    public class ColorsExtension : IMarkupExtension<Color>
    {
        private static readonly Color s_defaultColor = Color.White;
        
        public ColorName ColorName { get; set; }

        public static Color? GetColor(string colorName)
        {
            var colors = new Colors();
            if (!colors.ContainsKey(colorName))
            {
                return null;
            }

            if (!colors.TryGetValue(colorName, out var value))
            {
                return null;
            }

            if (value is Color color)
            {
                return color;
            }

            return null;
        }
        
        public static Color? GetColor(ColorName colorName)
        {
            return GetColor(colorName.ToString());
        }
        
        Color IMarkupExtension<Color>.ProvideValue(IServiceProvider serviceProvider)
        {
            return GetColor(ColorName) ?? s_defaultColor;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}