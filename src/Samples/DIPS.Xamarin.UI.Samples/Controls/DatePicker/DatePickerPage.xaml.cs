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
    }

    public class DatePickerPageViewModel : INotifyPropertyChanged
    {
        public DatePickerPageViewModel()
        {
            ExtraButtonCommand = new Command<string>(s =>
            {
                //Do something 
            });
        }

        private DateTime m_date;

        public DateTime Date
        {
            get => m_date;
            set => PropertyChanged.RaiseWhenSet(ref m_date, value);
        }

        public DateTime MaximumDate => DateTime.Now.AddDays(5);

        public DateTime MinimumDate => DateTime.Now.AddDays(-5);

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ExtraButtonCommand { get; }
    }
}