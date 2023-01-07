using DIPS.Xamarin.UI.iOS.ContextMenu;
using DIPS.Xamarin.UI.iOS.Util;
using Foundation;
using UIKit;

namespace DIPS.Xamarin.UI.iOS
{
    /// <summary>
    /// A static class to use when having to interact with the library on iOS platform
    /// </summary>
    public static class Library
    {
        private static bool s_isInitialized;

        /// <summary>
        /// Method to call at startup of the app in order to keep assemblies and to run other initializing methods in the library
        /// </summary>
        public static void Initialize()
        {
            if (s_isInitialized) return;
            DIPS.Xamarin.UI.Internal.Utilities.Inspector.Instance = new Inspector();
            InternalDatePickerRenderer.Initialize();
            ContextMenuButtonRenderer.Initialize();
            
            var vibrationService = new VibrationService();
            Vibration.Vibration.Initialize(vibrationService);
            VibrationService.Initialize();
            s_isInitialized = true;
        }
    }
}