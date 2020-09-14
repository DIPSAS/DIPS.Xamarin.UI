using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DIPS.Xamarin.Forms.IssuesRepro.Github234
{
    public partial class Github234FloatingButtonPage : ContentPage
    {
        public Github234FloatingButtonPage()
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
            await Task.Delay(100);
            Application.Current.MainPage = Github234.CurrentMain;
        }
    }
}
