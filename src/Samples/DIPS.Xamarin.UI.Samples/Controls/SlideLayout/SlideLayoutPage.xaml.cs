using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Controls.SlideLayout
{
    public partial class SlideLayoutPage : ContentPage
    {
        public SlideLayoutPage()
        {
            BindingContext = new SlideLayoutViewModel();

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((SlideLayoutViewModel)BindingContext).Initialize();
        }
    }
}
