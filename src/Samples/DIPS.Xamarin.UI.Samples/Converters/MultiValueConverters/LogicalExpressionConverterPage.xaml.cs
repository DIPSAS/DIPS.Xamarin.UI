using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Converters.MultiValueConverters
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogicalExpressionConverterPage : ContentPage
    {
        public LogicalExpressionConverterPage()
        {
            InitializeComponent();
        }
    }

    public class LogicalExpressionConverterPageViewModel : INotifyPropertyChanged
    {
        private bool m_logicalProperty;
        private bool m_someOtherLogicalProperty;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool LogicalProperty 
        { 
            get => m_logicalProperty; 
            set => PropertyChanged.RaiseWhenSet(ref m_logicalProperty, value);
        }

        public bool SomeOtherLogicalProperty 
        { 
            get => m_someOtherLogicalProperty; 
            set => PropertyChanged.RaiseWhenSet(ref m_someOtherLogicalProperty, value); 
        }
    }
}