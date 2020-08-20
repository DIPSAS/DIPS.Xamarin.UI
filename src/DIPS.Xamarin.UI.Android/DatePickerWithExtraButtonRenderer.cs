using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using Android.App;
using Android.OS;
using System.ComponentModel;
using DIPS.Xamarin.UI.Android;
using DIPS.Xamarin.UI.Internal;

[assembly: ExportRenderer(typeof(DatePickerWithExtraButton), typeof(DatePickerWithExtraButtonRenderer))]
namespace DIPS.Xamarin.UI.Android
{
    internal class DatePickerWithExtraButtonRenderer : DatePickerRenderer, IDialogInterfaceOnClickListener
    {
        internal static void Initialize() { }

        private DatePickerWithExtraButton m_datepickerWithExtraButton;
        private DatePickerDialog m_dialog;

        public DatePickerWithExtraButtonRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement is DatePickerWithExtraButton oldDatePicker && Control != null)
            {

            }

            if (e.NewElement is DatePickerWithExtraButton newDatePicker && Control != null)
            {
                m_datepickerWithExtraButton = newDatePicker;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(nameof(DatePickerWithExtraButton.ExtraButtonText)))
            {
                if (m_dialog != null)
                {
                    m_dialog.GetButton((int)DialogButtonType.Neutral).Text = m_datepickerWithExtraButton.ExtraButtonText;
                }
            }
        }

        protected override DatePickerDialog CreateDatePickerDialog(int year, int month, int day)
        {
            m_dialog = base.CreateDatePickerDialog(year, month, day);
            m_dialog.SetButton((int)DialogButtonType.Neutral, m_datepickerWithExtraButton.ExtraButtonText, this);
            return m_dialog;
        }

        public void OnClick(IDialogInterface dialog, int which)
        {
            if (which == (int)DialogButtonType.Neutral)
            {
                m_datepickerWithExtraButton.ExtraButtonCommand?.Execute(m_datepickerWithExtraButton.ExtraButtonCommandParameter);
            }
        }
    }
}