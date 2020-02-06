using System;
using Xamarin.Forms;
namespace DIPS.Xamarin.UI.Controls.Slidable
{
    /// <summary>
    /// To be added
    /// </summary>
    public class DefaultSliderView : BoxView, ISliderSelectable
    {
        /// <summary>
        /// To be added
        /// </summary>
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

        /// <summary>
        /// To be added
        /// </summary>
        /// <param name="selected"></param>
        public async void OnSelectionChanged(bool selected)
        {
            if (selected)
            {
                await this.FadeTo(1.0, 100);
            }
            else
            {
                await this.FadeTo(0.25, 100);
                //Opacity = 0.25;
            }
        }
    }
}
