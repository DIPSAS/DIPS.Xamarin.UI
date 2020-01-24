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
        /// To be added
        /// </summary>
        public int MaxValue { get; set; } = int.MaxValue;

        /// <summary>
        /// To be added
        /// </summary>
        public int MinValue { get; set; } = int.MinValue;
    }
}
