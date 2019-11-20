using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Controls.Popup
{
    public partial class PopupPage : ContentPage
    {
        public PopupPage()
        {
            InitializeComponent();
            BindingContext = this;
            SaveCommand1 = new Command(o => Update1(o as PopupFilterViewModel));
            SaveCommand2 = new Command(o => Update2(o as PopupFilterViewModel));
            Ascending2 = true;
        }

        private void Update1(PopupFilterViewModel popupFilterViewModel)
        {
            Ascending1 = popupFilterViewModel.Ascending;
            OnPropertyChanged(nameof(Ascending1));
        }

        private void Update2(PopupFilterViewModel popupFilterViewModel)
        {
            Ascending2 = popupFilterViewModel.Ascending;
            OnPropertyChanged(nameof(Ascending2));
        }

        public ICommand SaveCommand1 { get; }
        public ICommand SaveCommand2 { get; }

        public bool Ascending1 { get; set; }
        public bool Ascending2 { get; set; }

        public Func<PopupFilterViewModel> FilterViewModelFactory1 => new Func<PopupFilterViewModel>(() => new PopupFilterViewModel { Ascending = Ascending1, SaveCommand = SaveCommand1 });
        public Func<PopupFilterViewModel> FilterViewModelFactory2 => new Func<PopupFilterViewModel>(() => new PopupFilterViewModel { Ascending = Ascending2, SaveCommand = SaveCommand2 });
    }

    public class PopupFilterViewModel : INotifyPropertyChanged
    {
        public bool Ascending { get; set; }

        public ICommand SaveCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
