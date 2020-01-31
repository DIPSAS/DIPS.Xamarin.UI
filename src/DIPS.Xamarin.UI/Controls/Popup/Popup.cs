using System;
using DIPS.Xamarin.UI.Controls.Modality;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Popup
{
    /// <summary>
    /// Used to create an Attached property to be used with PopupBehavior
    /// </summary>
    public class Popup : Modality.AttachedProperties.Modality
    {
        /// <summary>
        /// Set this property on the element your popup is attached to, to not open the popup when clicking the item.
        /// </summary>
        public static readonly BindableProperty OpenOnClickProperty =
            BindableProperty.CreateAttached("OpenOnClick", typeof(bool), typeof(Popup), true);

        /// <summary>
        /// <see cref="OpenOnClickProperty" />
        /// </summary>
        /// <param name="view"></param>
        /// <param name="value"></param>
        public static void SetOpenOnClick(BindableObject view, bool value)
        {
            view.SetValue(OpenOnClickProperty, value);
        }

        /// <summary>
        /// <see cref="OpenOnClickProperty" />
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static bool GetOpenOnClick(BindableObject view)
        {
            return (bool)view.GetValue(OpenOnClickProperty);
        }
    }
}
