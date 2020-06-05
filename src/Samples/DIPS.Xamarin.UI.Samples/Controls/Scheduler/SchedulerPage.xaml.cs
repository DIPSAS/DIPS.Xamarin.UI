using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Controls.Scheduler
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SchedulerPage : ContentPage
    {
        public SchedulerPage()
        {
            InitializeComponent();
        }
    }

    public class SchedulerPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public SchedulerPageViewModel()
        {
            Appointments = new List<AppointmentViewModel>()
            {
                new AppointmentViewModel(DateTime.Now.AddHours(-1), DateTime.Now),
                new AppointmentViewModel(DateTime.Now.AddHours(-2), DateTime.Now.AddHours(1)),
                new AppointmentViewModel(DateTime.Now.AddHours(1).AddMinutes(15), DateTime.Now.AddHours(1).AddMinutes(30)),
            };     
        }

        public List<AppointmentViewModel> Appointments { get; }
    }

    public class AppointmentViewModel
    {
        public AppointmentViewModel(DateTime fromDate, DateTime toDate)
        {
            FromDate = fromDate;
            ToDate = toDate;
        }

        public DateTime FromDate { get; }
        public DateTime ToDate { get; }
    }
}