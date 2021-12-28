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

            foreach (var (dLength, dDuration) in latestDeltas)
            {
                length += dLength;
                duration += dDuration;
            }

            latestDeltas = new (float, long)[latestDeltas.Length];

            var velocity = Math.Abs(length) / duration * 1000;
            return (velocity >= threshold, velocity);
        }

        internal static int IndexOfClosestSnapPoint(this SheetView sheetView, double y)
        {
            var index = 0;
            var current = sheetView.RatioToYValue(sheetView.SnapPoints.FirstOrDefault(), sheetView.m_sheetBehaviour.Alignment);
            for (var i = 0; i < sheetView.SnapPoints.Count; i++)
            {
                var snapPoint = sheetView.RatioToYValue(sheetView.SnapPoints[i], sheetView.m_sheetBehaviour.Alignment);
                if (Math.Abs(snapPoint - y) < Math.Abs(current - y))
                {
                    current = snapPoint;
                    index = i;
                } 
            }

            return index;
        }

        internal static SheetView.DragDirection FindLatestDragDirection(ref (float, long)[] latestDeltas)
        {
            var direction =
                latestDeltas.Sum(tuple =>
                    tuple.Item1); // sum of latest movements to get the general/most likely intended direction
            return direction < 0 ? SheetView.DragDirection.Up : SheetView.DragDirection.Down;
        }

        internal static double RatioToYValue(this SheetView sheetView, double ratio, AlignmentOptions options)
        {
            return options switch
            {
                AlignmentOptions.Bottom => sheetView.Height - (ratio * sheetView.Height),
                AlignmentOptions.Top => (ratio * sheetView.Height) - sheetView.Height,
                _ => throw new ArgumentOutOfRangeException(nameof(options), options, null)
            };
        }

        internal static double YValueToRatio(this SheetView sheetView, double y)
        {
            return y / sheetView.Height;
        }

        internal static double CoerceRatio(double d)
        {
            return d > 1.0 ? 1 : d < 0.0 ? 0.0 : d;
        }


        internal static bool TryFindSnapPoint(this SheetView sheetView, SheetView.DragDirection dragDirection,
            double target, AlignmentOptions alignment, out double y)
        {
            switch (dragDirection)
            {
                case SheetView.DragDirection.Up when alignment == AlignmentOptions.Bottom:
                case SheetView.DragDirection.Down when alignment == AlignmentOptions.Top:
                    for (var i = 0; i < sheetView.SnapPoints.Count; i++)
                    {
                        if (sheetView.SnapPoints[i] < target)
                        {
                            continue;
                        }

                        if (sheetView.SnapPoints[i] is 0.0) break; // 0.0 should not be treated as a snap point
                        y = RatioToYValue(sheetView, sheetView.SnapPoints[i], alignment);
                        return true;
                    }

                    break;
                case SheetView.DragDirection.Down when alignment == AlignmentOptions.Bottom:
                case SheetView.DragDirection.Up when alignment == AlignmentOptions.Top:
                    for (var i = sheetView.SnapPoints.Count - 1; i >= 0; i--)
                    {
                        if (sheetView.SnapPoints[i] > target)
                        {
                            continue;
                        }
                        
                        if (sheetView.SnapPoints[i] is 0.0) break; // 0.0 should not be treated as a snap point
                        y = RatioToYValue(sheetView, sheetView.SnapPoints[i], alignment);
                        return true;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dragDirection), dragDirection, null);
            }

            y = -1;
            return false;
        }

        internal static void FindOpeningY(this SheetView sheetView, out double y)
        {
            switch (sheetView.m_sheetBehaviour.SheetOpeningStrategy)
            {
                case SheetOpeningStrategyEnum.FirstSnapPoint:
                    var ratio = sheetView.SnapPoints.FirstOrDefault() is 0.0
                        ? sheetView.SnapPoints.Skip(1).FirstOrDefault()
                        : sheetView.SnapPoints.FirstOrDefault();
                    y = RatioToYValue(sheetView, ratio,
                        sheetView.m_sheetBehaviour.Alignment);
                    break;
                case SheetOpeningStrategyEnum.MostFittingSnapPoint:
                    if (TryFindSnapPoint(sheetView,
                        sheetView.m_sheetBehaviour.Alignment == AlignmentOptions.Bottom
                            ? SheetView.DragDirection.Up
                            : SheetView.DragDirection.Down,
                        YValueToRatio(sheetView, sheetView.SheetContentHeightRequest),
                        sheetView.m_sheetBehaviour.Alignment, out y)) { }
                    else
                    {
                        y = RatioToYValue(sheetView, sheetView.SnapPoints.LastOrDefault(),
                            sheetView.m_sheetBehaviour.Alignment);
                    }

                    break;
                case SheetOpeningStrategyEnum.FitContent: //TODO: Must respect last snap point ??
                    y = sheetView.m_sheetBehaviour.Alignment switch
                    {
                        AlignmentOptions.Bottom => sheetView.Height - sheetView.SheetContentHeightRequest < 0
                            ? 0
                            : sheetView.Height - sheetView.SheetContentHeightRequest,
                        AlignmentOptions.Top => sheetView.SheetContentHeightRequest > sheetView.Height
                            ? sheetView.Height
                            : sheetView.SheetContentHeightRequest,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    break;
                case SheetOpeningStrategyEnum.LastSnapPoint:
                    y = RatioToYValue(sheetView, sheetView.SnapPoints.LastOrDefault(),
                        sheetView.m_sheetBehaviour.Alignment);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}