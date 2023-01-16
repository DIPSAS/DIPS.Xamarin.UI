using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Pages
{
    public class NavigationPage : Xamarin.Forms.NavigationPage
    {
        public NavigationPage(Xamarin.Forms.ContentPage contentPage) : base(contentPage)
        {
            SetColors(Application.Current.RequestedTheme);
            Application.Current.RequestedThemeChanged += OnRequestedThemeChanged;
        }

        private void SetColors(OSAppTheme osAppTheme)
        {
            BarBackgroundColor = Colors.GetColor(ColorName.color_primary_light_primary_100);
            BarTextColor = (osAppTheme == OSAppTheme.Dark) ? Color.White : Color.White;
        }

        private void OnRequestedThemeChanged(object sender, AppThemeChangedEventArgs e) => SetColors(e.RequestedTheme);

        protected override void OnDisappearing()
        {
            Application.Current.RequestedThemeChanged -= OnRequestedThemeChanged;
            base.OnDisappearing();
        }
    }
}