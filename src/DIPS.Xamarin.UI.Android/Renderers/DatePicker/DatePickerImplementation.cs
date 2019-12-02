using Android.Content;
using DIPS.Xamarin.UI.Android.Renderers.DatePicker;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using DatePicker = DIPS.Xamarin.UI.Controls.DatePicker.DatePicker;

[assembly: ExportRenderer(typeof(DatePicker), typeof(DatePickerImplementation))]

namespace DIPS.Xamarin.UI.Android.Renderers.DatePicker
{
    /// <inheritdoc />
    public class DatePickerImplementation : DatePickerRenderer
    {
        /// <inheritdoc />
        public DatePickerImplementation(Context context) : base(context) { }

        /// <summary>
        ///     Method to use when linking in order to keep the assembly
        /// </summary>
        public static void Initialize() { }

        /// <inheritdoc />
        protected override void OnElementChanged(ElementChangedEventArgs<global::Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                //Clean up
            }

            if (e.NewElement is Controls.DatePicker.DatePicker)
            {
                //Initialize
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