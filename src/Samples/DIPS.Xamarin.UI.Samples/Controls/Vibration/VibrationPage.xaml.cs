using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Samples.Controls.Vibration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VibrationPage : ContentPage
    {
        private int duration = 200;

        public VibrationPage()
        {
            InitializeComponent();
            TimeSpanLabel.Text = "200";
            
        }

        private void Click(object sender, EventArgs e)
        {
            UI.Vibration.Vibration.Click();
        }

        private void Slider_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            duration = (int) e.NewValue;
            TimeSpanLabel.Text = e.NewValue.ToString(CultureInfo.InvariantCulture);
        }

        private void HeavyClick(object sender, EventArgs e)
        {
            UI.Vibration.Vibration.HeavyClick();
        }

        private void Vibrate(object sender, EventArgs e)
        {
            UI.Vibration.Vibration.Vibrate(duration);
        }

        private void Error(object sender, EventArgs e)
        {
            UI.Vibration.Vibration.Error();
        }

        private void Success(object sender, EventArgs e)
        {
            UI.Vibration.Vibration.Success();
        }

        private void DoubleClick(object sender, EventArgs e)
        {
            UI.Vibration.Vibration.DoubleClick();
        }
    }
}