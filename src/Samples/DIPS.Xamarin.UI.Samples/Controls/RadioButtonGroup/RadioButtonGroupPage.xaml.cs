using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Controls.RadioButtonGroup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RadioButtonGroupPage : ContentPage
    {
        public RadioButtonGroupPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is RadioButtonGroupPageViewModel RadioButtonPageViewModel)
            {
                RadioButtonPageViewModel.Initialize();
            }
        }
    }
}