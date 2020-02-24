namespace DIPS.Xamarin.UI.Controls.Slidable
{
    /// <summary>
    /// Config defining the max and min of SlideableLayout
    /// </summary>
    public class SliderConfig
    {
        /// <summary>
        /// Creates an instance with max and min set.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public SliderConfig(int min, int max)
        {
            MaxValue = max;
            MinValue = min;
        }

        /// <summary>
        /// Empty constructor giving max and min, int.max/int.min values.
        /// </summary>
        public SliderConfig() { }

        /// <summary>
        /// Indicates if the children of this component should get a positive value going downwards.
        /// Setting this to true will swap the Max and Min Order
        /// </summary>
        public bool IsRightToLeft { get; set; } = false;

        /// <summary>
        /// Inclusive max value of the SlidableLayout
        /// </summary>
        public int MaxValue { get; set; } = int.MaxValue;

        /// <summary>
        /// Inclusive min value of the SlidableLayout
        /// </summary>
        public int MinValue { get; set; } = int.MinValue;
    }
}
