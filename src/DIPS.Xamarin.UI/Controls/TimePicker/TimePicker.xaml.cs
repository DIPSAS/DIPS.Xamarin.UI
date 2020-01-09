using System;
using System.Globalization;
using DIPS.Xamarin.UI.Converters.ValueConverters;
using DIPS.Xamarin.UI.InternalUtils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.TimePicker
{
    /// <summary>
    /// An wrapper of the Xamarin.Forms.TimePicker.
    /// This TimePicker is border less and will let you customize the label that the user clicks to open the time picker.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimePicker : ContentView
    {
        /// <summary>
        /// BindableProperty for <see cref="Time"/>
        /// </summary>
        public static readonly BindableProperty TimeProperty = BindableProperty.Create(
            nameof(Time),
            typeof(TimeSpan),
            typeof(TimePicker),
            defaultBindingMode:BindingMode.TwoWay,
            propertyChanged: TimePropertyChanged,
            defaultValueCreator:DefaultTimeCreator);

        /// <summary>
        /// BindableProperty for <see cref="Label"/>
        /// </summary>
        public static readonly BindableProperty LabelColorProperty = BindableProperty.Create(
            nameof(LabelColor),
            typeof(Color),
            typeof(TimePicker),
            Color.Black);

        /// <summary>
        /// BindableProperty for <see cref="LabelSize"/>
        /// </summary>
        public static readonly BindableProperty LabelSizeProperty = BindableProperty.Create(
            nameof(LabelSize),
            typeof(double),
            typeof(TimePicker),
            propertyChanged:OnLabelSizePropertyChanged);

        private static void OnLabelSizePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is TimePicker timePicker)) return;
            timePicker.TimeLabel.FontSize = (double)newvalue;
        }

        /// <inheritdoc />
        public TimePicker()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The format to use when displaying the time label, <see cref="TimeConverter.TimeConverterFormat"/>
        /// </summary>
        public TimeConverter.TimeConverterFormat Format { get; set; }

        /// <summary>
        /// The color of the label that the user clicks to chose a time
        /// This is a bindable property
        /// </summary>
        public Color LabelColor
        {
            get => (Color)GetValue(LabelColorProperty);
            set => SetValue(LabelColorProperty, value);
        }

        /// <summary>
        /// The size of the label that the user clicks to chose a time
        /// This is a bindable property
        /// </summary>
        [TypeConverter(typeof(LabelFontSizeTypeConverter))]
        public double LabelSize
        {
            get => (double)GetValue(LabelSizeProperty);
            set => SetValue(LabelSizeProperty, value);
        }

        /// <summary>
        /// The time timespan that the user has chosen from the time picker
        /// This is a bindable property
        /// </summary>
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
            timePicker.TimeLabel.Text = formattedDate;
        }

        /// <summary>
        /// Opens the time picker
        /// </summary>
        public void Open()
        {
            FormsTimePicker.Focus();
        }

        /// <summary>
        /// Boolean value to indicate if the time picker is open
        /// </summary>
        public bool IsOpen => FormsTimePicker.IsFocused;
    }
}