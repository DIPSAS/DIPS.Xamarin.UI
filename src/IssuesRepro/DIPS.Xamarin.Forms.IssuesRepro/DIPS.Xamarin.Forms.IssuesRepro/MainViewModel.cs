using System.Windows.Input;
using Xamarin.Forms;

namespace DIPS.Xamarin.Forms.IssuesRepro
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            NavigationCommand = new Command<string>(Navigate);
        }

        public ICommand NavigationCommand { get; }

        private void Navigate(string obj)
        {
            if (obj.Equals("64"))
                Application.Current.MainPage.Navigation.PushAsync(new Github64.Github64());
        }
    }
}