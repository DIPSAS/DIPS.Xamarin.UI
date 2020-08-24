using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Internal
{
    internal class InternalDatePicker : DatePicker
    {
        public string ExtraButtonText
        {
            get => (string)GetValue(ExtraButtonTextProperty);
            set => SetValue(ExtraButtonTextProperty, value);
        }

        public static readonly BindableProperty ExtraButtonTextProperty = BindableProperty.Create(nameof(ExtraButtonText), typeof(string), typeof(InternalDatePicker));

        internal Action? OnExtraButtonClicked { get; set; }

        internal Action<DateTime>? OniOSDoneClicked { get; set; }
    }
}
