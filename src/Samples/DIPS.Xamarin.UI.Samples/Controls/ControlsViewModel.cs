using System.Windows.Input;
using DIPS.Xamarin.UI.Samples.Controls.DatePicker;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Controls
{
    public class ControlsViewModel
    {
        private readonly INavigation m_navigation;

        public ControlsViewModel(INavigation navigation)
        {
            m_navigation = navigation;
            NavigateToCommand = new Command<string>(NavigateTo);
        }

        public ICommand NavigateToCommand { get; }

        private void NavigateTo(string parameter)
        {
            if (parameter.Equals("DatePicker")) m_navigation.PushAsync(new DatePickerPage());
        }
    }
}