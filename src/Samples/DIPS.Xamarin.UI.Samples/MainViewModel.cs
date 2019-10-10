using System.Windows.Input;
using DIPS.Xamarin.UI.Samples.Controls;
using DIPS.Xamarin.UI.Samples.Controls.DatePicker;
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
            if (parameter.Equals("Controls"))
                m_navigation.PushAsync(new ControlsPage());
        }
    }
}