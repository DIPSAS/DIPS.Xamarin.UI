using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace DIPS.Xamarin.UI.Controls.Toast
{
    /// <summary>
    ///     Toast control that would appear on top of the presented view
    /// </summary>
    [SuppressMessage("ReSharper", "RedundantCatchClause")]
    public static class Toast
    {
        private static ToastCore ToastCore { get; } = new ToastCore();

        internal static void Initialize()
        {
            if (ToastCore == null) { } // allocate ToastCore object to ToastCore property
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
            try
            {
                await ToastCore.DisplayToast(text, options, layout);
                throw new Exception("Hello, this is a test!");
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
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
            try
            {
                await ToastCore.DisplayToast(text, options, layout);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }

        /// <summary>
        ///     Closes the displaying Toast control
        /// </summary>
        /// <returns>A void <c>Task</c></returns>
        public static async Task HideToast()
        {
            try
            {
                await ToastCore.HideToast();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }
    }
}