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
            new MyClass { Name = "߷", Color = "Red" },
            new MyClass { Name = "☪", Color = "Green" },
            new MyClass { Name = "♺", Color = "Blue" },
            new MyClass { Name = "♖", Color = "Purple" },
            new MyClass { Name = "⚒", Color = "Purple" },
            new MyClass { Name = "☢", Color = "Gold" },
            new MyClass { Name = "☭", Color = "Magenta" }
        };
    }

    public class MyClass
    {
        public string Name { get; set; }
        public string Color { get; set; }
    }
}