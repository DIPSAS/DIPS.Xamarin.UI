using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Converters.ValueConverters
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IsEmptyToObjectConverterPage : ContentPage
    {
        public IsEmptyToObjectConverterPage()
        {
            InitializeComponent();
        }
    }

    public class IsEmptyToObjectConverterPageViewModel : INotifyPropertyChanged
    {

        private string m_myText;
        public event PropertyChangedEventHandler PropertyChanged;

        public string MyText
        {
            get => m_myText;
            set => PropertyChanged.RaiseWhenSet(ref m_myText, value);
        }
    }
}