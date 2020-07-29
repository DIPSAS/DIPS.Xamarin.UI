using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Toast
{
    public class ToastOptions : BindableObject
    {
        /// <summary>
        ///     Performs action on tapping the toast
        ///     <remarks>Will Override closing the toast on tapping</remarks>
        /// </summary>
        public Action? ToastAction { get; set; }

        /// <summary>
        ///     Animation on displaying the Toast.
        ///     <remarks>Default animation is Fading-In in 250 ms</remarks>
        /// </summary>
        public Func<ToastView, Task> DisplayAnimation { get; set; } = toastView =>
        {
            toastView.Opacity = 0;
            return toastView.FadeTo(1, 500, Easing.Linear);
        };

        /// <summary>
        ///     Animation on closing the Toast
        ///     <remarks>Default animation is Fading-Out in 250 ms</remarks>
        /// </summary>
        public Func<ToastView, Task> CloseAnimation { get; set; } =
            toastView => toastView.FadeTo(0, 500, Easing.Linear);

        /// <summary>
        ///     Hide the toast automatically after the given milliseconds
        ///     <remarks>If value is 0, toast won't be hidden automatically. Default value is 3000 ms</remarks>
        /// </summary>
        public int HideToastIn { get; set; } = 3000;
    }
}