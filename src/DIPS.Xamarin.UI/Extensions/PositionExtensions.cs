using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Extensions
{
    /// <summary>
    /// Extension to view for getting the position values of an item relative to a certain parent.
    /// </summary>
    public static class PositionExtensions
    {
        /// <summary>
        /// Gets the x position relative to relativeParent of item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="relativeParent"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the y position relative to relativeParent of item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="relativeParent"></param>
        /// <returns></returns>
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
