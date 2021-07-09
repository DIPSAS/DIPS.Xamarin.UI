using System;

namespace DIPS.Xamarin.UI.Vibration
{
    public static class Vibration
    {
        internal static IVibrationService? VibrationService { get; set; }

        internal static void Initialize(IVibrationService vibrationService)
        {
            VibrationService = vibrationService;
        }

        public static void Vibrate(int duration)
        {
            if (duration < 0) return;
            VibrationService?.Vibrate(duration);
        }
        
        public static void Click()
        {
            VibrationService?.Click();
        }        
        
        public static void DoubleClick()
        {
            VibrationService?.DoubleClick();
        }        
        
        public static void HeavyClick()
        {
            VibrationService?.HeavyClick();
        }

        public static void SelectionChanged()
        {
            VibrationService?.SelectionChanged();
        }
        
        public static void Error()
        {
            VibrationService?.Error();
        }
        
        public static void Success()
        {
            VibrationService?.Success();
        }
    }
}