using System;
using System.ComponentModel;
using System.Windows.Input;
using DIPS.Xamarin.UI.Controls.Slidable;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.Forms.IssuesRepro.Github179
{
    [Issue(179)]
    public partial class Github179 : ContentPage
    {
        private DateTime m_date = DateTime.Now;

        public Github179()
        {
            InitializeComponent();
            BindingContext = new Issue179ViewModel();
        }

        public void DateSelected(object sender, DateChangedEventArgs eventArgs)
        {
            var dateDiff = (int)Math.Round(((eventArgs.NewDate.Date - m_date.Date).TotalDays));
            if (Math.Abs(slidablecontent.SlideProperties.Position - dateDiff) < 1)
            {
                return;
            }
            var time = Math.Min(1000, Math.Abs(dateDiff * 50));
            slidablecontent.ScrollTo(dateDiff, time);
        }
    }

    public class Issue179ViewModel : INotifyPropertyChanged
    {
        private SlidableProperties m_slidableProperties;

        public SlidableProperties SlidableProperties
        {
            get => m_slidableProperties;
            set
            {
                PropertyChanged?.RaiseWhenSet(ref m_slidableProperties, value);
                PropertyChanged.Raise(nameof(Date));
            }
        }

        public Func<int, object> CreateCalendar => i => new CalendarViewModel(DateTime.Now.AddDays(i).ToString("dd.MM"), () => SlidableProperties.ScrollTo(s => SlidableProperties = s, () => SlidableProperties, i));

        public DateTime Date => DateTime.Now.Date.AddDays(SlidableProperties.Position);

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class CalendarViewModel : ISliderSelectable, INotifyPropertyChanged
    {
        private bool m_selected;
        public CalendarViewModel(string value, Action onSelectedAction)
        {
            Value = value;
            SelectCommand = new Command(onSelectedAction);
        }

        public ICommand SelectCommand { get; }
        public string Value { get; }

        public void OnSelectionChanged(bool selected)
        {
            Selected = selected;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public bool Selected { get => m_selected; set => PropertyChanged.RaiseWhenSet(ref m_selected, value); }
    }
}
