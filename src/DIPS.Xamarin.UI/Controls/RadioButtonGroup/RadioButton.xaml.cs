using System;
using DIPS.Xamarin.UI.Controls.RadioButtonGroup.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.RadioButtonGroup
{
    /// <summary>
    ///     Radio button is a checkbox that can be toggled by it, this component should contain inside of a a
    ///     <see cref="RadioButtonGroup" />
    /// </summary>
    /// <remarks>
    ///     This component should not be used stand alone
    ///     Please use <see cref="RadioButtonGroup" /> instead of creating a group and handling multiples yourself.
    /// </remarks>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RadioButton : ContentView
    {
        private IHandleRadioButtons? m_radioButtonsHandler;

        /// <summary>
        ///     <see cref="Text" />
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(RadioButton));

        /// <summary>
        ///     <see cref="SelectedColor" />
        /// </summary>
        public static readonly BindableProperty SelectedColorProperty = BindableProperty.Create(
            nameof(SelectedColor),
            typeof(Color),
            typeof(RadioButton),
            Color.LightGray,
            BindingMode.TwoWay);

        /// <summary>
        ///     <see cref="DeSelectedColor" />
        /// </summary>
        public static readonly BindableProperty DeSelectedColorProperty = BindableProperty.Create(
            nameof(DeSelectedColor),
            typeof(Color),
            typeof(RadioButton),
            Color.LightGray,
            BindingMode.TwoWay);

        /// <summary>
        ///     <see cref="IsSelected" />
        /// </summary>
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(
            nameof(IsSelected),
            typeof(bool),
            typeof(RadioButton),
            false);

        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(
            nameof(BorderWidth),
            typeof(int),
            typeof(RadioButton),
            2);

        /// <summary>
        ///     Constructs an radio button
        /// </summary>
        public RadioButton()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     The width of the outermost border of the radio button
        ///     This is a bindable property
        /// </summary>
        public int BorderWidth
        {
            get => (int)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }

        /// <summary>
        ///     The color to use on the radio button when the button is not selected
        ///     This is a bindable property
        /// </summary>
        public Color DeSelectedColor
        {
            get => (Color)GetValue(DeSelectedColorProperty);
            set => SetValue(DeSelectedColorProperty, value);
        }

        internal object Identifier { get; set; }

        /// <summary>
        ///     A value to indicate if the radio button should be checked or not
        ///     This is a bindable property
        /// </summary>
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set
            {
                Animate(IsSelected);
                SetValue(IsSelectedProperty, value);
            }
        }

        /// <summary>
        ///     The color to use on the button when the button is selected
        ///     This is a bindable property
        /// </summary>
        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        /// <summary>
        ///     The text of the label that is placed alongside the radio button symbol
        ///     This is a bindable property
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        internal void Initialize(IHandleRadioButtons radioButtonsHandler)
        {
            m_radioButtonsHandler = radioButtonsHandler;
        }

        private void Animate(bool wasSelected)
        {
            if (!wasSelected)
            {
                RefreshColor(true);
                innerButton.ScaleTo(0.5);  
            }
            else
            {
                RefreshColor(false);
                innerButton.ScaleTo(0);
            }
        }

        internal void RefreshColor(bool isSelected)
        {
            if (!isSelected)
            {
                innerButton.BackgroundColor = DeSelectedColor;
                outerButton.BorderColor = DeSelectedColor;
            }
            else
            {
                innerButton.BackgroundColor = SelectedColor;
                outerButton.BorderColor = SelectedColor;
            }
        }

        private void OnTapped(object sender, EventArgs e)
        {
            m_radioButtonsHandler?.OnRadioButtonTapped(this);
        }
    }
}