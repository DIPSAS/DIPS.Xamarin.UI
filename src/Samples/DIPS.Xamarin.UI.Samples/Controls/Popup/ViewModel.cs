using System;
using System.ComponentModel;
using System.Windows.Input;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Controls.Popup
{
    public class ViewModel : INotifyPropertyChanged
    {
        private bool isOpen;

        public ViewModel()
        {
            Ascending = true;
            SaveCommand = new Command(o => Update((PopupFilterViewModel)o));
        }
        private void Update(PopupFilterViewModel popupFilterViewModel)
        {
            Ascending = popupFilterViewModel.Ascending;
            PropertyChanged?.Raise(nameof(Ascending));
        }

        public bool IsOpen { get => isOpen; set => this.Set(ref isOpen, value, PropertyChanged); }

        public string MyString { get; } = "Hello popupContent";

        public ICommand SaveCommand { get; }

        public bool Ascending { get; set; }

        public Func<ViewModel> GetViewModel => () => this;

        public Func<PopupFilterViewModel> FilterViewModelFactory => new Func<PopupFilterViewModel>(() => new PopupFilterViewModel { Ascending = Ascending, SaveCommand = SaveCommand });
        public event PropertyChangedEventHandler? PropertyChanged;
    }

    public class PopupFilterViewModel : INotifyPropertyChanged
    {
        public bool Ascending { get; set; }

        public ICommand? SaveCommand { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}

