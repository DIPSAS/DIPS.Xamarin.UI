using System.Diagnostics.CodeAnalysis;
using DIPS.Xamarin.UI.Controls.FloatingActionMenu;
using DIPS.Xamarin.UI.Controls.Toast;

namespace DIPS.Xamarin.UI
{
    /// <summary>
    ///     A static class to initialize DIPS.Xamarin.UI Library
    /// </summary>
    public static class Library
    {
        /// <summary>
        ///     Initializes the DIPS.Xamarin.UI Library, enables custom namespace http://dips.xamarin.ui.com to be used in XAML.
        ///     <remarks>This should be called once per application, typically in the `App.xaml.cs` constructor. </remarks>
        /// </summary>
        [ExcludeFromCodeCoverage]
        public static void Initialize()
        {
            Toast.Initialize();
        }

        /// <summary>
        ///     A static class used to enable features that are in preview.
        /// </summary>
        public static class PreviewFeatures
        {
            /// <summary>
            ///     Toggles animations for the badge on the <see cref="MenuButton" />
            /// </summary>
            public static bool MenuButtonAnimations { get; set; }

            /// <summary>
            ///     Toggles usage of SkeletonView <see cref="SkeletonView" />
            /// </summary>
            public static bool SkeletonView { get; set; }

            /// <summary>
            ///     Enable a feature that's in preview.
            /// </summary>
            /// <param name="previewFeature">A string specifying which preview feature you want to enable.</param>
            public static void EnableFeature(string previewFeature)
            {
                if (previewFeature == "MenuButtonAnimations")
                {
                    MenuButtonAnimations = true;
                }

                if (previewFeature == "SkeletonView")
                {
                    SkeletonView = true;
                }
            }
        }
    }
}