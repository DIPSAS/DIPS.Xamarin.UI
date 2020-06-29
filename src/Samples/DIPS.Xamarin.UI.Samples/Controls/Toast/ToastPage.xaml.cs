using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DIPS.Xamarin.UI.Controls.Toast;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Color = System.Drawing.Color;

namespace DIPS.Xamarin.UI.Samples.Controls.Toast
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToastPage : ContentPage
    {
        public ToastPage()
        {
            InitializeComponent();
            
            ShowToastCommand = new Command(() =>
            {
                // var toaster = new Toaster();
                // toaster.ShowToast();
                
                Toaster.Current.ShowToast();
            });
        }
        
        void OnButtonClicked(object sender, EventArgs args)
        {
            // var toaster = new Toaster();
            // toaster.ShowToast();

            Toaster.Current.Text = "Hello, Jupiter!";
            Toaster.Current.FontSize = 13;
            Toaster.Current.TextColor = Color.White;
            Toaster.Current.BackgroundColor = Color.DodgerBlue;
            Toaster.Current.CornerRadius = 8;
            Toaster.Current.Padding = new Thickness(20, 10);
            Toaster.Current.PositionY = 30;
            
            Toaster.Current.ShowToaster();
        }

        public ICommand ShowToastCommand { get; set; }
    }
}