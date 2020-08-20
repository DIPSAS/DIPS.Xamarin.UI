using System.Windows.Input;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Internal
{
    internal class DatePickerWithExtraButton : DatePicker
    {
        public static readonly BindableProperty ExtraButtonCommandProperty = BindableProperty.Create(nameof(ExtraButtonCommand), typeof(ICommand), typeof(DatePickerWithExtraButton));

        public ICommand ExtraButtonCommand
        {
            get => (ICommand)GetValue(ExtraButtonCommandProperty);
            set => SetValue(ExtraButtonCommandProperty, value);
        }



        public object ExtraButtonCommandParameter
        {
            get => (object)GetValue(ExtraButtonCommandParameterProperty);
            set => SetValue(ExtraButtonCommandParameterProperty, value);
        }

        public static readonly BindableProperty ExtraButtonCommandParameterProperty = BindableProperty.Create(nameof(ExtraButtonCommandParameter), typeof(object), typeof(DatePickerWithExtraButton));



        public string ExtraButtonText
        {
            get => (string)GetValue(ExtraButtonTextProperty);
            set => SetValue(ExtraButtonTextProperty, value);
        }

        public static readonly BindableProperty ExtraButtonTextProperty = BindableProperty.Create(nameof(ExtraButtonText), typeof(string), typeof(DatePickerWithExtraButton));
    }
}
