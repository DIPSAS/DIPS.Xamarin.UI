namespace DIPS.Xamarin.UI.Controls.Slidable
{
    public class SliderConfig
    {
        public SliderConfig(int min, int max)
        {
            MaxValue = max;
            MinValue = min;
        }

        public SliderConfig() { }

        public int MaxValue { get; set; } = int.MaxValue;
        public int MinValue { get; set; } = int.MinValue;
    }
}
