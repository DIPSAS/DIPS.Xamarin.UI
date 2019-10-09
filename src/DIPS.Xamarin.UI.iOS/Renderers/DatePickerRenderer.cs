using System.ComponentModel;
using UIKit;

namespace DIPS.Xamarin.UI.iOS.Renderers
{
    public class DatePickerRenderer : global::Xamarin.Forms.Platform.iOS.DatePickerRenderer
    {
        public static void Init() { }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);


            TurnOffBorder();
        }

        private void TurnOffBorder()
        {
            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;
        }
    }
}