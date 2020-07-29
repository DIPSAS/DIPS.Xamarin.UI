using System.Threading.Tasks;
using System.Windows.Input;
using DIPS.Xamarin.UI.Controls.Toast;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Color = System.Drawing.Color;
using ToastControl = DIPS.Xamarin.UI.Controls.Toast.Toast;

namespace DIPS.Xamarin.UI.Samples.Controls.Toast
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToastPage : ContentPage
    {
        private readonly ToastLayout LayoutOneMoonFourMars = new ToastLayout
        {
            BackgroundColor = Color.DodgerBlue,
            CornerRadius = 8,
            FontSize = 11,
            LineBreakMode = LineBreakMode.TailTruncation,
            MaxLines = 1,
            TextColor = Color.White,
            Padding = new Thickness(20, 10)
        };

        private readonly ToastLayout LayoutThreePluto = new ToastLayout
        {
            BackgroundColor = Color.DodgerBlue,
            CornerRadius = 8,
            FontSize = 15,
            LineBreakMode = LineBreakMode.TailTruncation,
            MaxLines = 1,
            TextColor = Color.White,
            Padding = new Thickness(20, 10)
        };

        private ToastLayout LayoutTwoVenus = new ToastLayout
        {
            BackgroundColor = Color.MediumSeaGreen,
            CornerRadius = 12,
            FontSize = 13,
            LineBreakMode = LineBreakMode.TailTruncation,
            MaxLines = 2,
            TextColor = Color.White,
            Padding = new Thickness(20, 10)
        };

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
                ToastControl.Current.DisplayAnimation = toastView =>
                {
                    toastView.Opacity = 0;
                    return toastView.FadeTo(1, 500, Easing.Linear);
                };
                ToastControl.Current.CloseAnimation = toastView => toastView.FadeTo(0, 500, Easing.Linear);

                ToastControl.Current.ToastLayout = LayoutOneMoonFourMars;
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
                ToastControl.Current.DisplayAnimation = toastView =>
                {
                    toastView.TranslationY -= 50;
                    return toastView.TranslateTo(0, toastView.TranslationY + 50, 500, Easing.Linear);
                };
                ToastControl.Current.CloseAnimation = toastView =>
                    toastView.TranslateTo(0, -(toastView.TranslationY + 50), 500, Easing.Linear);

                ToastControl.Current.ToastLayout = LayoutTwoVenus;
                ToastControl.Current.HideToastIn = 0;
                ToastControl.Current.ToastAction = null;
                ToastControl.Current.Text = parameter.ToString();

                ToastControl.Current.DisplayToast();
            });

            ShowToastCommand3 = new Command(parameter =>
            {
                ToastControl.Current.Text = parameter.ToString();

                ToastControl.Current.PositionY = 1;
                ToastControl.Current.ToastAction = async () =>
                {
                    await ToastControl.Current.CloseToast();
                };
                ToastControl.Current.ToastLayout = LayoutThreePluto;
                ToastControl.Current.HideToastIn = 3000;

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
            ToastControl.Current.PositionY = 1;
            ToastControl.Current.HideToastIn = 3000;
        }
    }
}