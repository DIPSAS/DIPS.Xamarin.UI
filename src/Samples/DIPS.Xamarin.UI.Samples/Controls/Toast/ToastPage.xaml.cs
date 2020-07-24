using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Color = System.Drawing.Color;
using ToastControl = DIPS.Xamarin.UI.Controls.Toast.Toast;

namespace DIPS.Xamarin.UI.Samples.Controls.Toast
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToastPage : ContentPage
    {
        private string m_pageTitle;
        private ICommand m_showToastCommand;
        private ICommand m_showToastCommand2;
        private ICommand m_showToastCommand3;

        public ToastPage()
        {
            InitializeComponent();

            ConfigureToast();

            PageTitle = "Hello, World!";

            ShowToastCommand = new Command(parameter =>
            {
                ToastControl.Current.HideToastIn = 5;
                ToastControl.Current.ToastAction = async () =>
                {
                    PageTitle = "Hello, Mercury!";
                    await Task.Delay(1500);
                    PageTitle = "Hello, World!";
                };
                ToastControl.Current.Text = parameter.ToString();

                ToastControl.Current.DisplayToast();
            });

            ShowToastCommand2 = new Command(parameter =>
            {
                ToastControl.Current.HideToastIn = 0;
                ToastControl.Current.ToastAction = null;
                ToastControl.Current.Text = parameter.ToString();

                ToastControl.Current.DisplayToast();
            });

            ShowToastCommand3 = new Command(parameter =>
            {
                ToastControl.Current.HideToastIn = 5;
                ToastControl.Current.ToastAction = async () =>
                {
                    await ToastControl.Current.CancelToast();
                };
                ToastControl.Current.Text = parameter.ToString();

                ToastControl.Current.DisplayToast();
            });
        }

        public string PageTitle
        {
            get => m_pageTitle;
            set
            {
                m_pageTitle = value;
                OnPropertyChanged(nameof(PageTitle));
            }
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

        public ICommand ShowToastCommand2
        {
            get => m_showToastCommand2;
            set
            {
                m_showToastCommand2 = value;
                OnPropertyChanged(nameof(ShowToastCommand2));
            }
        }

        public ICommand ShowToastCommand3
        {
            get => m_showToastCommand3;
            set
            {
                m_showToastCommand3 = value;
                OnPropertyChanged(nameof(ShowToastCommand3));
            }
        }

        private static void ConfigureToast()
        {
            ToastControl.Current.FontSize = 13;
            ToastControl.Current.TextColor = Color.White;
            ToastControl.Current.BackgroundColor = Color.DodgerBlue;
            ToastControl.Current.CornerRadius = 8;
            ToastControl.Current.Padding = new Thickness(20, 10);
            ToastControl.Current.PositionY = 30;
            ToastControl.Current.AnimateFor = 500;
            ToastControl.Current.HideToastIn = 5;
            ToastControl.Current.LineBreakMode = LineBreakMode.TailTruncation;
            ToastControl.Current.MaxLines = 1;
        }
    }
}