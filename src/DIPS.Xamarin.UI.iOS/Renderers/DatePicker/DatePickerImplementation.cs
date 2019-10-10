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
    public class DatePickerImplementation : DatePickerRenderer
    {
        private Controls.DatePicker.DatePicker m_datePicker;
        private nfloat m_defaultLayerBorderWith;
        private UITextBorderStyle m_defaultBorderStyle;

        public new static void Init() { }

        protected override void OnElementChanged(ElementChangedEventArgs<global::Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                Unsubscribe();
            }

            if (e.NewElement != null)
            {
                if (e.NewElement is Controls.DatePicker.DatePicker datePicker)
                {
                    m_datePicker = datePicker;

                    SubscribeToEvents();
                    SaveDefaultValues();
                    InitialChecks();
                }
            }
        }

        private void InitialChecks()
        {
            if (!m_datePicker.HasBorder)
            {
                TurnOffBorder();
            }
        }

        private void SaveDefaultValues()
        {
            m_defaultLayerBorderWith = Control.Layer.BorderWidth;
            m_defaultBorderStyle = Control.BorderStyle;

        }

        private void Unsubscribe()
        {
            m_datePicker.PropertyChanged -= OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(m_datePicker.HasBorder)))
            {
                ToggleBorder();
            }
        }

        private void SubscribeToEvents()
        {
            m_datePicker.PropertyChanged += OnPropertyChanged;
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
            Control.Layer.BorderWidth = m_defaultLayerBorderWith;
            Control.BorderStyle = m_defaultBorderStyle;
        }

        private void TurnOffBorder()
        {
            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;
        }
    }
}