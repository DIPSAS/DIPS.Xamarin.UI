using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using DIPS.Xamarin.UI.Android.Renderers.DatePicker;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using DatePicker = DIPS.Xamarin.UI.Controls.DatePicker.DatePicker;

[assembly: ExportRenderer(typeof(DatePicker), typeof(DatePickerImplementation))]

namespace DIPS.Xamarin.UI.Android.Renderers.DatePicker
{
    public class DatePickerImplementation : global::Xamarin.Forms.Platform.Android.DatePickerRenderer
    {
        private Controls.DatePicker.DatePicker m_datePicker;
        private Drawable m_defaultBackground;
        private int m_defaultBottomPadding;
        private LayoutParams m_defaultLayoutParmeters;
        private int m_defaultLeftPadding;
        private int m_defaultRightPadding;
        private int m_defaultTopPadding;
        public DatePickerImplementation(Context context) : base(context) { }

        public static void Init() { }

        protected override void OnElementChanged(ElementChangedEventArgs<global::Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null) Unsubscribe();

            if (e.NewElement != null)
                if (e.NewElement is Controls.DatePicker.DatePicker datePicker)
                {
                    m_datePicker = datePicker; ;

                    SubscribeToEvents();
                    SaveDefaultValues();
                    InitialChecks();
                }
        }

        private void InitialChecks()
        {
            if (!m_datePicker.HasBorder)
            {
                TurnOffBorder();
            }
        }

        private void Unsubscribe()
        {
            m_datePicker.PropertyChanged -= OnPropertyChanged;
        }

        private void SaveDefaultValues()
        {
            m_defaultBackground = Control.Background;
            m_defaultLayoutParmeters = Control.LayoutParameters;
            m_defaultLeftPadding = Control.PaddingLeft;
            m_defaultRightPadding = Control.PaddingRight;
            m_defaultBottomPadding = Control.PaddingBottom;
            m_defaultTopPadding = Control.PaddingTop;
        }

        private void SubscribeToEvents()
        {
            m_datePicker.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(m_datePicker.HasBorder)))
            {
                ToggleBorder();
            }
        }

        private void ToggleBorder()
        {
            if (m_datePicker.HasBorder)
            {
                TurnOnBorder();
            }
            else
            {
                TurnOffBorder();
            }
        }

        private void TurnOnBorder()
        {
            Control.Background = m_defaultBackground;
            Control.LayoutParameters = m_defaultLayoutParmeters;
            Control.SetPadding(m_defaultLeftPadding, m_defaultTopPadding, m_defaultRightPadding, m_defaultBottomPadding);
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