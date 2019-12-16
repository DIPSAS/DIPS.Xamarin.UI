namespace DIPS.Xamarin.UI.Controls.Slidable
{
    public struct SlidableProperties
    {
        public SlidableProperties(double position, int holdId, bool isHeld)
        {
            Position = position;
            HoldId = holdId;
            IsHeld = isHeld;
        }

        public SlidableProperties(double position) : this(position, -1, false)
        {
        }

        public double Position { get; }
        public int HoldId { get; }
        public bool IsHeld { get; }
    }
}
