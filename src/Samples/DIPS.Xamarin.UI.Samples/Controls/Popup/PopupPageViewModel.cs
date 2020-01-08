using System;
using System.ComponentModel;
using System.Windows.Input;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Controls.Popup
{
    public class PopupPageViewModel : INotifyPropertyChanged
    {
        private bool m_isOpen;

        public PopupPageViewModel()
        {
            Ascending = true;
            SaveCommand = new Command(o => Update((PopupFilterViewModel)o));
        }

        private void Update(PopupFilterViewModel popupFilterViewModel)
        {
            Ascending = popupFilterViewModel.Ascending;
            PropertyChanged?.Raise(nameof(Ascending));
        }

        public bool IsOpen { get => m_isOpen; set => PropertyChanged?.RaiseWhenSet(ref m_isOpen, value); }

        public string MyString { get; } = "Hello popupContent";

        public ICommand SaveCommand { get; }

        public bool Ascending { get; set; }

        public Func<PopupPageViewModel> GetViewModel => () => this;

        public Func<PopupFilterViewModel> FilterViewModelFactory => new Func<PopupFilterViewModel>(() => new PopupFilterViewModel(SaveCommand) { Ascending = Ascending });
        public event PropertyChangedEventHandler? PropertyChanged;
    }

    public class PopupFilterViewModel : INotifyPropertyChanged
    {
        public PopupFilterViewModel(ICommand saveCommand)
        {
            SaveCommand = saveCommand;
        }

        public bool Ascending { get; set; }

        public ICommand SaveCommand { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}

