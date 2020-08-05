using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Controls.Sheet;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.Forms.IssuesRepro.Github159
{
    [Issue(159)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Github159 : ContentPage
    {
        public Github159()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            if (sheetBehavior.VerticalContentAlignment == ContentAlignment.Fit)
            {
                sheetBehavior.VerticalContentAlignment = ContentAlignment.Fill;
            }
            else
            {
                sheetBehavior.VerticalContentAlignment = ContentAlignment.Fit;
            }
            
        }
    }
}