using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class TimeConverterPage : ContentPage
    {
        public TimeConverterPage()
        {
            InitializeComponent();
        }
    }

    public class TimeConverterPageViewModel : INotifyPropertyChanged
    {
        public TimeConverterPageViewModel()
        {
            OpenLocaleMobileSettingsCommand = new Command(() => MobileSettings.Instance.OpenLocale());
        }

        private TimeSpan m_time;

        public TimeSpan Time
        {
            get => m_time;
            set => PropertyChanged.RaiseWhenSet(ref m_time, value);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand OpenLocaleMobileSettingsCommand { get; }
        public string Locale => System.Threading.Thread.CurrentThread.CurrentCulture.ThreeLetterWindowsLanguageName;
    }
}