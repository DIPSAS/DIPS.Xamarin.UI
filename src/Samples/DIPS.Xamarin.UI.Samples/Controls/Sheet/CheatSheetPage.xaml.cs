using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Controls.Sheet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheatSheetPage : ContentPage
    {
        public CheatSheetPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            CheatSheet.Init();
        }

        private void OpenSheet(object sender, EventArgs e)
        {
            CheatSheet.Toggle();
        }
    }
}