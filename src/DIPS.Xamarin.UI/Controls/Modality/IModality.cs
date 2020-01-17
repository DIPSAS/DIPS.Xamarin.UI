using System;
using System.Collections.Generic;
using System.Text;

namespace DIPS.Xamarin.UI.Controls.Modality
{
    /// <summary>
    /// An interface to communicate between the modality layout and a modality component
    /// </summary>
    public interface IModality
    {
        /// <summary>
        /// Hides the modality component
        /// </summary>
        void Hide();
    }
}
