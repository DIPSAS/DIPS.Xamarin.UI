using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Essentials;

namespace DIPS.Xamarin.UI.Android
{
    public class VibrationService : IVibrationService
    {
        private static Activity s_activity;
        private static Permission s_hasPermission;
        private static Vibrator? s_vibrator;
        private readonly VibrationEffect m_doubleClick = VibrationEffect.CreatePredefined(VibrationEffect.EffectDoubleClick);
        private readonly VibrationEffect m_selectionChanged = VibrationEffect.CreatePredefined(VibrationEffect.EffectTick);
        private readonly VibrationEffect m_click = VibrationEffect.CreatePredefined(VibrationEffect.EffectClick);
        private readonly VibrationEffect m_heavyClick = VibrationEffect.CreatePredefined(VibrationEffect.EffectHeavyClick);

        public void Vibrate(int duration)
        {
            if (!ShouldVibrate())
            {
                return;
            }

            s_vibrator?.Vibrate(VibrationEffect.CreateOneShot(duration, VibrationEffect.DefaultAmplitude));
        }

        public void Click()
        {
            if (!ShouldVibrate())
            {
                return;
            }

            s_vibrator?.Vibrate(m_click);
        }

        public void HeavyClick()
        {
            if (!ShouldVibrate())
            {
                return;
            }

            s_vibrator?.Vibrate(m_heavyClick);
        }

        public void DoubleClick()
        {
            if (!ShouldVibrate())
            {
                return;
            }

            s_vibrator?.Vibrate(m_doubleClick);
        }

        public void SelectionChanged()
        {
            if (!ShouldVibrate())
            {
                return;
            }

            s_vibrator?.Vibrate(m_selectionChanged);
        }

        public void Error()
        {
            if (!ShouldVibrate())
            {
                return;
            }

            s_vibrator?.Vibrate(VibrationEffect.CreateOneShot(250, VibrationEffect.DefaultAmplitude));
        }

        internal static void Initialize()
        {
            s_activity = Platform.CurrentActivity;
            s_hasPermission = s_activity.CheckSelfPermission(Manifest.Permission.Vibrate);
        }

        private static bool ShouldVibrate()
        {
            if (s_hasPermission == Permission.Denied)
            {
                return false;
            }

            s_vibrator ??= Vibrator.FromContext(s_activity);
            return true;
        }
    }
}