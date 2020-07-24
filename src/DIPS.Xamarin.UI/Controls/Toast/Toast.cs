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
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            if (!(currentPage is ContentPage))
            {
                return;
            }

            // try get toast container
            var toastContainer = FindByName(currentPage.Id.ToString());
            if (toastContainer != null) // found toast container
            {
                // check opened toasts, can be only one or none
                var oldToast = toastContainer.Children.FirstOrDefault(w => w.GetType() == typeof(ToastView));
                if (oldToast != null) // swap toast content
                {
                    CancellationSource.Cancel();
                    CancellationSource = new CancellationTokenSource();
                    if (HideToastIn > 0)
                    {
                        await HideToasterIn(HideToastIn);
                    }

                    return;
                }
            }
            else // no toast container
            {
                // create and register toast container
                toastContainer = new Grid();
                RegisterName(currentPage.Id.ToString(), toastContainer);

                // old content
                var oldContent = ((ContentPage)currentPage).Content;

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
                CancellationSource.Cancel();
                CancellationSource = new CancellationTokenSource();
                await HideToasterIn(HideToastIn);
            }
        }

        /// <summary>
        ///     Cancels the displaying Toast control
        /// </summary>
        /// <returns>A void <c>Task</c></returns>
        public async Task CancelToast()
        {
            // get current page
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            if (!(currentPage is ContentPage))
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

        private ToastView GetToast()
        {
            var toast = new ToastView();

            toast.SetBinding(ToastView.TextProperty, new Binding(nameof(Text), source: this));
            toast.SetBinding(ToastView.FontSizeProperty, new Binding(nameof(FontSize), source: this));
            toast.SetBinding(ToastView.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
            toast.SetBinding(ToastView.TextColorProperty, new Binding(nameof(TextColor), source: this));
            toast.SetBinding(ToastView.BackgroundColorProperty, new Binding(nameof(BackgroundColor), source: this));
            toast.SetBinding(ToastView.CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));
            toast.SetBinding(ToastView.PaddingProperty, new Binding(nameof(Padding), source: this));
            toast.SetBinding(ToastView.PositionYProperty, new Binding(nameof(PositionY), source: this));
            toast.SetBinding(ToastView.LineBreakModeProperty, new Binding(nameof(LineBreakMode), source: this));
            toast.SetBinding(ToastView.MaxLinesProperty, new Binding(nameof(MaxLines), source: this));

            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) =>
            {
                if (ToastAction == null)
                {
                    CancellationSource.Cancel();
                    CancellationSource = new CancellationTokenSource();
                    _ = HideToasterIn(0);
                }
                else
                {
                    ToastAction();
                }
            };
            toast.GestureRecognizers.Add(tapGesture);

            return toast;
        }

        private async Task HideToasterIn(int timeInSeconds)
        {
            await Task.Delay(timeInSeconds * 1000, CancellationSource.Token);

            await CancelToast();
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
        ///     Bindable property for <see cref="FontSize" />
        /// </summary>
        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize), typeof(double), typeof(Toast),
                Label.FontSizeProperty.DefaultValue,
                defaultValueCreator: FontSizeDefaultValueCreator);

        /// <summary>
        ///     Bindable property for <see cref="FontFamily" />
        /// </summary>
        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(Toast),
                Label.FontFamilyProperty.DefaultValue);

        /// <summary>
        ///     Bindable property for <see cref="LineBreakMode" />
        /// </summary>
        public static readonly BindableProperty LineBreakModeProperty = BindableProperty.Create(nameof(LineBreakMode),
            typeof(LineBreakMode), typeof(Toast), Label.LineBreakModeProperty.DefaultValue);

        /// <summary>
        ///     Bindable property for <see cref="MaxLines" />
        /// </summary>
        public static readonly BindableProperty MaxLinesProperty =
            BindableProperty.Create(nameof(MaxLines), typeof(int), typeof(Toast),
                Label.MaxLinesProperty.DefaultValue);

        /// <summary>
        ///     Bindable property for <see cref="TextColor" />
        /// </summary>
        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(Toast),
                Label.TextColorProperty.DefaultValue);

        /// <summary>
        ///     Bindable property for <see cref="BackgroundColor" />
        /// </summary>
        public static readonly BindableProperty BackgroundColorProperty =
            BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(Toast), Color.Default);

        /// <summary>
        ///     Bindable property for <see cref="CornerRadius" />
        /// </summary>
        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius),
            typeof(float), typeof(Toast), -1f,
            validateValue: OnCornerRadiusValidate);

        /// <summary>
        ///     Bindable property for <see cref="Padding" />
        /// </summary>
        public static readonly BindableProperty PaddingProperty =
            BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(Toast), new Thickness(5, 5, 5, 5));

        /// <summary>
        ///     Bindable property for <see cref="PositionY" />
        /// </summary>
        public static readonly BindableProperty PositionYProperty =
            BindableProperty.Create(nameof(PositionY), typeof(double), typeof(Toast), 10d);

        private static object FontSizeDefaultValueCreator(BindableObject bindable)
        {
            return Device.GetNamedSize(NamedSize.Default, typeof(ToastView));
        }

        private static bool OnCornerRadiusValidate(BindableObject bindable, object value)
        {
            if (value is float f)
            {
                return (int)f == -1 || f >= 0f;
            }

            return false;
        }

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
        ///     <remarks> If value is 0, toast won't be hide automatically </remarks>
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
        ///     Gets or sets the size of the font for the Toast. This is a bindable property.
        /// </summary>
        [TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        /// <summary>
        ///     Gets or sets the maximum number of lines allowed in the Toast. This is a bindable property.
        /// </summary>
        public int MaxLines
        {
            get => (int)GetValue(MaxLinesProperty);
            set => SetValue(MaxLinesProperty, value);
        }

        /// <summary>
        ///     Gets or sets the LineBreakMode for the Toast. This is a bindable property.
        /// </summary>
        public LineBreakMode LineBreakMode
        {
            get => (LineBreakMode)GetValue(LineBreakModeProperty);
            set => SetValue(LineBreakModeProperty, value);
        }

        /// <summary>
        ///     Gets or sets the font family to which the font for the Toast belongs. This is a bindable property.
        /// </summary>
        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        /// <summary>
        ///     Gets or sets the Color for the text of this Toast. This is a bindable property.
        /// </summary>
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the color which will fill the background of the Toast. This is a bindable property.
        /// </summary>
        public Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        /// <summary>
        ///     Gets or sets the corner radius of the Toast. This is a bindable property.
        /// </summary>
        public float CornerRadius
        {
            get => (float)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        ///     Gets or sets the inner padding of the Toast text.
        ///     <remarks>
        ///         The padding is the space between the bounds of a Toast and the bounding region into which its Text property
        ///         should be arranged into.
        ///     </remarks>
        /// </summary>
        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
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