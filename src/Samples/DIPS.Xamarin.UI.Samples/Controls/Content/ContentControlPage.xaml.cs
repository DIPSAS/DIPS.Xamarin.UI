using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Controls.Content
{
    public partial class ContentControlPage : ContentPage
    {
        public ContentControlPage()
        {
            InitializeComponent();
            BindingContext = new ContentControlViewModel();
        }
    }
}
