using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DIPS.Xamarin.UI.Controls.Toast
{
    /// <summary>
    ///     Toast control that would appear on top of the presented view
    /// </summary>
    public static class Toast
    {
        private static ToastCore? ToastCore { get; set; }

        private static void Initialize()
        {
            ToastCore?.Dispose();
            ToastCore = new ToastCore();
        }

        /// <summary>
        ///     Display a Toast
        /// </summary>
        /// <param name="text">Text to be displayed inside the toast</param>
        /// <param name="options">An <see cref="Action{ToastOptions}" /> to modify Toast options</param>
        /// <param name="layout">An <see cref="Action{ToastLayout}" /> to modify Toast layout</param>
        /// <returns>A void <c>Task</c></returns>
        public static async Task DisplayToast(string text, Action<ToastOptions> options, Action<ToastLayout> layout)
        {
            try
            {
                if (ToastCore == null)
                {
                    Initialize();
                }

                await ToastCore?.DisplayToast(text, options, layout)!;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }

        /// <summary>
        ///     Display a Toast
        /// </summary>
        /// <param name="text">Text to be displayed inside the toast</param>
        /// <param name="options"><see cref="ToastOptions" /> to set for the Toast</param>
        /// <param name="layout"><see cref="ToastLayout" /> to set for the Toast</param>
        /// <returns>A void <c>Task</c></returns>
        public static async Task DisplayToast(string text, ToastOptions? options = null, ToastLayout? layout = null)
        {
            try
            {
                if (ToastCore == null)
                {
                    Initialize();
                }

                await ToastCore?.DisplayToast(text, options ?? new ToastOptions(), layout ?? new ToastLayout())!;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }

        /// <summary>
        ///     Hide the displaying Toast
        /// </summary>
        /// <returns>A void <c>Task</c></returns>
        public static async Task HideToast()
        {
            try
            {
                if (ToastCore == null)
                {
                    Initialize();
                }

                await ToastCore?.HideToast()!;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }
    }
}