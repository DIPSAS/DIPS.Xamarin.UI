using System.ComponentModel;
using System.Xml;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Resources.Colors
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public partial class Colors
    {
        private const string LightPreFix = "_light_";
        private const string DarkPreFix = "_dark_";

        public Colors()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Get the color value from a <see cref="ColorName"/>
        /// </summary>
        /// <param name="colorName">The name of the color to get</param>
        /// <returns><see cref="Color"/></returns>
        public static Color GetColor(ColorName colorName)
        {
            var colorToLookup = colorName.ToString();
            if (Application.Current.RequestedTheme == OSAppTheme.Dark)
            {
                if (colorToLookup.Contains(LightPreFix))
                {
                    colorToLookup = colorToLookup.Replace(LightPreFix, DarkPreFix);
                }
            }
            else
            {
                if (colorToLookup.Contains(DarkPreFix))
                {
                    colorToLookup = colorToLookup.Replace(DarkPreFix, LightPreFix);
                }
            }

            return ColorsExtension.GetColor(colorToLookup) ?? ColorsExtension.GetColor(colorName) ?? Color.Default;
        }
    }
}