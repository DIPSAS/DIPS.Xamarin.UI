using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Controls.Sheet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SheetPage : ContentPage
    {
        public SheetPage()
        {
            InitializeComponent();
        }

        private void MoveSheet(object sender, EventArgs e)
        {
            SheetBehavior.MoveTo(0.7);
        }
    }
}