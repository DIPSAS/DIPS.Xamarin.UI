using System.Collections.Generic;

using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Converters.MultiValueConverters
{
    public partial class ItemIsLastConverterPage : ContentPage
    {
        public ItemIsLastConverterPage()
        {
            BindingContext = this;
            InitializeComponent();
        }

        public List<string> Items { get; } = new List<string>
        {
            "abc",
            "dce",
            "ok",
            "test"
        };
    }
}
