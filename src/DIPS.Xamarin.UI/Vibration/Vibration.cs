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

        public static void Vibrate(TimeSpan duration)
        {
            VibrationService?.Vibrate(duration.Milliseconds);
        }
        
        public static void Click()
        {
            VibrationService?.Click();
        }        
        
        public static void DoubleClick()
        {
            VibrationService?.Click();
        }        
        
        public static void HeavyClick()
        {
            VibrationService?.Click();
        }

        public static void SelectionChanged()
        {
            VibrationService?.SelectionChanged();
        }
        
        public static void Error()
        {
            VibrationService?.Error();
        }
    }
}