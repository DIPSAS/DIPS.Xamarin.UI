using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DIPS.Xamarin.UI.Extensions;
using DIPS.Xamarin.UI.InternalShared;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Converters.ValueConverters
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DateConverterPage : ContentPage
    {
        public DateConverterPage()
        {
            InitializeComponent();
        }
    }

    public class DateConverterPageViewModel : INotifyPropertyChanged
    {
        public DateConverterPageViewModel()
        {

            OpenLocaleMobileSettingsCommand = new Command(() => MobileSettings.Instance.OpenLocale());
            Date = DateTime.Now;
        }

        private DateTime m_date;

        public event PropertyChangedEventHandler PropertyChanged;

        public DateTime Date
        {
            get => m_date;
            set
            {
                PropertyChanged.RaiseAfter(ref m_date, value);
                PropertyChanged.Raise(nameof(DateTime));
            }
        }

        public ICommand OpenLocaleMobileSettingsCommand { get; }

        public string Locale => CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
    }
}