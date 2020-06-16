using System.Windows.Input;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Extensions
{
    public class ExtensionsViewModel
    {
        private INavigation m_navigation;

        public ExtensionsViewModel(INavigation navigation)
        {
            m_navigation = navigation;
            NavigateToCommand = new Command<string>(NavigateTo);
        }

        public ICommand NavigateToCommand { get; }

        private async void NavigateTo(string parameter)
        {
            if (parameter.Equals("DIPSColor"))
                await m_navigation.PushAsync(new DIPSColorMarkupExtensionsPage { Title = parameter });
        }
    }
}
