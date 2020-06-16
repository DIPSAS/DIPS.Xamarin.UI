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
    public partial class TypeToObjectConverterPage : ContentPage
    {
        public TypeToObjectConverterPage()
        {
            InitializeComponent();
        }
    }

    public class TypeToObjectConverterPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;



        private bool m_isToggled;

        public bool IsToggled
        {
            get => m_isToggled;
            set
            {
                PropertyChanged.RaiseWhenSet(ref m_isToggled, value);
                if(m_isToggled)
                {
                    CurrentTypeClass = new OneTypeClass();
                }
                else
                {
                    CurrentTypeClass = new AnotherTypeClass();
                }
            }
        }

        private object m_currentTypeClass = new AnotherTypeClass();

        public object CurrentTypeClass
        {
            get => m_currentTypeClass;
            set => PropertyChanged.RaiseWhenSet(ref m_currentTypeClass, value);
        }

    }

    public class OneTypeClass
    {

    }

    public class AnotherTypeClass
    {

    }
}