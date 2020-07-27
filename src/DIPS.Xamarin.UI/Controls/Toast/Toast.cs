using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Toast
{
    /// <summary>
    ///     Toast control that would appear on top of the presented view
    /// </summary>
    public class Toast : BindableObject
    {
        private Toast()
        {
        }

        /// <summary>
        ///     Get the current instance of the Toast control
        /// </summary>
        public static Toast Current { get; } = new Toast();

        private CancellationTokenSource CancellationSource { get; set; } = new CancellationTokenSource();
        private Dictionary<string, Grid> ToastContainers { get; } = new Dictionary<string, Grid>();

        /// <summary>
        ///     Displays the Toast control
        /// </summary>
        /// <returns>A void <c>Task</c></returns>
        public async Task DisplayToast()
        {
            // get current page
            var currentPage = GetCurrentContentPage();
            if (currentPage == null)
            {
                return;
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
                ((ContentPage)currentPage).Content = toastContainer;
                toastContainer.Children.Add(oldContent);
            }

            // toast view
            var toastView = GetToast();
            toastView.Opacity = 0;
            toastContainer.Children.Add(toastView);

            // animate toast
            await toastView.FadeTo(1, (uint)AnimateFor, Easing.Linear);

            // hide toast
            if (HideToastIn > 0)
            {
                await CloseToastIn(HideToastIn);
            }
        }

        /// <summary>
        ///     Closes the displaying Toast control
        /// </summary>
        /// <returns>A void <c>Task</c></returns>
        public async Task CloseToast()
        {
            // get current page
            var currentPage = GetCurrentContentPage();
            if (currentPage == null)
            {
                return;
            }

            // get toast view, can be only one or none
            var toastContainer = FindByName(currentPage.Id.ToString());
            var toastView = toastContainer?.Children.FirstOrDefault(w => w.GetType() == typeof(ToastView));
            if (toastView == null)
            {
                return;
            }

            // animate toast
            await toastView.FadeTo(0, (uint)AnimateFor, Easing.Linear);

            // remove toast
            toastContainer.Children.Remove(toastView);
        }

        private static ContentPage? GetCurrentContentPage()
        {
            if (Application.Current.MainPage is ContentPage contentPage)
            {
                return contentPage;
            }

            if (Application.Current.MainPage is NavigationPage navigationPage)
            {
                if (navigationPage.CurrentPage.Navigation.ModalStack.Any())
                {
                    return navigationPage.CurrentPage.Navigation.ModalStack.Last() as ContentPage;
                }

                return navigationPage.CurrentPage as ContentPage;
            }

            if (Application.Current.MainPage is TabbedPage tabbedPage)
            {
                if (tabbedPage.CurrentPage is NavigationPage tabNavigationPage)
                {
                    if (tabNavigationPage.CurrentPage.Navigation.ModalStack.Any())
                    {
                        return tabNavigationPage.CurrentPage.Navigation.ModalStack.Last() as ContentPage;
                    }

                    return tabNavigationPage.CurrentPage as ContentPage;
                }

                return tabbedPage.CurrentPage as ContentPage;
            }

            return null;
        }

        private ToastView GetToast()
        {
            var toast = new ToastView();

            toast.SetBinding(ToastView.TextProperty, new Binding(nameof(Text), source: Current));
            toast.SetBinding(ToastView.PositionYProperty, new Binding(nameof(PositionY), source: Current));

            toast.SetBinding(ToastView.BackgroundColorProperty,
                new Binding(nameof(ToastLayout.BackgroundColor), source: Current.ToastLayout));
            toast.SetBinding(ToastView.CornerRadiusProperty,
                new Binding(nameof(ToastLayout.CornerRadius), source: Current.ToastLayout));
            toast.SetBinding(ToastView.FontFamilyProperty,
                new Binding(nameof(ToastLayout.FontFamily), source: Current.ToastLayout));
            toast.SetBinding(ToastView.FontSizeProperty,
                new Binding(nameof(ToastLayout.FontSize), source: Current.ToastLayout));
            toast.SetBinding(ToastView.HasShadowProperty,
                new Binding(nameof(ToastLayout.HasShadow), source: Current.ToastLayout));
            toast.SetBinding(ToastView.LineBreakModeProperty,
                new Binding(nameof(ToastLayout.LineBreakMode), source: Current.ToastLayout));
            toast.SetBinding(ToastView.MaxLinesProperty,
                new Binding(nameof(ToastLayout.MaxLines), source: Current.ToastLayout));
            toast.SetBinding(ToastView.PaddingProperty,
                new Binding(nameof(ToastLayout.Padding), source: Current.ToastLayout));
            toast.SetBinding(ToastView.TextColorProperty,
                new Binding(nameof(ToastLayout.TextColor), source: Current.ToastLayout));

            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) =>
            {
                if (ToastAction == null)
                {
                    _ = CloseToastIn(0);
                }
                else
                {
                    ToastAction();
                }
            };
            toast.GestureRecognizers.Add(tapGesture);

            return toast;
        }

        private async Task CloseToastIn(int timeInSeconds)
        {
            CancellationSource.Cancel();
            CancellationSource = new CancellationTokenSource();

            await Task.Delay(timeInSeconds * 1000, CancellationSource.Token);

            await CloseToast();
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

        #region Bindable Properties

        /// <summary>
        ///     Bindable property for <see cref="Text" />
        /// </summary>
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(Toast), Label.TextProperty.DefaultValue);

        /// <summary>
        ///     Bindable property for <see cref="ToastLayout" />
        /// </summary>
        public static readonly BindableProperty ToastLayoutProperty =
            BindableProperty.Create(nameof(ToastLayout), typeof(ToastLayout), typeof(Toast), new ToastLayout());

        /// <summary>
        ///     Bindable property for <see cref="PositionY" />
        /// </summary>
        public static readonly BindableProperty PositionYProperty =
            BindableProperty.Create(nameof(PositionY), typeof(double), typeof(Toast), 10d);

        #endregion

        #region Public Properties

        /// <summary>
        ///     Performs action on tapping the toast
        ///     <remarks> Will Override closing the toast on tapping </remarks>
        /// </summary>
        public Action? ToastAction { get; set; }

        /// <summary>
        ///     Animate the appearing and disappearing of the toast for the given milliseconds
        /// </summary>
        public int AnimateFor { get; set; } = 250;

        /// <summary>
        ///     Hide the toast automatically after the given seconds
        ///     <remarks> If value is 0, toast won't be hidden automatically </remarks>
        /// </summary>
        public int HideToastIn { get; set; } = 5;

        /// <summary>
        ///     Gets or sets the text for the Toast. This is a bindable property.
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        ///     Sets the Layout for the Toast. This is a bindable property.
        /// </summary>
        public ToastLayout ToastLayout
        {
            get => (ToastLayout)GetValue(ToastLayoutProperty);
            set => SetValue(ToastLayoutProperty, value);
        }

        /// <summary>
        ///     The vertical positioning of the toast in a percentage of the Main Page relative to the top margin of the Main
        ///     page. This is a bindable property.
        /// </summary>
        public double PositionY
        {
            get => (double)GetValue(PositionYProperty);
            set => SetValue(PositionYProperty, value);
        }

        #endregion
    }
}