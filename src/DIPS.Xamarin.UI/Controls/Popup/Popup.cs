using DIPS.Xamarin.UI.Controls.Modality;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Popup
{
    /// <summary>
    /// Used to create an Attached property to be used in a PopupBehavior
    /// </summary>
    public class Popup
    {
        /// <summary>
        /// Set this on every item inside you PopupBehavior.Content for it to automatically close the popup on click.
        /// </summary>
        public static readonly BindableProperty CloseOnClickProperty =
            BindableProperty.CreateAttached("CloseOnClick", typeof(bool), typeof(Popup), false, propertyChanged: OnCloseOnClickChanged);

        /// <summary>
        /// <see cref="CloseOnClickProperty" />
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public static void OnCloseOnClickChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if ((bool?)newValue != true)
            {
                return;
            }

            var view = (View)bindable;
            view.BindingContextChanged += (s, e) =>
            {
                var modalityLayout = view.GetParentOfType<ModalityLayout>();
                if (modalityLayout != null)
                {
                    modalityLayout.AddOnCloseRecognizer(view);
                }
            };
        }

        /// <summary>
        /// <see cref="CloseOnClickProperty" />
        /// </summary>
        /// <param name="view"></param>
        /// <param name="value"></param>
        public static void SetCloseOnClick(BindableObject view, bool value)
        {
            view.SetValue(CloseOnClickProperty, value);
        }

        /// <summary>
        /// <see cref="CloseOnClickProperty" />
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public static bool GetCloseOnClick(BindableObject view)
        {
            return (bool)view.GetValue(CloseOnClickProperty);
        }

    }
}
