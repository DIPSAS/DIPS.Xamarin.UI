using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIPS.Xamarin.UI.Internal;
using DIPS.Xamarin.UI.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(DatePickerWithExtraButton), typeof(DatePickerWithExtraButtonRenderer))]
namespace DIPS.Xamarin.UI.iOS
{
    public class DatePickerWithExtraButtonRenderer : DatePickerRenderer
    {
        internal static void Initialize() { }

        private DatePickerWithExtraButton m_datepickerWithExtraButton;

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement is DatePickerWithExtraButton oldDatePicker && Control != null)
            {

            }

            if (e.NewElement is DatePickerWithExtraButton newDatePicker && Control != null)
            {
                m_datepickerWithExtraButton = newDatePicker;
                SetExtraButton();
            }
        }

        private void SetExtraButton()
        {
            if (Control.InputAccessoryView is UIToolbar toolbar)
            {
                var previousItems = toolbar.Items;
                var extraButton = new UIBarButtonItem("Today", UIBarButtonItemStyle.Done, (o, a) =>
                {
                    m_datepickerWithExtraButton.ExtraButtonCommand?.Execute(m_datepickerWithExtraButton.ExtraButtonCommandParameter);
                    Control.ResignFirstResponder();
                });

                var newItems = new List<UIBarButtonItem>();
                newItems.Add(extraButton);
                newItems.AddRange(previousItems);

                toolbar.SetItems(newItems.ToArray(), false);
            }
        }
    }
}