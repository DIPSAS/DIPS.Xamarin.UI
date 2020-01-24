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
        private TimeSpan m_time;

        public DateTime Date
        {
            get => m_date;
            set
            {
                PropertyChanged.RaiseWhenSet(ref m_date, value); 
                PropertyChanged.Raise(nameof(DateTime));
            }
        }

        public TimeSpan Time
        {
            get => m_time;
            set
            {
                PropertyChanged.RaiseWhenSet(ref m_time, value);
                PropertyChanged.Raise(nameof(DateTime));
            }
        }

        public DateTime DateTime => Date + Time;

        public ICommand OpenLocaleMobileSettingsCommand { get; }
        public string Locale => System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}