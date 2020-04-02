using System;
using DIPS.Xamarin.UI.Internal.Utilities;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace DIPS.Xamarin.UI.iOS.Util
{
    /// <summary>
    /// Used to set callbacks to inspect the native implementation of a shared view-class. <see href="https://github.com/DIPSAS/DIPS.Xamarin.UI/wiki/Inspector">(wiki doc)</see>
    /// <example><see cref="OnInspectingViewCallback"/> sends the native implementation of <see cref="UIView"/> when a <see cref="View"/> is getting inspected</example>
    /// </summary>
    public class Inspector : IInspector
    {
        /// <summary>
        /// A callback that should be invoked when the inspector has collected a <see cref="UIView"/> from a <see cref="View"/>
        /// </summary>
        public static Action<UIView> OnInspectingViewCallback { get; set; }

        void IInspector.Inspect(View view)
        {
            var renderer = Platform.CreateRenderer(view);
            var nativeView = renderer?.NativeView;
            OnInspectingViewCallback?.Invoke(nativeView);
        }
    }
}