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

        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(RadioButton), propertyChanged:OnSelectedChanged);

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

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

        private static void OnSelectedChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is RadioButton radioButton) radioButton.OnSelected((bool)oldvalue, (bool)newvalue);
        }


        private async void OnSelected(bool oldvalue, bool newvalue)
        {
            if (!oldvalue)
            {
                await innerFrame.ScaleTo(0.5);
                innerFrame.BackgroundColor = SelectedColor;
                outerFrame.BorderColor = SelectedColor;
            }
            else
            {
                await innerFrame.ScaleTo(0);
                innerFrame.BackgroundColor = DeSelectedColor;
                outerFrame.BorderColor = DeSelectedColor;
            }
        }

        public event EventHandler Tapped;

        private void OnTapped(object sender, EventArgs e)
        {
            IsSelected = !IsSelected;

            if (IsSelected)
            {
                SelectedCommand?.Execute(SelectedCommandParameter);
            }

            Tapped.Invoke(this, e);
        }
    }
}