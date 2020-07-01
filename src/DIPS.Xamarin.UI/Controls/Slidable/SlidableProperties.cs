using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Slidable
{
    /// <summary>
    /// Properties used for SlideLayout
    /// </summary>
    public struct SlidableProperties
    {
        internal SlidableProperties(double position, int holdId, bool isHeld)
        {
            Position = position;
            HoldId = holdId;
            IsHeld = isHeld;
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="position"></param>
        public SlidableProperties(double position) : this(position, -1, false)
        {
        }

        /// <summary>
        /// Overload enabling cast from double to SlideableProperties
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator SlidableProperties(double value) => new SlidableProperties(value);

        /// <summary>
        /// Overload enabling cast from SlideableProperties to double
        /// </summary>
        /// <param name="value"></param>
        public static explicit operator double(SlidableProperties value) => value.Position;

        /// <summary>
        /// Position of the layout at the moment.
        /// </summary>
        public double Position { get; }

        internal int HoldId { get; }

        internal bool IsHeld { get; }

        private static int s_scrollToId = -42;
        public static void ScrollTo(Action<SlidableProperties> callback, Func<SlidableProperties> current, int index, int timeInMillis = 250)
        {
            if(current().IsHeld)
            {
                return;
            }

            var id = --s_scrollToId;
            var dx = index - current().Position;
            if(Math.Abs(dx) < 0.001 || timeInMillis < 20)
            {
                callback(new SlidableProperties(index));
                return;
            }

            var start = current().Position;
            var delta = dx / (double)timeInMillis;
            var s = Stopwatch.StartNew();
            callback(new SlidableProperties(start, id, false));
            Device.StartTimer(TimeSpan.FromMilliseconds(20), () =>
            {
                if(s_scrollToId != id || current().HoldId != id)
                {
                    return false;
                }

                var time = s.ElapsedMilliseconds;
                if(time >= timeInMillis)
                {
                    callback(new SlidableProperties(index, id, false));
                    return false;
                }
                else
                {
                    callback(new SlidableProperties(start + delta * time, id, false));
                }
                return true;
            });
        }
    }
}
