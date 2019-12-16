using System.ComponentModel;
using System.Windows.Input;
using DIPS.Xamarin.UI.Controls.Slidable;
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
            SlidableProperties = new SlidableProperties(0);
            OnSelectedIndexChangedCommand = new Command(o => Selected = o.ToString());
        }

        public SliderConfig Config => new SliderConfig(-10, 0);

        public ICommand OnSelectedIndexChangedCommand { get; }
        public string Selected { get => selected; set => PropertyChanged?.RaiseWhenSet(ref selected, value); }

        public SlidableProperties SlidableProperties { get => slidableProperties; set => PropertyChanged?.RaiseWhenSet(ref slidableProperties, value); }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
