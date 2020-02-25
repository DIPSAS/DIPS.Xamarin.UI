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
            if (obj.Equals("86"))
                Application.Current.MainPage.Navigation.PushAsync(new Github86.Github86());
            if (obj.Equals("112"))
                Application.Current.MainPage.Navigation.PushAsync(new Github112.Github112());
            if (obj.Equals("123"))
                Application.Current.MainPage.Navigation.PushAsync(new Github123.Github123());
            if (obj.Equals("137"))
                Application.Current.MainPage.Navigation.PushAsync(new NewIssue.Github137());
            if (obj.Equals("142"))
                Application.Current.MainPage.Navigation.PushAsync(new Github142.Github142());
        }
    }
}