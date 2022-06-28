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
    public partial class BoolToDoubleConverterPage : ContentPage
    {
        public BoolToDoubleConverterPage()
        {
            InitializeComponent();
        }
    }
    
    public class BoolToDoubleConverterViewModel : INotifyPropertyChanged
    {
        private bool m_someLogicalProperty;
        public event PropertyChangedEventHandler PropertyChanged;

        public bool SomeLogicalProperty
        {
            get => m_someLogicalProperty;
            set => this.Set(ref m_someLogicalProperty, value, PropertyChanged);
        }
        
    }
}