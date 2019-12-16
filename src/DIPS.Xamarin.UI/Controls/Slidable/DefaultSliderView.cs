using System;
using Xamarin.Forms;
namespace DIPS.Xamarin.UI.Controls.Slidable
{
    public class DefaultSliderView : BoxView, ISliderSelectable
    {
        public DefaultSliderView()
        {
            Margin = 0;
            WidthRequest = 3;
            Color = Color.DarkGreen;
            CornerRadius = 1;
            HorizontalOptions = LayoutOptions.Center;
            VerticalOptions = LayoutOptions.Center;
        }

        public void OnSelectionChanged(bool selected)
        {
            if (selected)
            {
                Opacity = 1;
            }
            else
            {
                Opacity = 0.25;
            }
        }
    }
}
