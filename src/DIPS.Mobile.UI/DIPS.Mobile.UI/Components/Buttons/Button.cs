using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Buttons
{
    public class Button : Xamarin.Forms.Button
    {
        public Button()
        {
            SetColors(Application.Current.RequestedTheme);
            ContentLayout = new ButtonContentLayout(ButtonContentLayout.ImagePosition.Left, 5);
            Application.Current.RequestedThemeChanged += OnRequestedThemeChanged;
        }

        private void OnRequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            SetColors(e.RequestedTheme);
        }

        private void SetColors(OSAppTheme osAppTheme)
        {
            BackgroundColor = Colors.GetColor(ColorName.color_primary_light_primary_100);
            TextColor = Color.White;
        }
    }
}