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
                Opacity = 1;
                await ((View)this).ScaleTo(1.0, 150, Easing.BounceIn);
            }
            else
            {
                Opacity = 0.25;
                await ((View)this).ScaleTo(0.8, 150, Easing.BounceIn);
            }
        }
    }
}
