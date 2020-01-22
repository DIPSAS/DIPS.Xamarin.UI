using System;
using DIPS.Xamarin.UI.Controls.Modality;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Popup
{
    /// <summary>
    /// Used to create an Attached property to be used in a PopupBehavior
    /// </summary>
    [Obsolete("Popup attached property is obsolete because the attached property fits better in a modality context. Please use Modality attached property instead")]
    public class Popup : Modality.AttachedProperties.Modality
    {
       
    }
}
