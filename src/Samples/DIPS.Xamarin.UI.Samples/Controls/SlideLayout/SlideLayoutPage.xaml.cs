using System;
using System.Collections.Generic;

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
    }
}
