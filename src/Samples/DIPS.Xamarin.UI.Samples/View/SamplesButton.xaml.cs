using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.View
{
    public partial class SamplesButton : ContentView
    {
        public SamplesButton()
        {
            InitializeComponent();
            BindingContextChanged += SamplesButton_BindingContextChanged;
        }

        private void SamplesButton_BindingContextChanged(object sender, EventArgs e)
        {
            if (BindingContext == null) return;
            btn.Text = Text;
            btn.CommandParameter = CommandParameter;
            img.Source = Image;
        }


        public string Text { get; set; } = string.Empty;
        public ImageSource Image { get; set; } = string.Empty;
        public object? CommandParameter { get; set; }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(Command),
            typeof(SamplesButton));

        public Command Command
        {
            get => (Command)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
    }
}
