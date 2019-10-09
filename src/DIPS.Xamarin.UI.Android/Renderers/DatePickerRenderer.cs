using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace DIPS.Xamarin.UI.Android.Renderers
{
    public class DatePickerRenderer : global::Xamarin.Forms.Platform.Android.DatePickerRenderer
    {
        public DatePickerRenderer(Context context) : base(context) { }

        public static void Init() { }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                //Check shared implementation to see if `HasBorder` is set
                TurnOffBorder();
            }
        }

        private void TurnOffBorder()
        {
            Control.Background = null;

            var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
            layoutParams.SetMargins(0, 0, 0, 0);
            LayoutParameters = layoutParams;
            Control.LayoutParameters = layoutParams;
            Control.SetPadding(0, 0, 0, 0);
            SetPadding(0, 0, 0, 0);
        }
    }
}