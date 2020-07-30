using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Toast
{
    /// <summary>
    ///     Set layout options for the Toast control
    /// </summary>
    public class ToastLayout : BindableObject
    {
        /// <summary>
        ///     Gets or sets the color which will fill the background of the Toast.
        ///     <remarks>Default value is <see cref="Color.Black" /></remarks>
        /// </summary>
        public Color BackgroundColor { get; set; } = Color.Black;

        /// <summary>
        ///     Gets or sets the corner radius of the Toast.
        ///     <remarks>Default value is 8</remarks>
        /// </summary>
        public float CornerRadius { get; set; } = 8;

        /// <summary>
        ///     Gets or sets the font family to which the font for the Toast belongs.
        /// </summary>
        public string FontFamily { get; set; }

        /// <summary>
        ///     Gets or sets the size of the font for the Toast.
        ///     <remarks>Default value is 11</remarks>
        /// </summary>
        public double FontSize { get; set; } = 11;

        /// <summary>
        ///     Gets or sets the LineBreakMode for the Toast.
        ///     <remarks>Default value is <see cref="LineBreakMode.TailTruncation" /></remarks>
        /// </summary>
        public LineBreakMode LineBreakMode { get; set; } = LineBreakMode.TailTruncation;

        /// <summary>
        ///     Gets or sets a flag indicating if the Toast has a shadow displayed.
        /// </summary>
        public bool HasShadow { get; set; }

        /// <summary>
        ///     The horizontal margins of the Toast control in device pixels
        ///     <remarks>Default value is 10</remarks>
        /// </summary>
        public double HorizontalMargin { get; set; } = 10;

        /// <summary>
        ///     Gets or sets the maximum number of lines allowed in the Toast.
        ///     <remarks>Default value is 1</remarks>
        /// </summary>
        public int MaxLines { get; set; } = 1;

        /// <summary>
        ///     Gets or sets the inner padding of the Toast text.
        ///     <remarks>
        ///         The padding is the space between the bounds of a Toast and the bounding region into which its Text property
        ///         should be arranged into.
        ///         Default value is 8 on all sides"/>
        ///     </remarks>
        /// </summary>
        public Thickness Padding { get; set; } = new Thickness(8);

        /// <summary>
        ///     Gets or sets the Color for the text of this Toast.
        ///     <remarks>Default value is <see cref="Color.White" /></remarks>
        /// </summary>
        public Color TextColor { get; set; } = Color.White;

        /// <summary>
        ///     The vertical positioning of the toast from the Navigation Bar in device pixels
        ///     <remarks>Default value is 10 off from the Navigation Bar</remarks>
        /// </summary>
        public double YPosition { get; set; } = 10;
    }
}