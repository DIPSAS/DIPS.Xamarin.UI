using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ToastControl = DIPS.Xamarin.UI.Controls.Toast.Toast;

namespace DIPS.Xamarin.UI.Samples.Controls.Toast
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToastPage : ContentPage
    {
        public ToastPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new DIPS.Xamarin.UI.Samples.Controls.Sheet.SheetPage());
        }
    }
}