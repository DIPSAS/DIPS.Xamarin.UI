using System;
using System.ComponentModel;
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
    }

    public class RadioButtonPageViewModel : INotifyPropertyChanged
    {
        private string m_deSelectedColor = "#047F89";
        private string m_selectedColor = "#047F89";

        private Options m_selectedOption;

        public RadioButtonPageViewModel()
        {
            OptionsCommand = new Command<Options>(Execute);
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

        public Command<Options> OptionsCommand { get; }

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

        public string SelectedOption { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void Execute(Options obj)
        {
            m_selectedOption = obj;
        }
    }

    public enum Options
    {
        First,
        Second,
        Third
    }
}