using System;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Internal.Xaml;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Toast
{
    /// <summary>
    ///     Options to setup in the Toast control
    /// </summary>
    public class ToastOptions
    {
        /// <summary>
        ///     Action to be invoked when the user taps the Toast
        ///     <remarks>Will Override closing the Toast on tapping. Default action is to close the Toast</remarks>
        /// </summary>
        public Action ToastAction { get; set; } = async () => await Toast.Current.HideToast();

        /// <summary>
        ///     Func to be invoked when the Toast is displayed
        ///     <remarks>
        ///         Use this if you need to animate the toast after it is added to the Page. Default animation is Fading-In in
        ///         250 ms
        ///     </remarks>
        /// </summary>
        public Func<ToastView, Task> DisplayAnimation { get; set; } = toast =>
        {
            toast.Opacity = 0;
            return toast.FadeTo(1, 500, Easing.Linear);
        };

        /// <summary>
        ///     Func to be invoked when the Toast is hiding.
        ///     <remarks>
        ///         Use this if you need to animate the toast before it is removed from the page. Default animation is
        ///         Fading-Out in 250 ms
        ///     </remarks>
        /// </summary>
        public Func<ToastView, Task> CloseAnimation { get; set; } = toast => toast.FadeTo(0, 500, Easing.Linear);

        /// <summary>
        ///     Hide the toast automatically after the given milliseconds
        ///     <remarks>If value is 0, toast won't be hidden automatically. Default value is 3000 ms</remarks>
        /// </summary>
        public int Duration { get; set; } = 3000;
    }
}