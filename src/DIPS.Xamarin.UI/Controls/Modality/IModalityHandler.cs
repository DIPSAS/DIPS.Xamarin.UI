using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIPS.Xamarin.UI.Controls.Modality
{
    /// <summary>
    /// An interface to communicate between the modality layout and a modality component
    /// </summary>
    public interface IModalityHandler
    {
        /// <summary>
        /// Method that gets invoked when the user clicks the overlay and wants to hide the modal component
        /// </summary>
        void Hide();

        /// <summary>
        /// Task that should be ran before removal of the current modal component
        /// </summary>
        /// <returns></returns>
        Task BeforeRemoval();

        /// <summary>
        /// Task that should be ran after removal of the current modal component
        /// </summary>
        /// <returns></returns>
        Task AfterRemoval();
    }
}
