using System.Windows.Input;
using DIPS.Xamarin.UI.Samples.Controls.DatePicker;
using DIPS.Xamarin.UI.Samples.Converters.ValueConverters;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Converters
{
    public class ConvertersViewModel
    {
        private readonly INavigation m_navigation;

        public ConvertersViewModel(INavigation navigation)
        {
            m_navigation = navigation;
            NavigateToCommand = new Command<string>(NavigateTo);
        }

        public ICommand NavigateToCommand { get; }

        private void NavigateTo(string parameter)
        {
            switch (parameter)
            {
                case "BoolToObject":
                    m_navigation.PushAsync(new BoolToObjectConverterPage());
                    break;
                case "InvertedBool":
                    m_navigation.PushAsync(new InvertedBoolConverterPage());
                    break;
                case "IsEmptyConverter":
                    m_navigation.PushAsync(new IsEmptyConverterPage());
                    break;
                case "DateTimeConverter":
                    m_navigation.PushAsync(new DateTimeConverterPage());
                    break;
                case "DateConverter":
                    m_navigation.PushAsync(new DateConverterPage());
                    break;
            }
        }
    }
}