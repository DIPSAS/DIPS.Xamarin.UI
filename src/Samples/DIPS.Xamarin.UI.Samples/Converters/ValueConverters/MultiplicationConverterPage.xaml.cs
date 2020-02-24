using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Converters.ValueConverters
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MultiplicationConverterPage : ContentPage
    {
        public MultiplicationConverterPage()
        {
            InitializeComponent();
        }
    }

    public class MultiplicationConverterPageViewModel : INotifyPropertyChanged
    {
        private double m_value;
        public event PropertyChangedEventHandler? PropertyChanged;

        public double Value
        {
            get => m_value;
            set => PropertyChanged?.RaiseWhenSet(ref m_value, value);
        }
    }
}