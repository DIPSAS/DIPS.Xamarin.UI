using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.RadioButtonGroup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RadioButtonGroup : ContentView
    {
        public static readonly BindableProperty RunInitialCommandProperty = BindableProperty.Create(
            nameof(RunInitialCommand),
            typeof(bool),
            typeof(RadioButtonGroup),
            true);

        public static readonly BindableProperty RadioButtonsProperty = BindableProperty.Create(
            nameof(RadioButtons),
            typeof(ObservableCollection<RadioButton>),
            typeof(RadioButtonGroup));

        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(
            nameof(SelectedColor),
            typeof(Color),
            typeof(RadioButtonGroup),
            null);

        public static readonly BindableProperty DeSelectedColorProperty = BindableProperty.Create(
            nameof(DeSelectedColor),
            typeof(Color),
            typeof(RadioButtonGroup),
            null);

        public static readonly BindableProperty SeparatorColorProperty = BindableProperty.Create(
            nameof(SeparatorColor),
            typeof(Color),
            typeof(RadioButtonGroup),
            Color.LightGray);

        public RadioButtonGroup()
        {
            InitializeComponent();
            RadioButtons = new ObservableCollection<RadioButton>();
        }

        public Color DeSelectedColor
        {
            get => (Color)GetValue(DeSelectedColorProperty);
            set => SetValue(DeSelectedColorProperty, value);
        }

        public ObservableCollection<RadioButton> RadioButtons
        {
            get => (ObservableCollection<RadioButton>)GetValue(RadioButtonsProperty);
            set => SetValue(RadioButtonsProperty, value);
        }

        public bool RunInitialCommand
        {
            get => (bool)GetValue(RunInitialCommandProperty);
            set => SetValue(RunInitialCommandProperty, value);
        }

        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        public Color SeparatorColor
        {
            get => (Color)GetValue(SeparatorColorProperty);
            set => SetValue(SeparatorColorProperty, value);
        }

        protected override void InvalidateLayout()
        {
            base.InvalidateLayout();
        }

        protected override void InvalidateMeasure()
        {
            base.InvalidateMeasure();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            Initialize();
        }

        /// <summary>
        ///     When rendering
        /// </summary>
        private void Initialize()
        {
            foreach (var radioButton in RadioButtons)
            {
                //Set colors for each radiobutton
                radioButton.SelectedColor = SelectedColor;
                radioButton.DeSelectedColor = DeSelectedColor;

                //Set margin for each button
                radioButton.Margin = new Thickness(0, 15, 0, 15);

                //Add separator before the first element
                if (RadioButtons.First() == radioButton) AddSeparator();

                //Add each radiobutton to the FlexLayout
                radioButtonContainer.Children.Add(radioButton);

                //Add a separator after each radio button
                AddSeparator();

                radioButton.Tapped += OnRadioButtonTapped;
            }

            SetInitialRadioButton();
        }

        private void SetInitialRadioButton()
        {
            var firstInitiallySelected = RadioButtons.FirstOrDefault(rb => rb.IsSelectedInitially) ?? RadioButtons.First();

            firstInitiallySelected.IsSelected = true;
#pragma warning disable 4014
            firstInitiallySelected.Animate(false);
#pragma warning restore 4014
        }

        private void AddSeparator()
        {
            radioButtonContainer.Children.Add(new BoxView() { HeightRequest = 1, BackgroundColor = SeparatorColor });
        }

        private async void OnRadioButtonTapped(object sender, EventArgs e)
        {
            if (!(sender is RadioButton tappedRadioButton)) return;

            foreach (var radioButton in RadioButtons)
            {
                if (!radioButton.Text.Equals(tappedRadioButton.Text))
                {
                    if (radioButton.IsSelected)
                    {
                        radioButton.IsSelected = false;
                        await radioButton.Animate(true);
                    }
                }
            }
        }
    }
}