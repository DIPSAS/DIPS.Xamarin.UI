using System;
using System.Globalization;
using DIPS.Xamarin.UI.Converters.ValueConverters;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.TimePicker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimePicker : ContentView
    {
        public static readonly BindableProperty TimeProperty = BindableProperty.Create(
            nameof(Time),
            typeof(TimeSpan),
            typeof(TimePicker),
            defaultBindingMode:BindingMode.TwoWay,
            propertyChanged: TimePropertyChanged,
            defaultValueCreator:DefaultTimeCreator);

        public static readonly BindableProperty LabelColorProperty = BindableProperty.Create(
            nameof(LabelColor),
            typeof(Color),
            typeof(TimePicker),
            Color.Black);

        public static readonly BindableProperty LabelSizeProperty = BindableProperty.Create(
            nameof(LabelSize),
            typeof(double),
            typeof(TimePicker),
            defaultValueCreator: DefaultLabelSizeCreator);

        public TimePicker()
        {
            InitializeComponent();
        }

        public TimeConverter.TimeConverterFormat Format { get; set; }

        public Color LabelColor
        {
            get => (Color)GetValue(LabelColorProperty);
            set => SetValue(LabelColorProperty, value);
        }

        public double LabelSize
        {
            get => (double)GetValue(LabelSizeProperty);
            set => SetValue(LabelSizeProperty, value);
        }

        public TimeSpan Time
        {
            get => (TimeSpan)GetValue(TimeProperty);
            set => SetValue(TimeProperty, value);
        }

        private static object DefaultTimeCreator(BindableObject bindable)
        {
            var now = DateTime.Now;
            return new TimeSpan(now.Hour, now.Minute, now.Second);
        }

        private static void TimePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is TimePicker timePicker))
                return;
            var formattedObject = new TimeConverter() { Format = timePicker.Format }.Convert(timePicker.Time, null, null, CultureInfo.CurrentCulture);
            if (!(formattedObject is string formattedDate))
                return;
            timePicker.DateLabel.Text = formattedDate;
        }

        private static object DefaultLabelSizeCreator(BindableObject bindable)
        {
            return Device.GetNamedSize(NamedSize.Body, typeof(Label));
        }
    }
}