using Android.App;
using Android.Content;
using DIPS.Xamarin.UI.InternalShared;
using Provider = Android.Provider;

namespace DIPS.Xamarin.UI.Samples.Droid.MobileSettings
{
    public class AndroidSettings : IMobileSettings
    {
        private readonly MainActivity m_mainActivity;

        public AndroidSettings(MainActivity mainActivity)
        {
            m_mainActivity = mainActivity;
        }

        public void OpenLocale()
        {
            m_mainActivity?.StartActivity(new Intent(Provider.Settings.ActionLocaleSettings));

        }
    }
}