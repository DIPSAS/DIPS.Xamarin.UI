using System;
using System.Globalization;
using DIPS.Xamarin.UI.Converters.ValueConverters;
using DIPS.Xamarin.UI.Internal.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.DatePicker
{
    /// <summary>
    ///     An wrapper of the Xamarin.Forms.DatePicker.
    ///     This DatePicker is border less and will let you customize the label that the user clicks to open the date picker.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatePicker : ContentView
    {
        /// <summary>
        /// Bindable property for <see cref="Date"/>
        /// </summary>
        public static readonly BindableProperty DateProperty = BindableProperty.Create(
            nameof(Date),
            typeof(DateTime),
            typeof(DatePicker),
            global::Xamarin.Forms.DatePicker.DateProperty.DefaultValue,
            BindingMode.TwoWay,
            propertyChanged: OnDateChanged);

        /// <summary>
        /// Bindable property for <see cref="MaximumDate"/>
        /// </summary>
        public static readonly BindableProperty MaximumDateProperty = BindableProperty.Create(
            nameof(MaximumDate),
            typeof(DateTime),
            typeof(DatePicker),
            global::Xamarin.Forms.DatePicker.MaximumDateProperty.DefaultValue);

        /// <summary>
        /// Bindable property for <see cref="MinimumDate"/>
        /// </summary>
        public static readonly BindableProperty MinimumDateProperty = BindableProperty.Create(
            nameof(MinimumDate),
            typeof(DateTime),
            typeof(DatePicker),
            global::Xamarin.Forms.DatePicker.MinimumDateProperty.DefaultValue);

        /// <summary>
        /// Bindable property for <see cref="LabelColor"/>
        /// </summary>
        public static readonly BindableProperty LabelColorProperty = BindableProperty.Create(
            nameof(LabelColor),
            typeof(Color),
            typeof(DatePicker),
            Color.Black);

        /// <summary>
        /// Bindable property for <see cref="LabelSize"/>
        /// </summary>
        public static readonly BindableProperty LabelSizeProperty = BindableProperty.Create(
            nameof(LabelSize),
            typeof(double),
            typeof(DatePicker),
            propertyChanged: OnLabelSizePropertyChanged);

        private static void OnLabelSizePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is DatePicker datepicker)) return;
            datepicker.DateLabel.FontSize = (double)newvalue;
        }

        /// <inheritdoc />
        public DatePicker()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The date that the user picks from the date picker
        /// This is a bindable property
        /// </summary>
        public DateTime Date
        {
            get => (DateTime)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }

        /// <summary>
        /// The format to use when displaying the date label, <see cref="DateConverter.DateConverterFormat"/>
        /// </summary>
        public DateConverter.DateConverterFormat Format { get; set; }

        /// <summary>
        /// The color of the label that the user can click to open the date picker
        /// This is a bindable property
        /// </summary>
        public Color LabelColor
        {
            get => (Color)GetValue(LabelColorProperty);
            set => SetValue(LabelColorProperty, value);
        }

        /// <summary>
        /// The label size of the label that the user can click to open the date picker
        /// This is a bindable property
        /// </summary>
        /// <remarks>This support named font sizes <see href="https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/text/fonts#named-font-sizes"/></remarks>
        [TypeConverter(typeof(LabelFontSizeTypeConverter))]
        public double LabelSize
        {
            get => (double)GetValue(LabelSizeProperty);
            set => SetValue(LabelSizeProperty, value);
        }

        /// <summary>
        /// The maximum date to set to the date picker
        /// This is a bindable property
        /// </summary>
        public DateTime MaximumDate
        {
            get => (DateTime)GetValue(MaximumDateProperty);
            set => SetValue(MaximumDateProperty, value);
        }

        /// <summary>
        /// The minimum date to set to the date picker
        /// This is a bindable property
        /// </summary>
        public DateTime MinimumDate
        {
            get => (DateTime)GetValue(MinimumDateProperty);
            set => SetValue(MinimumDateProperty, value);
        }

        /// <summary>
        /// Opens the date picker
        /// </summary>
        public void Open() => FormsDatePicker.Focus();

        private static void OnDateChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is DatePicker datePicker)) return;
            var formattedObject = new DateConverter() { Format = datePicker.Format }.Convert(datePicker.Date, null, null, CultureInfo.CurrentCulture);
            if (!(formattedObject is string formattedDate)) return;
            datePicker.DateLabel.Text = formattedDate;
        }
    }
}