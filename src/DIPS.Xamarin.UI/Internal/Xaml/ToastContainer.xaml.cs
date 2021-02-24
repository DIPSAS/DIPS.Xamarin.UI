using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Internal.Xaml
{
    [ContentProperty(nameof(MainContent))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToastContainer : ContentView
    {
        public static readonly BindableProperty MainContentProperty = BindableProperty.Create(
            nameof(MainContent),
            typeof(View),
            typeof(ToastContainer));

        public ToastContainer()
        {
            InitializeComponent();
        }

        public View MainContent
        {
            get => (View)GetValue(MainContentProperty);
            set => SetValue(MainContentProperty, value);
        }
    }
}