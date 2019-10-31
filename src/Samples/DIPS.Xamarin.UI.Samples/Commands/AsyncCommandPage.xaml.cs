using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Commands
{
    public partial class AsyncCommandPage : ContentPage
    {
        public AsyncCommandPage()
        {
            InitializeComponent();
            BindingContext = new AsyncCommandViewModel();
        }
    }
}
