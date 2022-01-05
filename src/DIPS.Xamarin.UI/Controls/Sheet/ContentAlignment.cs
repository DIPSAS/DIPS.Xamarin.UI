namespace DIPS.Xamarin.UI.Controls.Sheet
{
    /// <summary>
    ///     The alignment of the content of the sheet.
    /// </summary>
    public enum ContentAlignment
    {
        /// <summary>
        ///     The content will only use as much space as it needs.
        /// </summary>
        Fit = 0,

        /// <summary>
        ///     The content will use the entire sheet as space.
        /// </summary>
        Fill,

        /// <summary>
        ///     The content will use the same space as the sheet
        /// </summary>
        SameAsSheet
    }
}