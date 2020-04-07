using System;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Skeleton
{
    /// <summary>
    /// Defines the shape of a skeleton view
    /// </summary>
    public class SkeletonShape
    {
        /// <summary>
        /// Which Row to place the skeleton part inn
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Which Column to place the skeleton inn
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// Rowspan of the skeleton
        /// </summary>
        public int RowSpan { get; set; } = 1;

        /// <summary>
        /// ColumnSpan of the skeleton
        /// </summary>
        public int ColumnSpan { get; set; } = 1;

        /// <summary>
        /// Corner Radius of the Skeleton
        /// </summary>
        public double CornerRadius { get; set; }

        /// <summary>
        /// Width of the skeleton. Default is -1 and row/column will be used
        /// </summary>
        public double Width { get; set; } = -1;

        /// <summary>
        /// Height of the skeleton. Default is -1 and row/column will be used
        /// </summary>
        public double Height { get; set; } = -1;

        /// <summary>
        /// Horizontal alignment of the skeleton
        /// </summary>
        public LayoutOptions HorizontalAlignment { get; set; } = LayoutOptions.FillAndExpand;

        /// <summary>
        /// Vertical alignment of the skeleton
        /// </summary>
        public LayoutOptions VerticalAlignment { get; set; } = LayoutOptions.FillAndExpand;

        /// <summary>
        /// Margin of the skeleton
        /// </summary>
        public int Margin { get; set; }
    }
}
