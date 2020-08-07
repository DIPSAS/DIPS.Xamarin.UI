using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Internal.Xaml;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Toast
{
    internal class ToastCore
    {
        public ToastCore()
        {
            Application.Current.PageAppearing -= OnPageAppearing;
            Application.Current.PageAppearing += OnPageAppearing;

            Application.Current.PageDisappearing -= OnPageDisappearing;
            Application.Current.PageDisappearing += OnPageDisappearing;
        }

        private CancellationTokenSource CancellationSource { get; set; } = new CancellationTokenSource();
        private Dictionary<string, Grid> ToastContainers { get; } = new Dictionary<string, Grid>();
        private ToastLayout ToastLayout { get; set; }
        private ToastOptions ToastOptions { get; set; }

        private void OnPageDisappearing(object sender, Page page)
        {
            if (page is ContentPage contentPage)
            {
                CancellationSource.Cancel();
                _ = HideToast(contentPage);
            }
        }

        private void OnPageAppearing(object sender, Page e)
        {
            Initialize();
        }

        /// <summary>
        ///     Set Toast container in Page Content on Page load
        /// </summary>
        private void Initialize()
        {
            _ = GetToastContainerSettingUpIfNeededAsync();
        }

        private async Task HideToast(ContentPage currentPage)
        {
            CancellationSource.Cancel();

            // get toast view, can be only one or none
            var toastContainer = FindByName(currentPage.Id.ToString());
            var toastView = toastContainer?.Children.FirstOrDefault(w => w.GetType() == typeof(ToastView));
            if (toastView == null)
            {
                return;
            }

            // animate toast
            if (ToastOptions.OnBeforeHidingToast != null)
            {
                await ToastOptions.OnBeforeHidingToast((ToastView)toastView);
            }

            // remove toast
            toastContainer.Children.Remove(toastView);
        }

        private async Task<Grid?> GetToastContainerSettingUpIfNeededAsync()
        {
            // get current page
            var currentPage = GetCurrentContentPage();
            if (currentPage == null)
            {
                return null;
            }

            // try get toast container
            var toastContainer = FindByName(currentPage.Id.ToString());
            if (toastContainer != null) // found toast container
            {
                // check opened toasts, can be only one or none
                var oldToast = toastContainer.Children.FirstOrDefault(w => w.GetType() == typeof(ToastView));
                if (oldToast != null) // close old toast
                {
                    CancellationSource.Cancel();
                    toastContainer.Children.Remove(oldToast);
                }
            }
            else // no toast container
            {
                // create and register toast container
                toastContainer = new Grid();
                RegisterName(currentPage.Id.ToString(), toastContainer);

                // old content
                var oldContent = currentPage.Content;

                // set new content
                await MainThread.InvokeOnMainThreadAsync(() => { currentPage.Content = toastContainer; });
                toastContainer.Children.Add(oldContent);
            }

            return toastContainer;
        }

        private static ContentPage? GetCurrentContentPage()
        {
            if (Application.Current.MainPage is ContentPage contentPage)
            {
                return contentPage;
            }

            if (Application.Current.MainPage is NavigationPage navigationPage)
            {
                if (navigationPage.CurrentPage.Navigation.ModalStack.LastOrDefault() is ContentPage modalContentPage)
                {
                    return modalContentPage;
                }

                if (navigationPage.CurrentPage is ContentPage navContentPage)
                {
                    return navContentPage;
                }
            }

            if (Application.Current.MainPage is TabbedPage tabbedPage)
            {
                if (tabbedPage.CurrentPage is NavigationPage tabNavigationPage)
                {
                    if (tabNavigationPage.CurrentPage.Navigation.ModalStack.LastOrDefault() is ContentPage
                        modalContentPage)
                    {
                        return modalContentPage;
                    }

                    if (tabNavigationPage.CurrentPage is ContentPage contentNavPage)
                    {
                        return contentNavPage;
                    }
                }

                if (tabbedPage.CurrentPage is ContentPage tabContentPage)
                {
                    return tabContentPage;
                }
            }

            throw new NotSupportedException(
                $"Cannot display the Toast. Toast could not find an underlying {typeof(ContentPage)}");
        }

        private ToastView GetToast(string text)
        {
            var toast = new ToastView
            {
                BackgroundColor = ToastLayout.BackgroundColor,
                CornerRadius = ToastLayout.CornerRadius,
                FontFamily = ToastLayout.FontFamily,
                FontSize = ToastLayout.FontSize,
                HasShadow = ToastLayout.HasShadow,
                LineBreakMode = ToastLayout.LineBreakMode,
                MaxLines = ToastLayout.MaxLines,
                Padding = ToastLayout.Padding,
                Margin = new Thickness(ToastLayout.HorizontalMargin, ToastLayout.PositionY,
                    ToastLayout.HorizontalMargin, 0),
                Text = text,
                TextColor = ToastLayout.TextColor
            };

            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) =>
            {
                ToastOptions.ToastAction?.Invoke();
            };
            toast.GestureRecognizers.Add(tapGesture);

            return toast;
        }

        private async Task HideToastIn(int timeInMilliseconds)
        {
            CancellationSource.Cancel();
            CancellationSource = new CancellationTokenSource();

            await Task.Delay(timeInMilliseconds, CancellationSource.Token);

            await HideToast();
        }

        private void RegisterName(string name, Grid container)
        {
            if (!ToastContainers.ContainsKey(name))
            {
                ToastContainers[name] = container;
            }
        }

        private Grid? FindByName(string name)
        {
            return ToastContainers.ContainsKey(name) ? ToastContainers[name] : null;
        }

        #region Internal Methods

        internal async Task DisplayToast(string text, Action<ToastOptions> options, Action<ToastLayout> layout)
        {
            var toastOptions = new ToastOptions();
            options(toastOptions);
            var layoutOptions = new ToastLayout();
            layout(layoutOptions);

            await DisplayToast(text, toastOptions, layoutOptions);
        }

        internal async Task DisplayToast(string text, ToastOptions options = null, ToastLayout layout = null)
        {
            // set properties
            ToastOptions = options ?? new ToastOptions();
            ToastLayout = layout ?? new ToastLayout();

            // get toast container
            var toastContainer = await GetToastContainerSettingUpIfNeededAsync();
            if (toastContainer == null)
            {
                return;
            }

            // toast view
            var toastView = GetToast(text);
            toastContainer.Children.Add(toastView);

            // animate toast
            if (ToastOptions.OnBeforeDisplayingToast != null)
            {
                await ToastOptions.OnBeforeDisplayingToast(toastView);
            }

            // hide toast
            if (ToastOptions.Duration >= 0)
            {
                await HideToastIn(ToastOptions.Duration);
            }
        }

        internal async Task HideToast()
        {
            // get current page
            var currentPage = GetCurrentContentPage();
            if (currentPage == null)
            {
                return;
            }

            await HideToast(currentPage);
        }

        #endregion
    }
}