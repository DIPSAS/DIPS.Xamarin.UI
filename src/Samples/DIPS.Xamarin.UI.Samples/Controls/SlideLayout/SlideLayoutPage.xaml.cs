using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Controls.SlideLayout
{
    public partial class SlideLayoutPage : ContentPage
    {
        public SlideLayoutPage()
        {
            InitializeComponent();
            BindingContext = new SlideLayoutViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((SlideLayoutViewModel)BindingContext).Initialize();
        }

        private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            frame.FadeTo(1, 150);
            await Task.Delay(1000);
            frame.FadeTo(0, 150);
        }
    }
}
