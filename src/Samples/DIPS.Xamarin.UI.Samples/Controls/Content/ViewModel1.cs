using System.ComponentModel;
using DIPS.Xamarin.UI.Extensions;

namespace DIPS.Xamarin.UI.Samples.Controls.Content
{
    public class ViewModel1 : INotifyPropertyChanged
    {
        private bool swapTemplate = true;

        public int LuckyNumber => 42;
        public bool SwapTemplate { get => swapTemplate; set => PropertyChanged?.RaiseWhenSet(ref swapTemplate, value); }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
