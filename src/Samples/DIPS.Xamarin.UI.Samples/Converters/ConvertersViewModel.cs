using System.Windows.Input;
using DIPS.Xamarin.UI.Samples.Converters.MultiValueConverters;
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
                case "BoolToDouble":
                    m_navigation.PushAsync(new BoolToDoubleConverterPage(){Title = parameter});
                    break;
                case "BoolToObject":
                    m_navigation.PushAsync(new BoolToObjectConverterPage(){Title = parameter});
                    break;
                case "InvertedBool":
                    m_navigation.PushAsync(new InvertedBoolConverterPage() { Title = parameter });
                    break;
                case "IsEmptyConverter":
                    m_navigation.PushAsync(new IsEmptyConverterPage() { Title = parameter });
                    break;
                case "IsEmptyToObjectConverter":
                    m_navigation.PushAsync(new IsEmptyToObjectConverterPage() { Title = parameter });
                    break;
                case "DateConverter":
                    m_navigation.PushAsync(new DateConverterPage() { Title = parameter });
                    break;
                case "TimeConverter":
                    m_navigation.PushAsync(new TimeConverterPage() { Title = parameter });
                    break;
                case "DateAndTimeConverter":
                    m_navigation.PushAsync(new DateAndTimeConverterPage() { Title = parameter });
                    break;
                case "MultiplicationConverter":
                    m_navigation.PushAsync(new MultiplicationConverterPage() { Title = parameter });
                    break;
                case "AdditionConverter":
                    m_navigation.PushAsync(new AdditionConverterPage() { Title = parameter });
                    break;
                case "StringCaseConverter":
                    m_navigation.PushAsync(new StringCaseConverterPage() { Title = parameter });
                break;
                case "TypeToObjectConverter":
                    m_navigation.PushAsync(new TypeToObjectConverterPage() { Title = parameter });
                    break;
                case "LogicalExpressionConverter":
                    m_navigation.PushAsync(new LogicalExpressionConverterPage() { Title = parameter });
                    break;
                case "PositionInListConverter":
                    m_navigation.PushAsync(new PositionInListConverterPage() { Title = parameter });
                    break;
                case "ObjectToBoolConverter":
                    m_navigation.PushAsync(new ObjectToBoolConverterPage() { Title = parameter });
                    break;
            }
        }
    }
}