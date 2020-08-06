using System;
using System.Threading.Tasks;

namespace DIPS.Xamarin.UI.Controls.Toast
{
    /// <summary>
    ///     Toast control that would appear on top of the presented view
    /// </summary>
    public static class Toast
    {
        private static ToastCore ToastCore { get; } = new ToastCore();

        internal static void Initialize()
        {
            if (ToastCore == null) { } // allocate ToastCore object to ToastCore prop
        }

        /// <summary>
        ///     Displays the Toast control
        /// </summary>
        /// <param name="text">Text to be displayed in the Toast control</param>
        /// <param name="options">An <see cref="Action{ToastOptions}" /> to modify Toast options</param>
        /// <param name="layout">An <see cref="Action{ToastLayout}" /> to modify Toast layout</param>
        /// <returns>A void <c>Task</c></returns>
        public static async Task DisplayToast(string text, Action<ToastOptions> options, Action<ToastLayout> layout)
        {
            await ToastCore.DisplayToast(text, options, layout);
        }

        /// <summary>
        ///     Displays the Toast control
        /// </summary>
        /// <param name="text">Text to be displayed in the Toast control</param>
        /// <param name="options"><see cref="ToastOptions" /> to set for the Toast control</param>
        /// <param name="layout"><see cref="ToastLayout" /> to set for the Toast control</param>
        /// <returns>A void <c>Task</c></returns>
        public static async Task DisplayToast(string text, ToastOptions options = null, ToastLayout layout = null)
        {
            await ToastCore.DisplayToast(text, options, layout);
        }

        /// <summary>
        ///     Closes the displaying Toast control
        /// </summary>
        /// <returns>A void <c>Task</c></returns>
        public static async Task HideToast()
        {
            await ToastCore.HideToast();
        }
    }
}