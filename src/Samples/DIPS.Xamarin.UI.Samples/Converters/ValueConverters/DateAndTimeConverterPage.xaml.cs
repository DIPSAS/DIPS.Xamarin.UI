using System;
using System.ComponentModel;
using System.Windows.Input;
using DIPS.Xamarin.UI.Extensions;
using DIPS.Xamarin.UI.InternalShared;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Converters.ValueConverters
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DateAndTimeConverterPage : ContentPage
    {
        public DateAndTimeConverterPage()
        {
            InitializeComponent();
        }
    }

    public class DateAndTimeConverterPageViewModel : INotifyPropertyChanged
    {
        public DateAndTimeConverterPageViewModel()
        {
            OpenLocaleMobileSettingsCommand = new Command(() => MobileSettings.Instance.OpenLocale());
        }

        private DateTime m_date;

        public DateTime Date
        {
            get => m_date;
            set => PropertyChanged.RaiseAfter(ref m_date, value);
        }

        public ICommand OpenLocaleMobileSettingsCommand { get; }
        public string Locale => System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}