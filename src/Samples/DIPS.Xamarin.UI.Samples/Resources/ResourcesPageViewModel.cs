using System.Windows.Input;
using DIPS.Xamarin.UI.Samples.Resources.Colors;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Resources
{
    public class ResourcesPageViewModel
    {
        private readonly INavigation m_navigation;

        public ResourcesPageViewModel(INavigation navigation)
        {
            m_navigation = navigation;
            NavigateToCommand = new Command<string>(NavigateTo);
        }

        public ICommand NavigateToCommand { get; }
            
        private void NavigateTo(string parameter)
        {
            if (parameter.Equals("Colors"))
                m_navigation.PushAsync(new ColorsPage());

        }
    }
}