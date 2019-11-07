using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class RadioButtonPageViewModel {

        private Options m_selectedOption;

        public RadioButtonPageViewModel()
        {
            OptionsCommand = new Command<Options>(Execute);    
        }

        private void Execute(Options obj)
        {
            m_selectedOption = obj;
        }

        public string SelectedOption { get; set; }

        public Command<Options> OptionsCommand { get; }
    }

    public enum Options
    {
        First,
        Second,
        Third
    }
}