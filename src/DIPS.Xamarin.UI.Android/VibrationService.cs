using System;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using DIPS.Xamarin.UI.Vibration;

namespace DIPS.Xamarin.UI.Android
{
    internal class VibrationService : IVibrationService
    {
        private static Activity s_activity;
        private static Permission s_hasPermission;
        private static Vibrator? s_vibrator;

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

            if (Build.VERSION.SdkInt <= BuildVersionCodes.P)
            {
                s_vibrator?.Vibrate(VibrationEffect.CreateOneShot(10, VibrationEffect.DefaultAmplitude));
            }
            else
            {
                s_vibrator?.Vibrate(VibrationEffect.CreatePredefined(VibrationEffect.EffectClick));
            }
        }

        public void HeavyClick()
        {
            if (!ShouldVibrate())
            {
                return;
            }
            
            if (Build.VERSION.SdkInt <= BuildVersionCodes.P)
            {
                s_vibrator?.Vibrate(VibrationEffect.CreateOneShot(20, VibrationEffect.DefaultAmplitude + 10));
            }
            else
            {
                s_vibrator?.Vibrate(VibrationEffect.CreatePredefined(VibrationEffect.EffectHeavyClick));
            }
        }

        public async void DoubleClick()
        {
            if (!ShouldVibrate())
            {
                return;
            }
            
            if (Build.VERSION.SdkInt <= BuildVersionCodes.P)
            {
                s_vibrator?.Vibrate(VibrationEffect.CreateOneShot(10, VibrationEffect.DefaultAmplitude));
                await Task.Delay(20);
                s_vibrator?.Vibrate(VibrationEffect.CreateOneShot(10, VibrationEffect.DefaultAmplitude));
            }
            else
            {
                s_vibrator?.Vibrate(VibrationEffect.CreatePredefined(VibrationEffect.EffectDoubleClick));
            }
        }

        public void Error()
        {
            if (!ShouldVibrate())
            {
                return;
            }

            var pause = 50;
            var action = 80;
            s_vibrator?.Vibrate(
                VibrationEffect.CreateWaveform(new long[] {0, action, pause, action, pause, action, pause, 150}, -1));
        }

        public void Success()
        {
            if (!ShouldVibrate())
            {
                return;
            }

            var pause = 50;
            var action = 80;
            s_vibrator?.Vibrate(VibrationEffect.CreateWaveform(new long[] {0, action, pause, action}, -1));
        }

        public IPlatformFeedbackGenerator Generate()
        {
            return new PlatformFeedbackGenerator();
        }

        internal static void Initialize(Activity activity)
        {
            s_activity = activity;
            s_hasPermission = s_activity.CheckSelfPermission(Manifest.Permission.Vibrate);
        }

        private static bool ShouldVibrate()
        {
            s_vibrator ??= Vibrator.FromContext(s_activity);
            return true;
        }

        private class PlatformFeedbackGenerator : IPlatformFeedbackGenerator
        {
            private Vibrator? m_vibrator;
            private VibrationEffect? m_vibe = VibrationEffect.CreateOneShot(5, 5);

            public PlatformFeedbackGenerator()
            {
                m_vibrator ??= Vibrator.FromContext(s_activity);
            }
            
            public void SelectionChanged()
            {
                if (s_hasPermission == Permission.Denied)
                {
                    return;
                }

                m_vibrator?.Vibrate(m_vibe);
            }

            public void Prepare()
            {
            }

            public void Release()
            {
                m_vibrator = null;
            }
        }
    }
}