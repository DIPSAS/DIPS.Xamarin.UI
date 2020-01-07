using System;
using System.ComponentModel;
using System.Windows.Input;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Controls.Popup
{
    public partial class PopupPage : ContentPage
    {

        public PopupPage()
        {
            InitializeComponent();

            BindingContext = new ViewModel();
        }
    }
}
