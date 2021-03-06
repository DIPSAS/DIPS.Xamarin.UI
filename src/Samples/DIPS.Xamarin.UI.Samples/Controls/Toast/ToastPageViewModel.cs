using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using DIPS.Xamarin.UI.Commands;
using DIPS.Xamarin.UI.Controls.Toast;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;
using ToastControl = DIPS.Xamarin.UI.Controls.Toast.Toast;

namespace DIPS.Xamarin.UI.Samples.Controls.Toast
{
    public class ToastPageViewModel : INotifyPropertyChanged
    {
        private readonly ToastLayout m_moonLayout = new ToastLayout
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

        private readonly ToastLayout m_plutoLayout = new ToastLayout
        {
            BackgroundColor = Color.DodgerBlue,
            CornerRadius = 8,
            FontSize = 15,
            LineBreakMode = LineBreakMode.TailTruncation,
            MaxLines = 1,
            Padding = new Thickness(20, 10),
            TextColor = Color.White,
            PositionY = 50
        };

        private readonly ToastOptions m_plutoOptions = new ToastOptions
        {
            ToastAction = async () =>
            {
                await ToastControl.HideToast();
            },
            Duration = 0
        };

        private ICommand m_marsCommand;
        private ICommand m_moonCommand;
        private string m_pageTitle;
        private ICommand m_plutoCommand;
        private ICommand m_venusCommand;

        public ToastPageViewModel()
        {
            PageTitle = "Hello, World!";

            var moonOptions = new ToastOptions
            {
                ToastAction = async () =>
                {
                    PageTitle = "Hello, Mercury!";
                    await Task.Delay(1500);
                    PageTitle = "Hello, World!";
                },
                OnBeforeDisplayingToast = toastView =>
                {
                    toastView.Opacity = 0;
                    return toastView.FadeTo(1, 500, Easing.Linear);
                },
                OnBeforeHidingToast = toastView => toastView.FadeTo(0, 500, Easing.Linear),
                Duration = 10000
            };

            MoonCommand =
                new AsyncCommand<string>(title => ToastControl.DisplayToast(title, moonOptions, m_moonLayout));
            VenusCommand =
                new AsyncCommand<string>(title => ToastControl.DisplayToast(title, VenusOptions(), VenusLayout()));
            MarsCommand = new AsyncCommand<string>(title => ToastControl.DisplayToast(title));
            PlutoCommand =
                new AsyncCommand<string>(title => ToastControl.DisplayToast(title, m_plutoOptions, m_plutoLayout));
        }

        public string PageTitle
        {
            get => m_pageTitle;
            set => PropertyChanged.RaiseWhenSet(ref m_pageTitle, value, this);
        }

        public ICommand MoonCommand
        {
            get => m_moonCommand;
            set => PropertyChanged.RaiseWhenSet(ref m_moonCommand, value, this);
        }

        public ICommand VenusCommand
        {
            get => m_venusCommand;
            set => PropertyChanged.RaiseWhenSet(ref m_venusCommand, value, this);
        }

        public ICommand MarsCommand
        {
            get => m_marsCommand;
            set => PropertyChanged.RaiseWhenSet(ref m_marsCommand, value, this);
        }

        public ICommand PlutoCommand
        {
            get => m_plutoCommand;
            set => PropertyChanged.RaiseWhenSet(ref m_plutoCommand, value, this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private static Action<ToastLayout> VenusLayout()
        {
            return layout =>
            {
                layout.BackgroundColor = Color.MediumSeaGreen;
                layout.CornerRadius = 12;
                layout.FontSize = 13;
                layout.HorizontalMargin = 25;
                layout.LineBreakMode = LineBreakMode.TailTruncation;
                layout.MaxLines = 2;
                layout.Padding = new Thickness(20, 10);
                layout.TextColor = Color.White;
                layout.PositionY = 30;
            };
        }

        private static Action<ToastOptions> VenusOptions()
        {
            return options =>
            {
                options.ToastAction = null;
                options.OnBeforeDisplayingToast = toastView =>
                {
                    toastView.TranslationY -= 50;
                    return toastView.TranslateTo(0, toastView.TranslationY + 50, 500, Easing.Linear);
                };
                options.OnBeforeHidingToast = toastView =>
                    toastView.TranslateTo(0, -(toastView.TranslationY + 50), 500, Easing.Linear);
                options.Duration = 5000;
            };
        }
    }
}