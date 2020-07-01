using System;
using System.ComponentModel;
using System.Windows.Input;
using DIPS.Xamarin.UI.Controls.Slidable;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.Forms.IssuesRepro.Github179
{
    public partial class Github179 : ContentPage
    {
        public Github179()
        {
            InitializeComponent();
            BindingContext = new IssueViewModel();
        }
    }

    public class IssueViewModel : INotifyPropertyChanged
    {
        private SlidableProperties slidableProperties;

        public SlidableProperties SlidableProperties { get => slidableProperties; set => PropertyChanged?.RaiseWhenSet(ref slidableProperties, value); }

        public Func<int, object> CreateCalendar => i => new CalendarViewModel(DateTime.Now.AddDays(i).ToString("dd.MM"), () => SlidableProperties.ScrollTo(s => SlidableProperties = s, () => SlidableProperties, i));

        private DateTime m_date = DateTime.Now;

        public DateTime Date
        {
            get => m_date;
            set
            {
                var dateDiff = (int)Math.Round(((value.Date - m_date.Date).TotalDays));
                SlidableProperties.ScrollTo(s => SlidableProperties = s, () => SlidableProperties, dateDiff, 5000);
                PropertyChanged.RaiseWhenSet(ref m_date, value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class CalendarViewModel : ISliderSelectable, INotifyPropertyChanged
    {
        private bool m_selected;
        public CalendarViewModel(string value, Action onSelectedCommand)
        {
            Value = value;
            SelectCommand = new Command(onSelectedCommand);
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
