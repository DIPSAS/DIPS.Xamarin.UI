using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.Scheduler
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Scheduler : ContentView
    {
        public static readonly BindableProperty AppointmentsProperty = BindableProperty.Create(nameof(Appointments), typeof(IEnumerable<object>), typeof(Scheduler), propertyChanged: OnAppointmentsPropertyChanged);

        public static readonly BindableProperty ScheduleDateProperty = BindableProperty.Create(nameof(ScheduleDate), typeof(DateTime), typeof(Scheduler), defaultValue:DateTime.Now);

        public Scheduler()
        {
            InitializeComponent();
        }

        public string AppointmentFromDate { get; set; }

        public IEnumerable<object> Appointments
        {
            get => (IEnumerable<object>)GetValue(AppointmentsProperty);
            set => SetValue(AppointmentsProperty, value);
        }

        public string AppointmentToDate { get; set; }

        public DateTime ScheduleDate
        {
            get => (DateTime)GetValue(ScheduleDateProperty);
            set => SetValue(ScheduleDateProperty, value);
        }
        private static void OnAppointmentsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is Scheduler scheduler))
            {
                return;
            }

            scheduler.Initialize();
        }

        private void Initialize()
        {
        }
    }
}