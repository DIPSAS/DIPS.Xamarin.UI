using System;
using System.Linq;
using DIPS.Xamarin.UI.Controls.Sheet;

namespace DIPS.Xamarin.UI.Internal.Xaml.Sheet
{
    internal static class SheetViewUtility
    {
        internal static (bool, float) IsThresholdReached(ref (float, long)[] latestDeltas, int threshold)
        {
            var length = 0f;
            var duration = 0L;

            foreach (var deltaTuple in latestDeltas)
            {
                length += deltaTuple.Item1;
                duration += deltaTuple.Item2;
            }

            latestDeltas = new (float, long)[latestDeltas.Length];

            var velocity = Math.Abs(length) / duration * 1000;
            return (velocity >= threshold, velocity);
        }

        internal static SheetView.DragDirection FindLatestDragDirection(ref (float, long)[] latestDeltas)
        {
            var direction =
                latestDeltas.Sum(tuple =>
                    tuple.Item1); // sum of latest movements to get the general/most likely intended direction
            return direction < 0 ? SheetView.DragDirection.Up : SheetView.DragDirection.Down;
        }

        internal static double RatioToYValue(double ratio, double height, AlignmentOptions options)
        {
            return options switch
            {
                AlignmentOptions.Bottom => height - (ratio * height),
                AlignmentOptions.Top => (ratio * height) - height,
                _ => throw new ArgumentOutOfRangeException(nameof(options), options, null)
            };
        }

        internal static double YValueToRatio(double y, double height)
        {
            return y / height;
        }

        internal static double CoerceRatio(double d)
        {
            return d > 1.0 ? 1 : d < 0.0 ? 0.0 : d;
        }
        
    }
}