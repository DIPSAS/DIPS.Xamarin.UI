using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.RadioButtonGroup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RadioButtonGroup : ContentView
    {
        public static readonly BindableProperty RadioButtonsProperty = BindableProperty.Create(
            nameof(RadioButtons),
            typeof(ObservableCollection<RadioButton>),
            typeof(RadioButtonGroup),
            new ObservableCollection<RadioButton>());

        public RadioButtonGroup()
        {
            InitializeComponent();
            RadioButtons.CollectionChanged += RadioButtonsOnCollectionChanged;
        }

        private void RadioButtonsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Initialize();
        }

        private void Initialize()
        {
            foreach (var radioButton in RadioButtons)
            {
                radioButton.SelectedColor = SelectedColor;
                radioButton.DeSelectedColor = DeSelectedColor;

                radioButtonContainer.Children.Add(radioButton);
                radioButton.Tapped += OnRadioButtonTapped;
            }
        }

        public ObservableCollection<RadioButton> RadioButtons
        {
            get => (ObservableCollection<RadioButton>)GetValue(RadioButtonsProperty);
            set => SetValue(RadioButtonsProperty, value);
        }

        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(RadioButtonGroup), null);

        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        public static readonly BindableProperty DeSelectedColorProperty = BindableProperty.Create(nameof(DeSelectedColor), typeof(Color), typeof(RadioButtonGroup), null);

        public Color DeSelectedColor
        {
            get => (Color)GetValue(DeSelectedColorProperty);
            set => SetValue(DeSelectedColorProperty, value);
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
                        await radioButton.OnSelected(true);
                    }
                }
            }
        }
    }
}