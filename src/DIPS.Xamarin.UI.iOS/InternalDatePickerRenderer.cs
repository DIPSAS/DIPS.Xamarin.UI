using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DIPS.Xamarin.UI.Internal;
using DIPS.Xamarin.UI.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(InternalDatePicker), typeof(InternalDatePickerRenderer))]
namespace DIPS.Xamarin.UI.iOS
{
    internal class InternalDatePickerRenderer : DatePickerRenderer
    {
        internal static void Initialize() { }

        private const string ExtraButtonIdentifier = "ExtraDatePickerUIBarButtonItem";
        private InternalDatePicker m_internalDatePicker;
        private UIToolbar m_toolBar;
        private UIDatePicker m_uiDatePicker;

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement is InternalDatePicker oldDatePicker && Control != null)
            {
                //Dispose
            }

            if (e.NewElement is InternalDatePicker newDatePicker && Control != null)
            {
                m_internalDatePicker = newDatePicker;
                AddExtraUIBarButtonItemToToolbar();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals(nameof(InternalDatePicker.ExtraButtonText)))
            {
                SetExtraButtonTitle();
            }
        }

        private void AddExtraUIBarButtonItemToToolbar()
        {
            if (Control.InputAccessoryView is UIToolbar toolbar && Control.InputView is UIDatePicker uiDatePicker && !string.IsNullOrEmpty(m_internalDatePicker.ExtraButtonText))
            {
                m_toolBar = toolbar;
                m_uiDatePicker = uiDatePicker;
                var previousItems = toolbar.Items;
                var extraButton = new UIBarButtonItem(m_internalDatePicker.ExtraButtonText, UIBarButtonItemStyle.Done, (o, a) =>
                {
                    m_internalDatePicker.OnExtraButtonClicked?.Invoke();
                    Control.ResignFirstResponder();
                })
                {
                    AccessibilityIdentifier = ExtraButtonIdentifier
                };

                var doneButton = previousItems.FirstOrDefault(item => item.Style == UIBarButtonItemStyle.Done);
                if(doneButton != null)
                {
                    doneButton.Clicked += OnDone;
                }

                var newItems = new List<UIBarButtonItem>();
                newItems.Add(extraButton);
                newItems.AddRange(previousItems);

                toolbar.SetItems(newItems.ToArray(), false);
            }
        }

        private void OnDone(object sender, EventArgs e)
        {
            m_internalDatePicker.OniOSDoneClicked?.Invoke(m_uiDatePicker.Date.ToDateTime().Date);
        }

        private void SetExtraButtonTitle()
        {
            var extraButton = m_toolBar.Items.SingleOrDefault(item => item.AccessibilityIdentifier.Equals(ExtraButtonIdentifier));
            if (extraButton != null)
            {
                extraButton.Title = m_internalDatePicker.ExtraButtonText;
            }
            else
            {
                AddExtraUIBarButtonItemToToolbar();
            }
        }
    }
}