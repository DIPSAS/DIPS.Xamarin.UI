using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IEnumerable MyLocalList { get; set; } = new[] { "A", "B", "C", "D" };
    }
}