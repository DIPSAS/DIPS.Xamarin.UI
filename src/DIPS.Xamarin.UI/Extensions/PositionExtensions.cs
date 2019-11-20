using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Extensions
{
    public static class PositionExtensions
    {
        public static double GetX(this View item, View relativeParent)
        {
            var x = item.X;
            var parent = (View)item.Parent;
            while (parent != relativeParent && parent != null)
            {
                x += parent.X;
                parent = (View)parent.Parent;
            }

            return x;
        }

        public static double GetY(this View item, View relativeParent)
        {
            var y = item.Y;
            var parent = (View)item.Parent;
            while (parent != relativeParent && parent != null)
            {
                y += parent.Y;
                parent = (View)parent.Parent;
            }

            return y;
        }
    }
}
