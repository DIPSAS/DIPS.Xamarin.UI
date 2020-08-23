using System;
using System.Globalization;
using System.Windows.Input;
using DIPS.Xamarin.UI.Converters.ValueConverters;
using DIPS.Xamarin.UI.Internal.Utilities;
using Xamarin.Forms;
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
        /// <inheritdoc />
        public DatePicker()
        {
            InitializeComponent();

            FormsDatePicker.OnExtraButtonClicked = OnExtraButtonClicked;
            FormsDatePicker.OniOSDoneClicked = OniOSDone;
            FormsDatePicker.Date = Date;
        }

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
        /// <see cref="ExtraButtonCommandParameter"/>
        /// </summary>
        public static readonly BindableProperty ExtraButtonCommandParameterProperty = BindableProperty.Create(nameof(ExtraButtonCommandParameter), typeof(object), typeof(DatePicker));

        /// <summary>
        /// <see cref="ExtraButtonCommand"/>
        /// </summary>
        public static readonly BindableProperty ExtraButtonCommandProperty = BindableProperty.Create(nameof(ExtraButtonCommand), typeof(ICommand), typeof(DatePicker));

        /// <summary>
        /// <see cref="ExtraButtonText"/>
        /// </summary>
        public static readonly BindableProperty ExtraButtonTextProperty = BindableProperty.Create(nameof(ExtraButtonText), typeof(string), typeof(DatePicker));

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
        /// Invoked when the date picker is closed.
        /// </summary>
        public event EventHandler Closed;

        /// <summary>
        /// Invoked on date changes
        /// </summary>
        public event EventHandler<DateChangedEventArgs>? DateSelected;

        /// <summary>
        /// Invoked when the extra button that is placed to the left in the date picker is clicked.
        /// </summary>
        public event EventHandler ExtraButtonClicked;

        /// <summary>
        /// Invoked when the date picker is opened.
        /// </summary>
        public event EventHandler Opened;

        /// <summary>
        /// The date that the user picks from the date picker.
        /// This is a bindable property
        /// </summary>
        public DateTime Date
        {
            get => (DateTime)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }

        /// <summary>
        /// The command that executes when the user taps the extra button to the left in the date picker.
        /// This is a bindable property.
        /// </summary>
        public ICommand ExtraButtonCommand
        {
            get => (ICommand)GetValue(ExtraButtonCommandProperty);
            set => SetValue(ExtraButtonCommandProperty, value);
        }

        /// <summary>
        /// The command paramter to be passed to the <see cref="ExtraButtonCommand"/>.
        /// This is a bindable property.
        /// </summary>
        public object ExtraButtonCommandParameter
        {
            get => GetValue(ExtraButtonCommandParameterProperty);
            set => SetValue(ExtraButtonCommandParameterProperty, value);
        }

        /// <summary>
        /// The text of the extra button that you can add to the left in the date picker.
        /// This is a bindable property.
        /// </summary>
        public string ExtraButtonText
        {
            get => (string)GetValue(ExtraButtonTextProperty);
            set => SetValue(ExtraButtonTextProperty, value);
        }

        /// <summary>
        /// The format to use when displaying the date label, <see cref="DateConverter.DateConverterFormat"/>
        /// </summary>
        public DateConverter.DateConverterFormat Format { get; set; }

        /// <summary>
        /// The color of the label that the user can click to open the date picker.
        /// This is a bindable property
        /// </summary>
        public Color LabelColor
        {
            get => (Color)GetValue(LabelColorProperty);
            set => SetValue(LabelColorProperty, value);
        }

        /// <summary>
        /// The label size of the label that the user can click to open the date picker.
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
        /// The maximum date to set to the date picker.
        /// This is a bindable property
        /// </summary>
        public DateTime MaximumDate
        {
            get => (DateTime)GetValue(MaximumDateProperty);
            set => SetValue(MaximumDateProperty, value);
        }

        /// <summary>
        /// The minimum date to set to the date picker.
        /// This is a bindable property
        /// </summary>
        public DateTime MinimumDate
        {
            get => (DateTime)GetValue(MinimumDateProperty);
            set => SetValue(MinimumDateProperty, value);
        }

        /// <summary>
        /// A enum to pick between the different strategies for when the date picker should update its date value.
        /// </summary>
#pragma warning disable IDE1006 // Naming Styles
        public iOSDateChangeStrategy DateChangedStrategyiOS { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// Opens the date picker.
        /// </summary>
        public void Open()
        {
            FormsDatePicker.Focus();
        }

        /// <summary>
        /// Closes the date picker.
        /// </summary>
        public void Close()
        {
            FormsDatePicker.Unfocus();
        }

        /// <summary>
        /// <see cref="Open"/>
        /// </summary>
        public new void Focus()
        {
            Open();
        }

        /// <summary>
        /// <see cref="Close"/>
        /// </summary>
        public new void Unfocus()
        {
            Close();
        }

        internal void OnExtraButtonClicked()
        {
            Close();
            ExtraButtonCommand?.Execute(ExtraButtonCommandParameter);
            ExtraButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        internal void OniOSDone(DateTime date)
        {
            if(DateChangedStrategyiOS == iOSDateChangeStrategy.WhenDone)
            {
                Date = date;
            }
        }

        private static void OnDateChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is DatePicker datePicker) || !(newvalue is DateTime newDate) || !(oldvalue is DateTime oldDate))
            {
                return;
            }

            datePicker.FormsDatePicker.Date = newDate;
            var formattedObject = new DateConverter() { Format = datePicker.Format }.Convert(datePicker.Date, null, null, CultureInfo.CurrentCulture);
            if (!(formattedObject is string formattedDate))
            {
                return;
            }
            datePicker.InvokeDateSelected(oldDate, newDate);
            datePicker.DateLabel.Text = formattedDate;
        }

        private void InvokeDateSelected(DateTime oldDate, DateTime newDate)
        {
            DateSelected?.Invoke(this, new DateChangedEventArgs(oldDate, newDate));
        }

        private static void OnLabelSizePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is DatePicker datepicker))
            {
                return;
            }

            datepicker.DateLabel.FontSize = (double)newvalue;
        }
        private void FormsDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if(Device.RuntimePlatform == Device.iOS)
            {
                if(DateChangedStrategyiOS == iOSDateChangeStrategy.WhenDone)
                {
                    return;
                }
            }

            Date = e.NewDate;
        }

        private void FormsDatePicker_Focused(object sender, FocusEventArgs e)
        {
            Opened?.Invoke(this, EventArgs.Empty);
        }

        private void FormsDatePicker_Unfocused(object sender, FocusEventArgs e)
        {
            Closed?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// <see cref="DatePicker.DateChangedStrategyiOS"/>
    /// </summary>
#pragma warning disable IDE1006 // Naming Styles
    public enum iOSDateChangeStrategy
#pragma warning restore IDE1006 // Naming Styles
    {
        /// <summary>
        /// The date will change when the user spins to a date in the ios date picker spinner
        /// </summary>
        WhenValueChanged = 0,
        /// <summary>
        /// The date will change when the user taps done in the date picker spinner toolbar
        /// </summary>
        WhenDone = 1
    }
}