namespace DIPS.Xamarin.UI.Controls.TrendGraph
{
    /// <summary>
    ///     Animation types for <see cref="TrendGraph" />
    ///     <remarks>Default is <see cref="None" /></remarks>
    /// </summary>
    public enum TrendAnimation
    {
        /// <summary>
        ///     No animation
        /// </summary>
        None = 0,

        /// <summary>
        ///     Growing bars with faster growing background animation
        /// </summary>
        GrowAll,

        /// <summary>
        ///     Growing bars with faster fading background animation
        /// </summary>
        GrowAndFade,

        /// <summary>
        ///     Growing bars with fixed background animation
        /// </summary>
        GrowAndNone
    }
}