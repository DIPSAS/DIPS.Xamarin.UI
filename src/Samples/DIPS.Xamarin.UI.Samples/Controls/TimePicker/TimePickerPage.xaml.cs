using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Controls.TimePicker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimePickerPage : ContentPage
    {
        public TimePickerPage()
        {
            InitializeComponent();
        }
    }

    public class TimePickerPageViewModel : INotifyPropertyChanged
    {
        public TimePickerPageViewModel()
        {
        }
        private TimeSpan m_time;
        public event PropertyChangedEventHandler PropertyChanged;

        public TimeSpan Time
        {
            get => m_time;
            set => PropertyChanged.RaiseWhenSet(ref m_time, value);
        }
    }
}