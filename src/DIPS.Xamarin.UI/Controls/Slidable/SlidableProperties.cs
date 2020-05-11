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
    }
}
