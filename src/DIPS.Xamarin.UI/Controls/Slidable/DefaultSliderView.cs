using System;
using DIPS.Xamarin.UI.Resources.Colors;
using Xamarin.Forms;
namespace DIPS.Xamarin.UI.Controls.Slidable
{
    internal class DefaultSliderView : BoxView
    {
        public DefaultSliderView()
        {
            Margin = 0;
            WidthRequest = 1;
            Color = Theme.TealPrimaryAir;
            CornerRadius = 0;
            HorizontalOptions = LayoutOptions.Center;
            VerticalOptions = LayoutOptions.Center;
        }
    }
}
