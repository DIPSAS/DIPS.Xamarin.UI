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
            Application.Current.PageAppearing -= OnPageAppearing;
            Application.Current.PageAppearing += OnPageAppearing;

            Application.Current.PageDisappearing -= OnPageDisappearing;
            Application.Current.PageDisappearing += OnPageDisappearing;
        }

        /// <summary>
        ///     Get the current instance of the Toast control
        /// </summary>
        public static Toast Current { get; } = new Toast();

        private CancellationTokenSource CancellationSource { get; set; } = new CancellationTokenSource();
        private Dictionary<string, Grid> ToastContainers { get; } = new Dictionary<string, Grid>();

        private void OnPageDisappearing(object sender, Page page)
        {
            if (page is ContentPage contentPage)
            {
                CancellationSource.Cancel();
                _ = CloseToast(contentPage);
            }
        }

        private void OnPageAppearing(object sender, Page e)
        {
            Initialize();
        }

        /// <summary>
        ///     Initialize Toast control.
        /// </summary>
        public void Initialize()
        {
            GetToastContainerSettingUpIfNeeded();
        }

        /// <summary>
        ///     Displays the Toast control
        /// </summary>
        /// <returns>A void <c>Task</c></returns>
        public async Task
            DisplayToast(string text, ToastOptions toastOptions = null,
                ToastLayout toastLayout = null) // string text, ToastLayout toastLayout = null
        {
            // set properties
            Text = text;
            ToastOptions = toastOptions ?? new ToastOptions();
            ToastLayout = toastLayout ?? new ToastLayout();

            // get toast container
            var toastContainer = GetToastContainerSettingUpIfNeeded();
            if (toastContainer == null)
            {
                return;
            }

            // toast view
            var toastView = GetToast();
            toastContainer.Children.Add(toastView);

            // animate toast
            if (ToastOptions.DisplayAnimation != null)
            {
                await ToastOptions.DisplayAnimation(toastView);
            }

            // hide toast
            if (ToastOptions.HideToastIn > 0)
            {
                await CloseToastIn(ToastOptions.HideToastIn);
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

            await CloseToast(currentPage);
        }

        private async Task CloseToast(ContentPage currentPage)
        {
            // get toast view, can be only one or none
            var toastContainer = FindByName(currentPage.Id.ToString());
            var toastView = toastContainer?.Children.FirstOrDefault(w => w.GetType() == typeof(ToastView));
            if (toastView == null)
            {
                return;
            }

            // animate toast
            if (ToastOptions?.CloseAnimation != null)
            {
                await ToastOptions.CloseAnimation((ToastView)toastView);
            }

            // remove toast
            toastContainer.Children.Remove(toastView);
        }

        private Grid? GetToastContainerSettingUpIfNeeded()
        {
            var x = Application.Current.MainPage;

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
                currentPage.Content = toastContainer;
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

            toast.SetBinding(ToastView.BackgroundColorProperty,
                new Binding(nameof(ToastLayout.BackgroundColor), source: ToastLayout));
            toast.SetBinding(ToastView.CornerRadiusProperty,
                new Binding(nameof(ToastLayout.CornerRadius), source: ToastLayout));
            toast.SetBinding(ToastView.FontFamilyProperty,
                new Binding(nameof(ToastLayout.FontFamily), source: ToastLayout));
            toast.SetBinding(ToastView.FontSizeProperty,
                new Binding(nameof(ToastLayout.FontSize), source: ToastLayout));
            toast.SetBinding(ToastView.HasShadowProperty,
                new Binding(nameof(ToastLayout.HasShadow), source: ToastLayout));
            toast.SetBinding(ToastView.LineBreakModeProperty,
                new Binding(nameof(ToastLayout.LineBreakMode), source: ToastLayout));
            toast.SetBinding(ToastView.MaxLinesProperty,
                new Binding(nameof(ToastLayout.MaxLines), source: ToastLayout));
            toast.SetBinding(ToastView.PaddingProperty,
                new Binding(nameof(ToastLayout.Padding), source: ToastLayout));
            toast.SetBinding(ToastView.PositionYProperty,
                new Binding(nameof(ToastLayout.PositionY), source: ToastLayout));
            toast.SetBinding(ToastView.TextColorProperty,
                new Binding(nameof(ToastLayout.TextColor), source: ToastLayout));

            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) =>
            {
                if (ToastOptions?.ToastAction == null)
                {
                    _ = CloseToastIn(0);
                }
                else
                {
                    ToastOptions.ToastAction();
                }
            };
            toast.GestureRecognizers.Add(tapGesture);

            return toast;
        }

        private async Task CloseToastIn(int timeInMilliseconds)
        {
            CancellationSource.Cancel();
            CancellationSource = new CancellationTokenSource();

            await Task.Delay(timeInMilliseconds, CancellationSource.Token);

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

        #region Public Properties

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
        ///     Sets the Options for the Toast.
        /// </summary>
        public ToastOptions ToastOptions
        {
            get;
            set;
        }

        #endregion

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

        #endregion
    }
}