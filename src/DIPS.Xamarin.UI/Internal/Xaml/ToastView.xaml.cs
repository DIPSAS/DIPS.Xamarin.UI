using System.ComponentModel;
using DIPS.Xamarin.UI.Controls.Toast;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Internal.Xaml
{
    /// <summary>
    ///     Toaster control that would appear on top of the presented view
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public partial class ToastView : ContentView
    {
        /// <inheritdoc />
        internal ToastView()
        {
            InitializeComponent();
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
        ///     Gets or sets the layout for the Toast. This is a bindable property.
        /// </summary>
        public ToastLayout ToastLayout
        {
            get => (ToastLayout)GetValue(ToastLayoutProperty);
            set => SetValue(ToastLayoutProperty, value);
        }

        /// <summary>
        ///     Gets or sets the options for the Toast. This is a bindable property.
        /// </summary>
        public ToastOptions ToastOptions
        {
            get => (ToastOptions)GetValue(ToastOptionsProperty);
            set => SetValue(ToastOptionsProperty, value);
        }

        #endregion

        #region Bindable Properties

        /// <summary>
        ///     Bindable property for <see cref="Text" />
        /// </summary>
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(ToastView), Label.TextProperty.DefaultValue);

        /// <summary>
        ///     Bindable property for <see cref="ToastLayout" />
        /// </summary>
        public static readonly BindableProperty ToastLayoutProperty =
            BindableProperty.Create(nameof(ToastLayout), typeof(ToastLayout), typeof(ToastView), new ToastLayout());

        /// <summary>
        ///     Bindable property for <see cref="ToastOptions" />
        /// </summary>
        public static readonly BindableProperty ToastOptionsProperty =
            BindableProperty.Create(nameof(ToastOptions), typeof(ToastOptions), typeof(ToastView), new ToastOptions());

        #endregion
    }
}