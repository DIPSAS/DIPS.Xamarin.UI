using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.Forms.IssuesRepro.Github234
{
    [Issue(234)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Github234 : ContentPage
    {
        public static Page CurrentMain;
        public Github234()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Button_Clicked(null, EventArgs.Empty);
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            CurrentMain = Application.Current.MainPage;
            try
            {
                var p = await Task.Run(async () =>
                {
                    Thread.Sleep(10);
                    var page = new Github234FloatingButtonPage();
                    return page;
                });
                Application.Current.MainPage = p;
            }
            catch(Exception exception)
            {
                Application.Current.MainPage = new ContentPage
                {
                    Content = new Label
                    {
                        Text = exception.Message + "\n" + exception.StackTrace,
                    }
                };
            }
        }
    }
}
