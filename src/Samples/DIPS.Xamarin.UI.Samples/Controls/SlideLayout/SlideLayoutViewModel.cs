using System;
using System.ComponentModel;
using DIPS.Xamarin.UI.Controls.Slidable;
using DIPS.Xamarin.UI.Extensions;

namespace DIPS.Xamarin.UI.Samples.Controls.SlideLayout
{
    public class SlideLayoutViewModel : INotifyPropertyChanged
    {
        private SlidableProperties slidableProperties;

        public SlideLayoutViewModel()
        {
            SlidableProperties = new SlidableProperties(0);
        }

        public SlidableProperties SlidableProperties { get => slidableProperties; set => PropertyChanged.RaiseWhenSet(ref slidableProperties, value); }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
