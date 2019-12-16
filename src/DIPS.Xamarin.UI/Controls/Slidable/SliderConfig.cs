namespace DIPS.Xamarin.UI.Controls.Slidable
{
    public class SliderConfig
    {
        public SliderConfig(int min, 
            int max, 
            double elementWidth, 
            bool widthIsProportional, 
            bool upToTheLeft)
        {
            MaxValue = max;
            ElementWidth = elementWidth;
            WidthIsProportional = widthIsProportional;
            UpToTheLeft = upToTheLeft;
            MinValue = min;
        }
        public SliderConfig() { }

        public int MaxValue { get; set; } = int.MaxValue;
        public int MinValue { get; set; } = int.MinValue;
        public double ElementWidth { get; set; } = 0.2;
        public bool WidthIsProportional { get; set; } = true;
        public bool UpToTheLeft { get; set; } = false;
    }
}
