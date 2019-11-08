using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.RadioButtonGroup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RadioButton : ContentView
    {

        public RadioButton()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(RadioButton));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(RadioButton), Color.LightGray);

        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        public static readonly BindableProperty DeSelectedColorProperty = BindableProperty.Create(nameof(DeSelectedColor), typeof(Color), typeof(RadioButton), Color.LightGray);

        public Color DeSelectedColor
        {
            get => (Color)GetValue(DeSelectedColorProperty);
            set => SetValue(DeSelectedColorProperty, value);
        }

        internal bool IsSelected { get; set; }

        public static readonly BindableProperty SelectedCommandProperty = BindableProperty.Create(nameof(SelectedCommand), typeof(Command), typeof(RadioButton), defaultBindingMode:BindingMode.TwoWay);

        public Command SelectedCommand
        {
            get => (Command)GetValue(SelectedCommandProperty);
            set => SetValue(SelectedCommandProperty, value);
        }

        public static readonly BindableProperty SelectedCommandParameterProperty = BindableProperty.Create(nameof(SelectedCommandParameter), typeof(object), typeof(RadioButton), defaultBindingMode:BindingMode.TwoWay);

        public object SelectedCommandParameter
        {
            get => (object)GetValue(SelectedCommandParameterProperty);
            set => SetValue(SelectedCommandParameterProperty, value);
        }


        public static readonly BindableProperty IsSelectedInitiallyProperty = BindableProperty.Create(nameof(IsSelectedInitially), typeof(bool), typeof(RadioButton), false);

        public bool IsSelectedInitially
        {
            get => (bool)GetValue(IsSelectedInitiallyProperty);
            set => SetValue(IsSelectedInitiallyProperty, value);
        }


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

        public event EventHandler Tapped;

        private async void OnTapped(object sender, EventArgs e)
        {
            if (IsSelected) return;

            await Animate(false);
            IsSelected = true;

            if (IsSelected)
            {
                SelectedCommand?.Execute(SelectedCommandParameter);
            }

            Tapped.Invoke(this, e);
        }
    }
}