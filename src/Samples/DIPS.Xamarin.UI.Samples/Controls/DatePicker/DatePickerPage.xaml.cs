using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Controls.DatePicker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatePickerPage : ContentPage
    {
        public DatePickerPage()
        {
            InitializeComponent();
        }

        private void ExtraButtonClicked(object sender, EventArgs e)
        {

        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {

        }
    }

    public class DatePickerPageViewModel : INotifyPropertyChanged
    {
        public DatePickerPageViewModel()
        {
            SetTodayDateCommand = new Command<string>(s =>
            {
                Date = DateTime.Now;
            });
        }

        private DateTime m_date;

        public DateTime Date
        {
            get => m_date;
            set => PropertyChanged.RaiseWhenSet(ref m_date, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SetTodayDateCommand { get; }
    }
}