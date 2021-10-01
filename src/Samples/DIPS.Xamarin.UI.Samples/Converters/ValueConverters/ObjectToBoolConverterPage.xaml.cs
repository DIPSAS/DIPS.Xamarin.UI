using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Converters.ValueConverters
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ObjectToBoolConverterPage : ContentPage
    {
        public ObjectToBoolConverterPage()
        {
            InitializeComponent();
        }
    }
    
    public class ObjectToBoolConverterViewModel : INotifyPropertyChanged
    {
        private string m_someObject;

        public ObjectToBoolConverterViewModel()
        {
            SomeObject = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string SomeObject
        {
            get => m_someObject;
            set => this.Set(ref m_someObject, value, PropertyChanged);
        }
    }
    
}