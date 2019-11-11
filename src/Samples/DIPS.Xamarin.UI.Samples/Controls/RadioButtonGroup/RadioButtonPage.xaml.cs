using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Controls.RadioButtonGroup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RadioButtonGroupPage : ContentPage
    {
        public RadioButtonGroupPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is RadioButtonPageViewModel RadioButtonPageViewModel){
                await RadioButtonPageViewModel.Initialize();
            }
        }
    }

    public class RadioButtonPageViewModel : INotifyPropertyChanged
    {
        private string m_deSelectedColor = "#047F89";
        private string m_selectedColor = "#047F89";
        private OptionViewModel m_selectedOption;
        private ObservableCollection<OptionViewModel> m_options;

        public RadioButtonPageViewModel()
        {
            Options = new ObservableCollection<OptionViewModel>();
            AddNewCommand = new Command(() => Options.Add(new OptionViewModel("Forth option", ServerOptions.Forth)));
        }

        public string DeSelectedColor
        {
            get => m_deSelectedColor;
            set
            {
                try
                {
                    new ColorTypeConverter().ConvertFromInvariantString(value);
                    m_deSelectedColor = value;
                    this.OnPropertyChanged(PropertyChanged);
                }
                catch (Exception e)
                {
                    //Swallow it.
                }
            }
        }

        public string SelectedColor
        {
            get => m_selectedColor;
            set
            {
                try
                {
                    new ColorTypeConverter().ConvertFromInvariantString(value);
                    m_selectedColor = value;
                    this.OnPropertyChanged(PropertyChanged);
                }
                catch (Exception e)
                {
                    //Swallow it.
                }
            }
        }

        public ObservableCollection<OptionViewModel> Options
        {
            get => m_options;
            set => this.Set(ref m_options, value, PropertyChanged);
        }

        public OptionViewModel SelectedOption
        {
            get => m_selectedOption;
            set => this.Set(ref m_selectedOption, value, PropertyChanged);
        }

        public ICommand AddNewCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public Task Initialize()
        {
            var firstOption = new OptionViewModel("A very very very very looooooooooooooooooooooooooooooooooooooooooong option", ServerOptions.First);
            var secondOption = new OptionViewModel("Second option", ServerOptions.Second);
            var thirdOption = new OptionViewModel("Third option", ServerOptions.Third);

            Options.Add(firstOption);
            Options.Add(secondOption);
            Options.Add(thirdOption);
            SelectedOption = secondOption;

            return Task.CompletedTask;
        }

        public void OnDone()
        {
            if (SelectedOption.ServerOptions == ServerOptions.First)
            {

            }
        }
    }

    public class OptionViewModel
    {
        public string Name { get; }
        public ServerOptions ServerOptions { get; }

        public OptionViewModel(string name, ServerOptions serverOptions)
        {
            Name = name;
            ServerOptions = serverOptions;
        }
    }

    public enum ServerOptions
    {
        First,
        Second,
        Third,
        Forth
    }
}