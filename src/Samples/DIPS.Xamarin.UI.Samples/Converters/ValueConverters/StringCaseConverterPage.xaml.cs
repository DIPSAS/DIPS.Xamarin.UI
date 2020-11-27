using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Converters.ValueConverters
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StringCaseConverterPage : ContentPage
    {
        public StringCaseConverterPage()
        {
            InitializeComponent();
        }
    }

    public class StringCaseConverterViewModel
    {
        public string MyString => "en test string";
    }
}