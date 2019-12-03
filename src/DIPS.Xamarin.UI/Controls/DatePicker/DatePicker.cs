using System;
using System.Diagnostics.CodeAnalysis;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.DatePicker
{
    /// <inheritdoc />
    /// This has extended properties that is not available in the standard Xamarin.Forms.DatePicker
    [ExcludeFromCodeCoverage]
    public class DatePicker : global::Xamarin.Forms.DatePicker
    {
        /// <summary>
        /// An obsolete property that was used to indicate wheter or not the date picker should have its native border turned on / off.
        /// <remarks>This property is obsolete, and all date pickers will have its native border turned off now https://github.com/DIPSAS/DIPS.Xamarin.UI/issues/46</remarks>
        /// </summary>
        [Obsolete("Has border property is removed and no longer has any effect, The date picker should always remove it's native border. https://github.com/DIPSAS/DIPS.Xamarin.UI/issues/46")]
        public bool HasBorder { get; set; }
    }
}
