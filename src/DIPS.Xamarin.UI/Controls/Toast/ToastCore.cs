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
    internal class ToastCore : IDisposable
    {
        public ToastCore()
        {
            if (Application.Current == null)
            {
                return;
            }

            Application.Current.PageAppearing += OnPageAppearing;
            Application.Current.PageDisappearing += OnPageDisappearing;
        }

        private CancellationTokenSource CancellationSource { get; set; } = new CancellationTokenSource();
        private Dictionary<string, Grid> ToastContainers { get; } = new Dictionary<string, Grid>();

        public void Dispose()
        {
            Application.Current.PageAppearing -= OnPageAppearing;
            Application.Current.PageDisappearing -= OnPageDisappearing;
            CancellationSource.Dispose();
        }

        private void OnPageAppearing(object sender, Page page)
        {
            var currentPage = GetCurrentContentPage();
            if (currentPage == page)
            {
                _ = GetToastContainerSettingUpIfNeededAsync();
            }
        }

        private void OnPageDisappearing(object sender, Page page)
        {
            if (page is ContentPage contentPage)
            {
                var currentPage = GetCurrentContentPage();
                CancellationSource.Cancel();
                _ = HideToast(contentPage, currentPage == contentPage);
            }
        }

        private async Task HideToast(ContentPage currentPage, bool removeContainer)
        {
            CancellationSource.Cancel();

            // get toast view, can be only one or none
            var toastContainer = FindByName(currentPage.Id.ToString());
            if (!(toastContainer?.Children.FirstOrDefault(w => w.GetType() == typeof(ToastView)) is ToastView toast))
            {
                // remove container
                if (removeContainer)
                {
                    UnregisterName(currentPage.Id.ToString());
                }

                return;
            }

            // animate toast
            if (toast.ToastOptions.OnBeforeHidingToast != null)
            {
                await toast.ToastOptions.OnBeforeHidingToast(toast);
            }

            // remove toast
            toastContainer.Children.Remove(toast);

            // remove container
            if (removeContainer)
            {
                UnregisterName(currentPage.Id.ToString());
            }
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

        private ToastView GetToast(string text, ToastOptions options, ToastLayout layout)
        {
            var toast = new ToastView
            {
                Text = text,
                ToastOptions = options,
                ToastLayout = layout,
                Margin = new Thickness(layout.HorizontalMargin, layout.PositionY, layout.HorizontalMargin, 0)
            };

            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) =>
            {
                toast.ToastOptions.ToastAction?.Invoke();
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

        private void UnregisterName(string name)
        {
            if (ToastContainers.ContainsKey(name))
            {
                ToastContainers.Remove(name);
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
            // get toast container
            var toastContainer = await GetToastContainerSettingUpIfNeededAsync();
            if (toastContainer == null)
            {
                return;
            }

            // toast view
            var toastView = GetToast(text, options ?? new ToastOptions(), layout ?? new ToastLayout());
            toastContainer.Children.Add(toastView);

            // animate toast
            if (toastView.ToastOptions.OnBeforeDisplayingToast != null)
            {
                await toastView.ToastOptions.OnBeforeDisplayingToast(toastView);
            }

            // hide toast
            if (toastView.ToastOptions.Duration >= 0)
            {
                await HideToastIn(toastView.ToastOptions.Duration);
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

            await HideToast(currentPage, false);
        }

        #endregion
    }
}