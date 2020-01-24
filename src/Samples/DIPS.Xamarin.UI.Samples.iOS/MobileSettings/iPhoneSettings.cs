using DIPS.Xamarin.UI.InternalShared;
using Foundation;
using UIKit;

namespace DIPS.Xamarin.UI.Samples.iOS.MobileSettings
{
    public class iPhoneSettings : IMobileSettings
    {
        public void OpenLocale()
        {
            UIApplication.SharedApplication.OpenUrl(new NSUrl(UIKit.UIApplication.OpenSettingsUrlString));
        }
    }
}