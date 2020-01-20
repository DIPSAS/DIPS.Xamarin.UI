using System;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Modality.AttachedProperties
{
    /// <summary>
    /// Used to create an attached properties for modality context
    /// </summary>
    public class Modality
    {
        /// <summary>
        /// Set this property to an visual element inside of a modality component to close the modality component
        /// </summary>
        public static readonly BindableProperty CloseOnClickProperty =
            BindableProperty.CreateAttached("CloseOnClick", typeof(bool), typeof(Modality), false, propertyChanged: OnCloseOnClickChanged);

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
            view.SizeChanged += OnSizeChanged;
        }

        private static void OnSizeChanged(object sender, EventArgs e)
        {
            if (!(sender is View view)) return;
            var modalityLayout = view.GetParentOfType<ModalityLayout>();
            if (modalityLayout != null)
            {
                modalityLayout.AddOnCloseRecognizer(view);
            }   
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