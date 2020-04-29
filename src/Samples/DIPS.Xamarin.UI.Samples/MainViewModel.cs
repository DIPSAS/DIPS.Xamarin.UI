using System.Windows.Input;
using DIPS.Xamarin.UI.Samples.Commands;
using DIPS.Xamarin.UI.Samples.Controls;
using DIPS.Xamarin.UI.Samples.Controls.DatePicker;
using DIPS.Xamarin.UI.Samples.Converters;
using DIPS.Xamarin.UI.Samples.Resources;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples
{
    public class MainViewModel
    {
        

        public MainViewModel()
        {

            NavigateToCommand = new Command<string>(NavigateTo);
        }

        public ICommand NavigateToCommand { get; }

        private void NavigateTo(string parameter)
        {
            var navigation = Application.Current.MainPage.Navigation;
            switch (parameter)
            {
                case "Controls":
                    navigation.PushAsync(new ControlsPage());
                    break;
                case "Resources":
                    navigation.PushAsync(new ResourcesPage());
                    break;
                case "Converters":
                    navigation.PushAsync(new ConvertersPage());
                    break;
                case "GitHub":
                    Browser.OpenAsync("https://github.com/DIPSAS/DIPS.Xamarin.UI", BrowserLaunchMode.External);
                    //m_navigation.PushAsync(new AsyncCommandPage());
                    break;
            }
        }
    }
}