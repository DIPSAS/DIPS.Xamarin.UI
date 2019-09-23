using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Converters.ValueConverters
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