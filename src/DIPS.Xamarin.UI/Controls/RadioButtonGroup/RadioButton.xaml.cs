using System;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Controls.RadioButtonGroup.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.RadioButtonGroup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RadioButton : ContentView
    {
        private IHandleRadioButtons? m_radioButtonsHandler;

        public RadioButton()
        {
            InitializeComponent();
        }

        internal void Initialize(IHandleRadioButtons radioButtonsHandler)
        {
            m_radioButtonsHandler = radioButtonsHandler;
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(RadioButton), defaultBindingMode: BindingMode.TwoWay);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(
            nameof(SelectedColor),
            typeof(Color),
            typeof(RadioButton),
            Color.LightGray,
            BindingMode.TwoWay);

        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        public static readonly BindableProperty DeSelectedColorProperty = BindableProperty.Create(
            nameof(DeSelectedColor),
            typeof(Color),
            typeof(RadioButton),
            Color.LightGray,
            BindingMode.TwoWay);

        public Color DeSelectedColor
        {
            get => (Color)GetValue(DeSelectedColorProperty);
            set => SetValue(DeSelectedColorProperty, value);
        }

        internal bool IsSelected { get; set; }


        private double m_highestHeight;

        internal object Identifier { get; set; }

        internal async Task Animate(bool wasSelected)
        {
            if (!wasSelected)
            {
                innerFrame.BackgroundColor = SelectedColor;
                outerFrame.BorderColor = SelectedColor;
                await innerFrame.ScaleTo(0.5);
            }
            else
            {
                innerFrame.BackgroundColor = DeSelectedColor;
                outerFrame.BorderColor = DeSelectedColor;
                await innerFrame.ScaleTo(0);
            }
        }

        internal void RefreshColors()
        {
            if (IsSelected)
            {
                innerFrame.BackgroundColor = SelectedColor;
                outerFrame.BorderColor = SelectedColor;
            }
            else
            {
                innerFrame.BackgroundColor = DeSelectedColor;
                outerFrame.BorderColor = DeSelectedColor;
            }
        }

        private void OnTapped(object sender, EventArgs e) => m_radioButtonsHandler?.OnRadioButtonTapped(this);
    }
}