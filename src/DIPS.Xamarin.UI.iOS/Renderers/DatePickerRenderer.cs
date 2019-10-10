using System.ComponentModel;
using DIPS.Xamarin.UI.iOS.Renderers;
using UIKit;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(DIPS.Xamarin.UI.Controls.DatePicker), typeof(DatePickerRenderer))]
namespace DIPS.Xamarin.UI.iOS.Renderers
{
    public class DatePickerRenderer : global::Xamarin.Forms.Platform.iOS.DatePickerRenderer
    {
        public static void Init() { }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            //Check shared implementation to see if `HasBorder` is set
            TurnOffBorder();
        }

        private void TurnOffBorder()
        {
            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;
        }
    }
}