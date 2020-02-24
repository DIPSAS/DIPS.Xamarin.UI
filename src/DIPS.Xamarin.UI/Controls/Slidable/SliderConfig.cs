namespace DIPS.Xamarin.UI.Controls.Slidable
{
    /// <summary>
    /// To be added
    /// </summary>
    public class SliderConfig
    {
        /// <summary>
        /// To be added
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public SliderConfig(int min, int max)
        {
            MaxValue = max;
            MinValue = min;
        }

        /// <summary>
        /// To be added
        /// </summary>
        public SliderConfig() { }

        /// <summary>
        /// Indicates if the children of this component should get a positive value going downwards.
        /// Setting this to true will swap the Max and Min Order
        /// </summary>
        public bool IsRightToLeft { get; set; } = false;

        /// <summary>
        /// To be added
        /// </summary>
        public int MaxValue { get; set; } = int.MaxValue;

        /// <summary>
        /// To be added
        /// </summary>
        public int MinValue { get; set; } = int.MinValue;
    }
}
