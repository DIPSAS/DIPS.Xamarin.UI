using System;
using Xamarin.Forms;
namespace DIPS.Xamarin.UI.Controls.Slidable
{
    internal class DefaultSliderView : BoxView, ISliderSelectable
    {
        public DefaultSliderView()
        {
            Margin = 0;
            WidthRequest = 3;
            Color = Color.DarkGreen;
            CornerRadius = 1;
            HorizontalOptions = LayoutOptions.Center;
            VerticalOptions = LayoutOptions.Center;
            Scale = 0.8;
        }

        public async void OnSelectionChanged(bool selected)
        {
            if (selected)
            {
                await this.FadeTo(1.0, 100);
            }
            else
            {
                await this.FadeTo(0.25, 100);
            }
        }
    }
}
