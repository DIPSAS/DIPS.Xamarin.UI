using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Samples
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            var color = Colors.GetColor(ColorName.color_primary_light_primary_100);
            BoxView.BackgroundColor = color;
        }
    }
}