using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using Android.App;
using Android.OS;
using System.ComponentModel;
using DIPS.Xamarin.UI.Android;
using DIPS.Xamarin.UI.Internal;
using Google.Android.Material.BottomSheet;

[assembly: ExportRenderer(typeof(InternalDatePicker), typeof(InternalDatePickerRenderer))]
namespace DIPS.Xamarin.UI.Android
{
    internal class InternalDatePickerRenderer : DatePickerRenderer, IDialogInterfaceOnClickListener
    {
        internal static void Initialize() { }

        private InternalDatePicker m_datepickerWithExtraButton;
        private DatePickerDialog m_dialog;

        public InternalDatePickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement is InternalDatePicker oldDatePicker && Control != null)
            {
                //Dispose
            }
            
            if (e.NewElement is InternalDatePicker newDatePicker && Control != null)
            {
                m_datepickerWithExtraButton = newDatePicker;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(nameof(InternalDatePicker.ExtraButtonText)))
            {
                SetExtraButtonText();

            }
        }

        private void SetExtraButtonText()
        {
            if (m_dialog != null)
            {
                if (!string.IsNullOrEmpty(m_datepickerWithExtraButton.ExtraButtonText))
                {
                    m_dialog.GetButton((int)DialogButtonType.Neutral).Text = m_datepickerWithExtraButton.ExtraButtonText;
                }
                else
                {
                    m_dialog.SetButton((int)DialogButtonType.Neutral, m_datepickerWithExtraButton.ExtraButtonText, this);
                }
            }
        }

        protected override DatePickerDialog CreateDatePickerDialog(int year, int month, int day)
        {
            m_dialog = base.CreateDatePickerDialog(year, month, day);

            if(!string.IsNullOrEmpty(m_datepickerWithExtraButton.ExtraButtonText))
            {
                m_dialog.SetButton((int)DialogButtonType.Neutral, m_datepickerWithExtraButton.ExtraButtonText, this);
            }
            
            return m_dialog;
        }

        public void OnClick(IDialogInterface dialog, int which)
        {
            if (which == (int)DialogButtonType.Neutral)
            {
                m_datepickerWithExtraButton.OnExtraButtonClicked?.Invoke();
            }
        }
    }
}