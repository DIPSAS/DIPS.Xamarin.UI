using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DIPS.Xamarin.UI
{
    /// <summary>
    /// A static class to initialize DIPS.Xamarin.UI Library
    /// </summary>
    public static class Library
    {
        /// <summary>
        /// Initializes the DIPS.Xamarin.UI Library, enables custom namespace http://dips.xamarin.ui.com to be used in XAML.
        /// <remarks>This should be called once per application, typically in the `App.xaml.cs` constructor. </remarks>
        /// </summary>
        [ExcludeFromCodeCoverage]
        public static void Initialize(){}

        /// <summary>
        /// 
        /// </summary>
        public static class PreviewFeatures
        {

            /// <summary>
            /// 
            /// </summary>
            /// <param name="previewFeature"></param>
            public static void EnableFeature(string previewFeature)
            {
                if (previewFeature == MenuButtonBadgeAnimationString) MenuButtonBadgeAnimation = true;
            }

            /// <summary>
            /// 
            /// </summary>
            public static bool MenuButtonBadgeAnimation { get; set; }
            
            
            internal static readonly string MenuButtonBadgeAnimationString = "MenuButtonBadgeAnimation";
        }

    }
}
