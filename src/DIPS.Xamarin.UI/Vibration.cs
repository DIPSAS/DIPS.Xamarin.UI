using System;
using System.Dynamic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI
{
    public static class Vibration
    {
        private static IVibrationService? VibrationService { get; set; }

        public static void Initialize(IVibrationService vibrationService)
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