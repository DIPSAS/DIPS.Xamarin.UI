namespace DIPS.Xamarin.UI.Controls.Slidable
{
    /// <summary>
    /// To be added
    /// </summary>
    public struct SlidableProperties
    {
        /// <summary>
        /// To be added
        /// </summary>
        /// <param name="position"></param>
        /// <param name="holdId"></param>
        /// <param name="isHeld"></param>
        internal SlidableProperties(double position, int holdId, bool isHeld)
        {
            Position = position;
            HoldId = holdId;
            IsHeld = isHeld;
            //tests
        }

        /// <summary>
        /// To be added
        /// </summary>
        /// <param name="position"></param>
        public SlidableProperties(double position) : this(position, -1, false)
        {
        }

        public static explicit operator SlidableProperties(double value) => new SlidableProperties(value);

        /// <summary>
        /// To be added
        /// </summary>
        public double Position { get; }

        /// <summary>
        /// To be added
        /// </summary>
        internal int HoldId { get; }

        /// <summary>
        /// To be added
        /// </summary>
        internal bool IsHeld { get; }
    }
}
