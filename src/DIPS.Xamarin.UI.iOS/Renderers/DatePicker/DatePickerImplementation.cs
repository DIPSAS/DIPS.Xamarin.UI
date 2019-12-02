using System;
using System.ComponentModel;
using DIPS.Xamarin.UI.iOS.Renderers.DatePicker;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using DatePicker = DIPS.Xamarin.UI.Controls.DatePicker.DatePicker;

[assembly: ExportRenderer(typeof(DatePicker), typeof(DatePickerImplementation))]
namespace DIPS.Xamarin.UI.iOS.Renderers.DatePicker
{
    /// <inheritdoc />
    public class DatePickerImplementation : DatePickerRenderer
    {
        /// <summary>
        /// Method to use when linking in order to keep the assembly
        /// </summary>
        public static void Initialize() { }

        /// <inheritdoc />
        protected override void OnElementChanged(ElementChangedEventArgs<global::Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // Clean up
            }

            
            if (e.NewElement is Controls.DatePicker.DatePicker)
            {
                // Initialize
                TurnOffBorder();
            }
        }

        private void TurnOffBorder()
        {
            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;
        }
    }
}