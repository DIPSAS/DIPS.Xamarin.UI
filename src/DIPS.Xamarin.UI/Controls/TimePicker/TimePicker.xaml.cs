using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.TimePicker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimePicker : ContentView
    {
        public TimePicker()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty TimeProperty = BindableProperty.Create(nameof(Time), typeof(TimeSpan), typeof(TimePicker), global::Xamarin.Forms.TimePicker.TimeProperty.DefaultValue, BindingMode.TwoWay);

        public TimeSpan Time
        {
            get => (TimeSpan)GetValue(TimeProperty);
            set => SetValue(TimeProperty, value);
        }

        public static readonly BindableProperty LabelColorProperty = BindableProperty.Create(nameof(LabelColor), typeof(Color), typeof(TimePicker), Color.Black);

        public Color LabelColor
        {
            get => (Color)GetValue(LabelColorProperty);
            set => SetValue(LabelColorProperty, value);
        }

        public static readonly BindableProperty LabelSizeProperty = BindableProperty.Create(nameof(LabelSize), typeof(double), typeof(TimePicker), defaultValueCreator:DefaultLabelSizeCreator);
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