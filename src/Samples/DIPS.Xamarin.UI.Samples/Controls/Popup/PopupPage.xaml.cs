using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Controls.Popup
{
    public partial class PopupPage : ContentPage
    {
        public PopupPage()
        {
            Ascending = true;
            InitializeComponent();
            BindingContext = this;
            SaveCommand = new Command(o => Update((PopupFilterViewModel)o));
        }

        private void Update(PopupFilterViewModel popupFilterViewModel)
        {
            Ascending = popupFilterViewModel.Ascending;
            OnPropertyChanged(nameof(Ascending));
        }

        public string MyString { get; } = "Hello popupContent";

        public ICommand SaveCommand { get; }

        public bool Ascending { get; set; }

        public Func<PopupPage> GetViewModel => () => this;

        public Func<PopupFilterViewModel> FilterViewModelFactory => new Func<PopupFilterViewModel>(() => new PopupFilterViewModel { Ascending = Ascending, SaveCommand = SaveCommand });
    }

    public class PopupFilterViewModel : INotifyPropertyChanged
    {
        public bool Ascending { get; set; }

        public ICommand? SaveCommand { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
