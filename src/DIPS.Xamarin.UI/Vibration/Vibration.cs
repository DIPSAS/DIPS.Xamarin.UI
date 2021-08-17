using System;
using Xamarin.Forms.Internals;

namespace DIPS.Xamarin.UI.Vibration
{
    /// <summary>
    /// Vibration/haptic feedback.
    /// </summary>
    public static class Vibration
    {
        internal static IVibrationService? VibrationService { get; set; }

        internal static void Initialize(IVibrationService vibrationService)
        {
            VibrationService = vibrationService;
        }

        /// <summary>
        /// [iOS] Vibrates for a constant short duration.
        /// [Android] Vibrates for specified duration.
        /// </summary>
        /// <param name="duration"></param>
        public static void Vibrate(int duration)
        {
            if (duration < 0) return;
            VibrationService?.Vibrate(duration);
        }
        
        /// <summary>
        /// Produces a simple click feedback.
        /// </summary>
        public static void Click()
        {
            VibrationService?.Click();
        }        
        
        /// <summary>
        /// Produces a double click feedback.
        /// </summary>
        public static void DoubleClick()
        {
            VibrationService?.DoubleClick();
        }        
        
        /// <summary>
        /// Produces a heavier and louder click feedback.
        /// </summary>
        public static void HeavyClick()
        {
            VibrationService?.HeavyClick();
        }

        
        /// <summary>
        /// Produces 4 bursts of feedback.
        /// </summary>
        public static void Error()
        {
            VibrationService?.Error();
        }
        
        /// <summary>
        /// Produces 2 bursts of feedback.
        /// </summary>
        public static void Success()
        {
            VibrationService?.Success();
        }
    }
}