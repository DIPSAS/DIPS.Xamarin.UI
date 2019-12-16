using System;
using Xamarin.Forms;
namespace DIPS.Xamarin.UI.Controls.Slidable
{
    public class SlidableContentLayout : SlidableLayout
    {
        private AbsoluteLayout m_container = new AbsoluteLayout();
        public SlidableContentLayout() : base()
        {
            Content = m_container;
            m_container.IsClippedToBounds = true;
        }

        protected override void OnScrolled(double index, double offset)
        {
            if (Width < 0.1) return;
            base.OnScrolled(index, offset);
            var itemWidth = GetItemWidth();
            var totalWidth = (Width/ itemWidth);
            m_container.Children.Clear();
            m_container.BackgroundColor = Color.Orange;
            var intDex = (int)index;
            for (var i = intDex - totalWidth/2-1; i <= intDex + totalWidth/2+1; i++)
            {
                var pos = (int)Math.Floor(i);
                var isIndex = pos == (int)Math.Floor(index);
                var label = new Label { Text = "" + pos, VerticalOptions = LayoutOptions.Center, HorizontalOptions= LayoutOptions.Center, BackgroundColor = isIndex ? Color.Red:Color.Transparent };
                AbsoluteLayout.SetLayoutFlags(label, Config.WidthIsProportional ? AbsoluteLayoutFlags.SizeProportional : AbsoluteLayoutFlags.HeightProportional);
                AbsoluteLayout.SetLayoutBounds(label, new Rectangle(offset + itemWidth * (i-index), 0, Config.ElementWidth, 1));
                m_container.Children.Add(label);
            }
        }
    }
}
