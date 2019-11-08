using System;
using System.Collections.ObjectModel;
using System.Linq;
using DIPS.Xamarin.UI.Controls.RadioButtonGroup.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.RadioButtonGroup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RadioButtonGroup : ContentView, IHandleRadioButtons
    {

        public static readonly BindableProperty RadioButtonsProperty = BindableProperty.Create(
            nameof(RadioButtons),
            typeof(ObservableCollection<RadioButton>),
            typeof(RadioButtonGroup),
            defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(
            nameof(SelectedColor),
            typeof(Color),
            typeof(RadioButtonGroup),
            null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: OnSelectedColorPropertyChanged);

        private static void OnSelectedColorPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is RadioButtonGroup radioButtonGroup)) return;
            if (!(newvalue is Color newColor)) return;

            radioButtonGroup.RadioButtons.ForEach(rb =>
            {
                rb.SelectedColor = newColor;
                rb.RefreshColors();
            });
        }

        public static readonly BindableProperty DeSelectedColorProperty = BindableProperty.Create(
            nameof(DeSelectedColor),
            typeof(Color),
            typeof(RadioButtonGroup),
            null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: OnDeSelectedColorPropertyChanged);

        private static void OnDeSelectedColorPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is RadioButtonGroup radioButtonGroup))
                return;
            if (!(newvalue is Color newColor))
                return;

            radioButtonGroup.RadioButtons.ForEach(rb =>
            {
                rb.DeSelectedColor = newColor;
                rb.RefreshColors();
            });
        }

        public static readonly BindableProperty SeparatorColorProperty = BindableProperty.Create(
            nameof(SeparatorColor),
            typeof(Color),
            typeof(RadioButtonGroup),
            Color.LightGray,
            defaultBindingMode: BindingMode.TwoWay);


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
            var row = 0;
            foreach (var radioButton in RadioButtons)
            {
                //Initialize radio button
                radioButton.Initialize(this);

                //Set colors for each radiobutton
                radioButton.SelectedColor = SelectedColor;
                radioButton.DeSelectedColor = DeSelectedColor;

                //Set margin for each button
                radioButton.Margin = new Thickness(0, 15, 0, 15);

                //Add separator before the first element
                if (RadioButtons.First() == radioButton)
                {
                    AddSeparator(row);
                    row++;

                }

                //Add each radiobutton to the FlexLayout
                radioButtonContainer.RowDefinitions.Add(new RowDefinition(){Height = GridLength.Auto});
                Grid.SetRow(radioButton, row);
                row++;
                radioButtonContainer.Children.Add(radioButton);

                //Add a separator after each radio button
                AddSeparator(row);
                row++;
            }

            SetInitialRadioButton();
        }

        private void SetInitialRadioButton()
        {
            var firstInitiallySelected = RadioButtons.FirstOrDefault(rb => rb.IsSelectedInitially);
            if (firstInitiallySelected == null) return;

            firstInitiallySelected.IsSelected = true;
#pragma warning disable 4014
            firstInitiallySelected.Animate(false);
#pragma warning restore 4014
        }

        private void AddSeparator(int row)
        {
            var boxView = new BoxView() { HeightRequest = 1, BackgroundColor = SeparatorColor };
            radioButtonContainer.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            Grid.SetRow(boxView, row);
            radioButtonContainer.Children.Add(boxView);
        }

        async void IHandleRadioButtons.OnRadioButtonTapped(RadioButton tappedRadioButton)
        {
            foreach (var radioButton in RadioButtons)
            {
                if (radioButton.Text.Equals(tappedRadioButton.Text)) continue;
                if (!radioButton.IsSelected) continue;

                radioButton.IsSelected = false;
                await radioButton.Animate(true);
            }
        }
    }
}