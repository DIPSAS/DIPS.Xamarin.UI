using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Toast
{
    /// <summary>
    ///     Toast L that would appear on top of the presented view
    /// </summary>
    public class ToastLayout : BindableObject
    {
        /// <summary>
        ///     Gets or sets the color which will fill the background of the Toast.
        /// </summary>
        public Color BackgroundColor { get; set; }

        /// <summary>
        ///     Gets or sets the corner radius of the Toast.
        /// </summary>
        public float CornerRadius { get; set; }

        /// <summary>
        ///     Gets or sets the font family to which the font for the Toast belongs.
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        ///     Gets or sets the size of the font for the Toast.
        /// </summary>
        [TypeConverter(typeof(FontSizeConverter))]
        public double FontSize { get; set; }

        /// <summary>
        ///     Gets or sets the LineBreakMode for the Toast.
        /// </summary>
        public LineBreakMode LineBreakMode { get; set; }

        /// <summary>
        ///     Gets or sets a flag indicating if the Toast has a shadow displayed.
        /// </summary>
        public bool HasShadow { get; set; }

        /// <summary>
        ///     Gets or sets the maximum number of lines allowed in the Toast.
        /// </summary>
        public int MaxLines { get; set; }

        /// <summary>
        ///     Gets or sets the inner padding of the Toast text.
        ///     <remarks>
        ///         The padding is the space between the bounds of a Toast and the bounding region into which its Text property
        ///         should be arranged into.
        ///     </remarks>
        /// </summary>
        public Thickness Padding { get; set; }

        /// <summary>
        ///     The vertical positioning of the toast in a percentage of the Main Page relative to the top margin of the Main
        ///     page.
        /// </summary>
        public double PositionY { get; set; }

        /// <summary>
        ///     Gets or sets the Color for the text of this Toast.
        /// </summary>
        public Color TextColor { get; set; }
    }
}