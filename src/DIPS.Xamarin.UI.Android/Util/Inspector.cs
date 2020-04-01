using System;
using DIPS.Xamarin.UI.Internal.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace DIPS.Xamarin.UI.Android.Util
{
    /// <summary>
    /// Used to set callbacks to inspect the native implementation of a shared view-class. <see href="https://github.com/DIPSAS/DIPS.Xamarin.UI/wiki/Inspector">(wiki doc)</see>
    /// <example><see cref="OnInspectingViewCallback"/> sends the native implementation of <see cref="global::Android.Views.View"/> when a <see cref="View"/> is getting inspected></example>
    /// </summary>
    public class Inspector : IInspector
    {
        /// <summary>
        /// A callback that should be invoked when the inspector has collected a <see cref="global::Android.Views.View"/> from a <see cref="View"/>
        /// </summary>
        public static Action<global::Android.Views.View> OnInspectingViewCallback { get; set; }

        void IInspector.Inspect(View view)
        {
            var renderer = Platform.GetRenderer(view);
            Platform.SetRenderer(view, renderer);
            var nativeView = renderer?.View;
            OnInspectingViewCallback?.Invoke(nativeView);
        }
    }
}