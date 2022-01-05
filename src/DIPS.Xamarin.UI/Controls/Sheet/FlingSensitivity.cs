namespace DIPS.Xamarin.UI.Controls.Sheet
{
    /// <summary>
    /// Possible values for <see cref="SheetBehavior.FlingSpeedThreshold"/>.
    /// </summary>
    public enum FlingSensitivity
    {
        /// <summary>
        /// 3500 pixels per second
        /// </summary>
        Low,
        
        /// <summary>
        /// 2000 pixels per second
        /// </summary>
        Medium,
        
        /// <summary>
        /// 1000 pixels per second
        /// </summary>
        High
    }

    internal static class FlingConstants
    {
        internal static int s_low = 3500;
        internal static int s_medium = 2000;
        internal static int s_high = 1000;
    }
}