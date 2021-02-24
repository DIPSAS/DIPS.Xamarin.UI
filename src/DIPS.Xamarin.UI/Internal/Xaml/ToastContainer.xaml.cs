using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Internal.Xaml
{
    /// <summary>
    ///     Toast container that act as a wrapper to the presented view
    /// </summary>
    [ContentProperty(nameof(MainContent))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToastContainer : ContentView
    {
        public ToastContainer()
        {
            InitializeComponent();
        }

        #region Bindable Properties

        /// <summary>
        ///     Bindable property for <see cref="MainContent" />
        /// </summary>
        public static readonly BindableProperty MainContentProperty = BindableProperty.Create(
            nameof(MainContent),
            typeof(View),
            typeof(ToastContainer));

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the MainContent for the ToastContainer. This is a bindable property.
        /// </summary>
        public View MainContent
        {
            get => (View)GetValue(MainContentProperty);
            set => SetValue(MainContentProperty, value);
        }
        #endregion
    }
}