using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Controls.RadioButtonGroup;
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
        private Option m_selectedOption;
        private ObservableCollection<Option> m_options;

        public RadioButtonPageViewModel()
        {
            Options = new ObservableCollection<Option>();
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

        public ObservableCollection<Option> Options
        {
            get => m_options;
            set => this.Set(ref m_options, value, PropertyChanged);
        }

        public Option SelectedOption
        {
            get => m_selectedOption;
            set => this.Set(ref m_selectedOption, value, PropertyChanged);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public Task Initialize()
        {
            var firstOption = new Option(ServerOptions.First, "First option");
            var secondOption = new Option(ServerOptions.Second, "Second option");
            var thirdOption = new Option(ServerOptions.Third, "Third option");
            var listOfOptions = new List<Option>() { firstOption, secondOption, thirdOption };

            Options = new ObservableCollection<Option>(listOfOptions);
            SelectedOption = firstOption;

            return Task.CompletedTask;
        }

        public void OnDone()
        {
            if ((ServerOptions)SelectedOption.Identifier == ServerOptions.First)
            {

            }
        }
    }

    public enum ServerOptions
    {
        First,
        Second,
        Third
    }
}