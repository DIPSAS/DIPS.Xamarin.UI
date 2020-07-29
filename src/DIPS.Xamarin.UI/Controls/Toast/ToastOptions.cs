using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Toast
{
    public class ToastOptions : BindableObject
    {
        #region Public Properties

        /// <summary>
        ///     Performs action on tapping the toast
        ///     <remarks> Will Override closing the toast on tapping </remarks>
        /// </summary>
        public Action? ToastAction { get; set; }

        /// <summary>
        ///     Animation on displaying the Toast
        /// </summary>
        public Func<ToastView, Task> DisplayAnimation { get; set; }

        /// <summary>
        ///     Animation on closing the Toast
        /// </summary>
        public Func<ToastView, Task> CloseAnimation { get; set; }

        /// <summary>
        ///     Hide the toast automatically after the given milliseconds
        ///     <remarks> If value is 0, toast won't be hidden automatically </remarks>
        /// </summary>
        public int HideToastIn { get; set; }

        #endregion
    }
}