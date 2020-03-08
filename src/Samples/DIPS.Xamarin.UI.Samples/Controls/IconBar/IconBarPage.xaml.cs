using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Controls.IconBar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IconBarPage : ContentPage
    {
        public IconBarPage()
        {
            InitializeComponent();
        }

        public List<IconBarSampleModel> SampleData { get; set; } = new List<IconBarSampleModel>
        {
            new IconBarSampleModel {Name = "߷", Type = "Urgent"},
            new IconBarSampleModel {Name = "♔", Type = "Normal"},
            new IconBarSampleModel {Name = "♺", Type = "Optional"},
            new IconBarSampleModel {Name = "♖", Type = "Normal"},
            new IconBarSampleModel {Name = "♞", Type = "Normal"},
            new IconBarSampleModel {Name = "❀", Type = "Normal"},
            new IconBarSampleModel {Name = "☭", Type = "Urgent"}
        };
    }
}