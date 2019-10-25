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
    }
}
