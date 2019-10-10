using System.ComponentModel;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public void OnStart()
        {
            BindingContext = new MainViewModel(Application.Current.MainPage.Navigation);
        }
    }
}