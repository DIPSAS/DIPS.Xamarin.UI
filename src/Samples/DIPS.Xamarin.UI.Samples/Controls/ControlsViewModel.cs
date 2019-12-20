using System.Windows.Input;
using DIPS.Xamarin.UI.Samples.Controls.DatePicker;
using DIPS.Xamarin.UI.Samples.Controls.Popup;
using DIPS.Xamarin.UI.Samples.Controls.RadioButtonGroup;
using DIPS.Xamarin.UI.Samples.Controls.TimePicker;
using DIPS.Xamarin.UI.Samples.Controls.TrendGraph;
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
            if (parameter.Equals("DatePicker")) m_navigation.PushAsync(new DatePickerPage(){Title = parameter});
            if (parameter.Equals("TimePicker")) m_navigation.PushAsync(new TimePickerPage() { Title = parameter });
            if (parameter.Equals("Popup")) m_navigation.PushAsync(new PopupPage() { Title = parameter });
            if (parameter.Equals("RadioButtonGroup")) m_navigation.PushAsync(new RadioButtonGroupPage() { Title = parameter });
            if (parameter.Equals("TrendGraph")) m_navigation.PushAsync(new TrendGraphPage() { Title = parameter });
        }
    }
}