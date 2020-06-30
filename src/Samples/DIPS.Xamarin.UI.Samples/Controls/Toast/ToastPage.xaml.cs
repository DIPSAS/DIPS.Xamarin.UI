using System;
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
        private ICommand m_showToastCommand;

        public ToastPage()
        {
            InitializeComponent();

            ConfigureToast();

            ShowToastCommand = new Command(parameter =>
            {
                Toaster.Current.Text = parameter.ToString();

                Toaster.Current.ShowToaster();
            });
        }

        public ICommand ShowToastCommand
        {
            get => m_showToastCommand;
            set
            {
                m_showToastCommand = value;
                OnPropertyChanged(nameof(ShowToastCommand));
            }
        }

        private static void ConfigureToast()
        {
            Toaster.Current.FontSize = 13;
            Toaster.Current.TextColor = Color.White;
            Toaster.Current.BackgroundColor = Color.DodgerBlue;
            Toaster.Current.CornerRadius = 8;
            Toaster.Current.Padding = new Thickness(20, 10);
            Toaster.Current.PositionY = 30;
            Toaster.Current.AnimateFor = 500;
            Toaster.Current.HideToastIn = 5;
            Toaster.Current.ToastAction = async () =>
            {
                await Task.Delay(2000);
                Console.WriteLine("Hello, Jupiter!");
            };
        }
    }
}