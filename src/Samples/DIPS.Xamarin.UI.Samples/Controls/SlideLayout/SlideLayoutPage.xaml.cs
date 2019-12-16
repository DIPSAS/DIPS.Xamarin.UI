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
    }
}
