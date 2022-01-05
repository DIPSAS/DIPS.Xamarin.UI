namespace DIPS.Xamarin.UI.Controls.Sheet
{
    /// <summary>
    /// Possible values for <see cref="SheetBehavior.SheetOpeningStrategy"/>.
    /// </summary>
    public enum SheetOpeningStrategyEnum
    {
        /// <summary>
        /// First value in <see cref="SheetBehavior.SnapPoints"/>.
        /// </summary>
        FirstSnapPoint,
        
        /// <summary>
        /// Last value in <see cref="SheetBehavior.SnapPoints"/>.
        /// </summary>
        LastSnapPoint,
        
        /// <summary>
        /// The value in <see cref="SheetBehavior.SnapPoints"/> that will fit all the content.
        /// </summary>
        MostFittingSnapPoint,
        
        /// <summary>
        /// Position that will show all content. Does NOT respect snap points.
        /// </summary>
        FitContent
    }
}