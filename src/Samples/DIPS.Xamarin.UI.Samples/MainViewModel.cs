using System.Windows.Input;
using DIPS.Xamarin.UI.Samples.Commands;
using DIPS.Xamarin.UI.Samples.Controls;
using DIPS.Xamarin.UI.Samples.Controls.DatePicker;
using DIPS.Xamarin.UI.Samples.Converters;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples
{
    public class MainViewModel
    {
        private readonly INavigation m_navigation;

        public MainViewModel(INavigation navigation)
        {
            m_navigation = navigation;
            NavigateToCommand = new Command<string>(NavigateTo);
        }

        public ICommand NavigateToCommand { get; }

        private void NavigateTo(string parameter)
        {
            switch (parameter)
            {
                case "Controls":
                    m_navigation.PushAsync(new ControlsPage());
                    break;
                case "Converters":
                    m_navigation.PushAsync(new ConvertersPage());
                    break;
                case "Async command":
                    m_navigation.PushAsync(new AsyncCommandPage());
                    break;
            }
        }
    }
}