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
    public partial class IsEmptyConverterPage : ContentPage
    {
        public IsEmptyConverterPage()
        {
            InitializeComponent();
        }
    }

    public class IsEmptyConverterViewModel
    {

        public List<string> EmptyListOfStrings { get; set; } = new List<string>();
        public List<string> NonEmptyListOfStrings { get; set; } = new List<string>(){"Non empty string"};
        public string EmptyString { get; set; } = "";
        public string NonEmptyString { get; set; } = "Non empty string";

    }
}