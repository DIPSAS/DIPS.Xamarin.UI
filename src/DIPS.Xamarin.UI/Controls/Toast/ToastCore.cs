using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Extensions;
using DIPS.Xamarin.UI.Internal.Xaml;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Toast
{
    internal class ToastCore : IDisposable
    {
        private ContentPage m_currentPageWithToast;

        private CancellationTokenSource CancellationSource { get; set; } = new CancellationTokenSource();
        private Dictionary<string, Grid> ToastContainers { get; } = new Dictionary<string, Grid>();

        public void Dispose()
        {
            CancellationSource.Dispose();
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

        private Grid GetToastContainer()
        {
            // get current page
            m_currentPageWithToast = GetCurrentContentPage();

            // try get toast container
            var toastContainer = FindByName(m_currentPageWithToast.Id.ToString());
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
                RegisterName(m_currentPageWithToast.Id.ToString(), toastContainer);

                // old content
                var oldContent = m_currentPageWithToast.Content;

                // set new content
                m_currentPageWithToast.Content = toastContainer;
                toastContainer.Children.Add(oldContent);
            }

            m_currentPageWithToast.Disappearing += OnPageDisappearing;

            return toastContainer;
        }

        private void OnPageDisappearing(object sender, EventArgs e)
        {
            m_currentPageWithToast.Disappearing -= OnPageDisappearing;
            _ = HideToast(m_currentPageWithToast, false);
        }

        private static ContentPage GetCurrentContentPage()
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

        private static ToastView GetToast(string text, ToastOptions options, ToastLayout layout)
        {
            var toast = new ToastView
            {
                Text = text,
                ToastOptions = options,
                ToastLayout = layout,
                Margin = new Thickness(layout.HorizontalMargin, layout.PositionY, layout.HorizontalMargin, 0)
            };

            if (toast.ToastOptions.ToastAction != null)
            {
                toast.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command((toast.ToastOptions.ToastAction)) });
            }

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

        internal async Task DisplayToast(string text, ToastOptions options, ToastLayout layout)
        {
            // get toast container
            var toastContainer = GetToastContainer();

            // toast view
            var toastView = GetToast(text, options, layout);
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

            await HideToast(currentPage, false);
        }

        #endregion
    }
}