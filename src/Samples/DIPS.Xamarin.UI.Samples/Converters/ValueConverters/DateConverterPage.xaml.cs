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
    }
}