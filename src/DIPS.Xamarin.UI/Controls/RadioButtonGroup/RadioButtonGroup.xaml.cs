using System;
using System.Collections;
using System.Collections.Generic;
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
        public RadioButtonGroup()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty OptionsProperty = BindableProperty.Create(
            nameof(Options),
            typeof(ObservableCollection<Option>),
            typeof(RadioButtonGroup),
            defaultBindingMode: BindingMode.TwoWay, propertyChanged:OnOptionsPropertyChanged);

        private static void OnOptionsPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if(!(bindable is RadioButtonGroup radioButtonGroup)) return;
            if(!(newvalue is ObservableCollection<Option> options))return;

            options.CollectionChanged += (o, e) => radioButtonGroup.Initialize(options);
            radioButtonGroup.Initialize(options);
        }

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

            radioButtonGroup.m_radioButtons.ForEach(rb =>
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
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: OnDeSelectedColorPropertyChanged);

        private static void OnDeSelectedColorPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is RadioButtonGroup radioButtonGroup))
                return;
            if (!(newvalue is Color newColor))
                return;

            radioButtonGroup.m_radioButtons.ForEach(rb =>
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
            defaultBindingMode: BindingMode.OneWay);

        private IList<RadioButton> m_radioButtons = new List<RadioButton>();

        public Color DeSelectedColor
        {
            get => (Color)GetValue(DeSelectedColorProperty);
            set => SetValue(DeSelectedColorProperty, value);
        }

        public ObservableCollection<Option> Options
        {
            get => (ObservableCollection<Option>)GetValue(OptionsProperty);
            set => SetValue(OptionsProperty, value);
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

        private void Initialize(ObservableCollection<Option> options)
        {
            var row = 0;
            if (options == null) return;
            m_radioButtons.Clear();

            foreach (var option in options)
            {
                var radioButton = new RadioButton() { Text = option.Name, Identifier = option.Identifier };
                //Initialize radio button
                radioButton.Initialize(this);

                //Set colors for each radiobutton
                radioButton.SelectedColor = SelectedColor;
                radioButton.DeSelectedColor = DeSelectedColor;

                //Set margin for each button
                radioButton.Margin = new Thickness(0, 15, 0, 15);

                //Add separator before the first element
                if (options.First() == option)
                {
                    AddSeparator(row);
                    row++;
                }

                //Add each radiobutton to the FlexLayout
                radioButtonContainer.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                Grid.SetRow(radioButton, row);
                row++;
                radioButtonContainer.Children.Add(radioButton);
                m_radioButtons.Add(radioButton);

                //Add a separator after each radio button
                AddSeparator(row);
                row++;
            }
        }

        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(Option), typeof(RadioButtonGroup), null, BindingMode.TwoWay, propertyChanged:OnIsSelectedPropertyChanged);

        private static void OnIsSelectedPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is RadioButtonGroup radioButtonGroup)) return;
            if (!(newvalue is Option selectedOption)) return;

            var selectedRadioButton = radioButtonGroup.m_radioButtons.SingleOrDefault(radioButton => radioButton.Identifier == selectedOption.Identifier);

#pragma warning disable 4014
            selectedRadioButton?.Animate(false);
#pragma warning restore 4014
        }

        public Option IsSelected
        {
            get => (Option)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }
        private void AddSeparator(int row)
        {
            var boxView = new BoxView() { HeightRequest = 1, BackgroundColor = SeparatorColor };
            radioButtonContainer.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            Grid.SetRow(boxView, row);
            radioButtonContainer.Children.Add(boxView);
        }

#pragma warning disable 4014
        void IHandleRadioButtons.OnRadioButtonTapped(RadioButton tappedRadioButton)
        {
            if (tappedRadioButton.IsSelected) return;
            var selectedOption = Options.FirstOrDefault(o => o.Identifier == tappedRadioButton.Identifier);
            if (selectedOption == null)
                return;
            IsSelected = selectedOption;


            foreach (var radioButton in m_radioButtons)
            {
                if (radioButton == tappedRadioButton) continue;
                radioButton.Animate(true);
            }
        }
#pragma warning restore 4014
    }
}