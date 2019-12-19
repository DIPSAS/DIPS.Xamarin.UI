﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Converters.ValueConverters;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.DatePicker
{
    /// <summary>
    /// An border less datepicker
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatePicker : ContentView
    {
        public DatePicker()
        {
            InitializeComponent();
        }

        public DateConverter.DateConverterFormat Format { get; set; }

        public static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateTime), typeof(DatePicker), global::Xamarin.Forms.DatePicker.DateProperty.DefaultValue, BindingMode.TwoWay, propertyChanged:OnDateChanged);

        private static void OnDateChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is DatePicker datePicker)) return;
            var formattedObject = new DateConverter() { Format = datePicker.Format }.Convert(datePicker.Date, null, null, CultureInfo.CurrentCulture);
            if(!(formattedObject is string formattedDate)) return;
            datePicker.DateLabel.Text = formattedDate;
        }

        /// <summary>
        /// <seealso cref="Date"/>
        /// </summary>
        public DateTime Date
        {
            get => (DateTime)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }

        public static readonly BindableProperty MaximumDateProperty = BindableProperty.Create(nameof(MaximumDate), typeof(DateTime), typeof(DatePicker), global::Xamarin.Forms.DatePicker.MaximumDateProperty.DefaultValue);

        public DateTime MaximumDate
        {
            get => (DateTime)GetValue(MaximumDateProperty);
            set => SetValue(MaximumDateProperty, value);
        }

        public static readonly BindableProperty MinimumDateProperty = BindableProperty.Create(nameof(MinimumDate), typeof(DateTime), typeof(DatePicker), global::Xamarin.Forms.DatePicker.MinimumDateProperty.DefaultValue);

        public DateTime MinimumDate
        {
            get => (DateTime)GetValue(MinimumDateProperty);
            set => SetValue(MinimumDateProperty, value);
        }

        public static readonly BindableProperty LabelColorProperty = BindableProperty.Create(nameof(LabelColor), typeof(Color), typeof(DatePicker), Color.Black);

        public Color LabelColor
        {
            get => (Color)GetValue(LabelColorProperty);
            set => SetValue(LabelColorProperty, value);
        }

        public static readonly BindableProperty LabelSizeProperty = BindableProperty.Create(nameof(LabelSize), typeof(double), typeof(DatePicker), defaultValueCreator:DefaultLabelSizeCreator);

        private static object DefaultLabelSizeCreator(BindableObject bindable)
        {
            return Device.GetNamedSize(NamedSize.Body, typeof(Label));
        }

        public double LabelSize
        {
            get => (double)GetValue(LabelSizeProperty);
            set => SetValue(LabelSizeProperty, value);
        }
    }
}