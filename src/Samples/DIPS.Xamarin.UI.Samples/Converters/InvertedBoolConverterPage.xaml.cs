using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Converters
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InvertedBoolConverterPage : ContentPage
    {
        public InvertedBoolConverterPage()
        {
            InitializeComponent();
        }
    }

    public class InvertedBoolConverterViewModel
    {
        public bool SomeLogicalProperty => false;
    }
}