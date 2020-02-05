using System;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Modality
{
    /// <summary>
    /// Relative layout which exposes InvalidateLayout
    /// </summary>
    public class ModalityRelativeLayout : RelativeLayout
    {
        /// <summary>
        /// Calls the protected InvalidateLayout
        /// </summary>
        public new void InvalidateLayout()
        {
            base.InvalidateLayout();
        }
    }
}
