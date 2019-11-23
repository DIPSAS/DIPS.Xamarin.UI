using System.Diagnostics.CodeAnalysis;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Extensions
{
    /// <summary>
    /// Extension to view for getting the position values of an item relative to a certain parent.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class PositionExtensions
    {
        /// <summary>
        /// Gets the x position relative to relativeParent of item
        /// </summary>
        /// <param name="item">Item to find the X value of</param>
        /// <param name="relativeParent">The parent to find the X value relative to. If null, this will find relative to the page</param>
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
        /// <param name="item">Item to find the Y value of</param>
        /// <param name="relativeParent">The parent to find the Y value relative to. If null, this will find relative to the page</param>
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

        /// <summary>
        /// Calculates the relative position of the value regarding max and min
        /// </summary>
        /// <param name="value">The value to find relative to max and min</param>
        /// <param name="minValue">Max value to cap towards if higher</param>
        /// <param name="maxValue">Min value to cap towards if lower</param>
        /// <returns></returns>
        public static double CalculateRelativePosition(this double value, double minValue, double maxValue)
        {
            if (value >= maxValue) return 1.0;
            if (value <= minValue) return 0.0;
            var totalDiff = maxValue - minValue;
            var valDiff = value - minValue;
            return valDiff / totalDiff;
        }
    }
}
