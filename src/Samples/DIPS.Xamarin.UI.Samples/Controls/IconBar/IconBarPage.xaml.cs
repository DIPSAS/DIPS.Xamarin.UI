using System.Collections;
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

        public List<MyClass> MyLocalList { get; set; } = new List<MyClass>
        {
            new MyClass { Name = "A" },
            new MyClass { Name = "B" },
            new MyClass { Name = "C" },
            new MyClass { Name = "D" }
        };
    }

    public class MyClass
    {
        public string Name { get; set; }
    }
}