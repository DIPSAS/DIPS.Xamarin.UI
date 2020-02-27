using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using DIPS.Xamarin.UI.Controls.Slidable;
using DIPS.Xamarin.UI.Converters.ValueConverters;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Controls.SlideLayout
{
    public class SlideLayoutViewModel : INotifyPropertyChanged
    {
        private SlidableProperties slidableProperties;
        private string selected;

        public SlideLayoutViewModel()
        {
            OnSelectedIndexChangedCommand = new Command(o => Selected = o.ToString());
        }

        public void Initialize()
        {
            SlidableProperties = new SlidableProperties(4);
        }

        public SliderConfig Config => new SliderConfig(-10, 0);

        public ICommand OnSelectedIndexChangedCommand { get; }
        public string Selected { get => selected; set => PropertyChanged?.RaiseWhenSet(ref selected, value); }
        public Func<int, object> CreateCalendar => i => new CalendarViewModel(DateTime.Now.AddDays(i));

        public SlidableProperties SlidableProperties { get => slidableProperties; set => PropertyChanged?.RaiseWhenSet(ref slidableProperties, value); }

        public event PropertyChangedEventHandler? PropertyChanged;
    }

    public class CalendarViewModel : INotifyPropertyChanged, ISliderSelectable
    {
        private bool selected;

        public CalendarViewModel(DateTime time)
        {
            Value = time.Day + "." + time.Month;
        }

        public string Value { get; }
        public bool Selected { get => selected; set => PropertyChanged.RaiseWhenSet(ref selected, value); }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnSelectionChanged(bool selected)
        {
            Selected = selected;
        }
    }
}
