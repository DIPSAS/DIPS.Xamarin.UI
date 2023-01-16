using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Pages
{
    public class ContentPage : Xamarin.Forms.ContentPage
    {
        public ContentPage()
        {
            SetColors(Application.Current.RequestedTheme);
            Application.Current.RequestedThemeChanged += OnRequestedThemeChanged;
        }

        private void OnRequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            SetColors(e.RequestedTheme);
        }

        private void SetColors(OSAppTheme osAppTheme)
        {
            BackgroundColor = Colors.GetColor(ColorName.color_secondary_light_secondary_70);
        }

        protected override void OnDisappearing()
        {
            Application.Current.RequestedThemeChanged -= OnRequestedThemeChanged;
            base.OnDisappearing();
        }
    }
}