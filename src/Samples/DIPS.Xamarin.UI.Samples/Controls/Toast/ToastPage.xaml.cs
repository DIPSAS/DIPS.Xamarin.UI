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
        private readonly ToastLayout LayoutOneMoon = new ToastLayout
        {
            BackgroundColor = Color.DodgerBlue,
            CornerRadius = 8,
            FontSize = 11,
            HasShadow = true,
            LineBreakMode = LineBreakMode.TailTruncation,
            MaxLines = 1,
            Padding = new Thickness(20, 10),
            TextColor = Color.White
        };

        private readonly ToastLayout LayoutThreePluto = new ToastLayout
        {
            BackgroundColor = Color.DodgerBlue,
            CornerRadius = 8,
            FontSize = 15,
            LineBreakMode = LineBreakMode.TailTruncation,
            MaxLines = 1,
            Padding = new Thickness(20, 10),
            PositionY = 5,
            TextColor = Color.White
        };

        private ToastLayout LayoutTwoVenus = new ToastLayout
        {
            BackgroundColor = Color.MediumSeaGreen,
            CornerRadius = 12,
            FontSize = 13,
            LineBreakMode = LineBreakMode.TailTruncation,
            MaxLines = 2,
            Padding = new Thickness(20, 10),
            PositionY = 1,
            TextColor = Color.White
        };

        private string m_pageTitle;
        private ICommand m_showToastCommand;
        private ICommand m_showToastCommand2;
        private ICommand m_showToastCommand3;
        private ICommand m_showToastCommand4;

        public ToastPage()
        {
            InitializeComponent();

            PageTitle = "Hello, World!";

            ShowToastCommand = new Command(parameter =>
            {
                var options = new ToastOptions
                {
                    ToastAction = async () =>
                    {
                        PageTitle = "Hello, Mercury!";
                        await Task.Delay(1500);
                        PageTitle = "Hello, World!";
                    },
                    DisplayAnimation = toastView =>
                    {
                        toastView.Opacity = 0;
                        return toastView.FadeTo(1, 500, Easing.Linear);
                    },
                    CloseAnimation = toastView => toastView.FadeTo(0, 500, Easing.Linear),
                    HideToastIn = 3000
                };
                ToastControl.Current.DisplayToast(parameter.ToString(), options, LayoutOneMoon);
            });

            ShowToastCommand2 = new Command(parameter =>
            {
                var options = new ToastOptions
                {
                    ToastAction = null,
                    DisplayAnimation = toastView =>
                    {
                        toastView.TranslationY -= 50;
                        return toastView.TranslateTo(0, toastView.TranslationY + 50, 500, Easing.Linear);
                    },
                    CloseAnimation = toastView => toastView.TranslateTo(0, -(toastView.TranslationY + 50), 500, Easing.Linear),
                    HideToastIn = 0
                };
                ToastControl.Current.DisplayToast(parameter.ToString(), options, LayoutTwoVenus);
            });

            ShowToastCommand3 = new Command(parameter =>
            {
                var options = new ToastOptions
                {
                    ToastAction = async () =>
                    {
                        await ToastControl.Current.CloseToast();
                    },
                    HideToastIn = 3000
                };
                ToastControl.Current.DisplayToast(parameter.ToString(), options, LayoutThreePluto);
            });

            ShowToastCommand4 = new Command(parameter =>
            {
                ToastControl.Current.DisplayToast(parameter.ToString());
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

        public ICommand ShowToastCommand4
        {
            get => m_showToastCommand4;
            set
            {
                m_showToastCommand4 = value;
                OnPropertyChanged(nameof(ShowToastCommand4));
            }
        }
    }
}