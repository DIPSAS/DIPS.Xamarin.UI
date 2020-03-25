using System;
using System.Windows.Input;
using DIPS.Xamarin.UI.Internal.Xaml;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.FloatingActionMenu
{
    /// <summary>
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuButton : ContentView
    {
        /// <summary>
        ///     <see cref="Title" />
        /// </summary>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(MenuButton),
            string.Empty);

        /// <summary>
        ///     <see cref="Command" />
        /// </summary>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MenuButton));

        /// <summary>
        ///     <see cref="CommandParameter" />
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(MenuButton));

        /// <summary>
        ///     <see cref="BackgroundColor" />
        /// </summary>
        public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(
            nameof(BackgroundColor),
            typeof(Color),
            typeof(MenuButton),
            Color.White);

        /// <summary>
        ///     <see cref="TitleTextColor" />
        /// </summary>
        public static readonly BindableProperty TitleTextColorProperty = BindableProperty.Create(
            nameof(TitleTextColor),
            typeof(Color),
            typeof(MenuButton),
            Color.Black);

        /// <summary>
        ///     <see cref="TextColor" />
        /// </summary>
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            nameof(TextColor),
            typeof(Color),
            typeof(MenuButton),
            Color.Black);

        /// <summary>
        ///     <see cref="Text" />
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(MenuButton),
            string.Empty);

        /// <summary>
        ///     <see cref="FontFamily" />
        /// </summary>
        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MenuButton));

        /// <summary>
        ///     <see cref="IsEnabled" />
        /// </summary>
        public new static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(
            nameof(IsEnabled),
            typeof(bool),
            typeof(MenuButton),
            true);

        /// <summary>
        ///     <see cref="TitleFontSize" />
        /// </summary>
        public static readonly BindableProperty TitleFontSizeProperty = BindableProperty.Create(
            nameof(TitleFontSize),
            typeof(double),
            typeof(MenuButton),
            12.0);

        /// <summary>
        ///     <see cref="FontSize" />
        /// </summary>
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
            nameof(FontSize),
            typeof(double),
            typeof(MenuButton),
            12.0);

        /// <summary>
        ///     <see cref="TitleFontFamily" />
        /// </summary>
        public static readonly BindableProperty TitleFontFamilyProperty = BindableProperty.Create(
            nameof(TitleFontFamily),
            typeof(string),
            typeof(MenuButton));

        public static readonly BindableProperty AttachBadgeProperty = BindableProperty.Create(nameof(AttachBadge), typeof(bool), typeof(MenuButton), propertyChanged:PropertyChanged);

        private static void PropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is MenuButton menuButton)
            {
                if (menuButton.Badge == null) return;
                if (!(bool)newvalue)
                {
                    menuButton.Badge.IsVisible = false;
                }
                else
                {
                    menuButton.Badge.IsVisible = true;
                }
            }
        }

        public static readonly BindableProperty BadgeCounterProperty = BindableProperty.Create(nameof(BadgeCounter), typeof(int), typeof(MenuButton));

        public static readonly BindableProperty BadgeColorProperty = BindableProperty.Create(nameof(BadgeColor), typeof(Color), typeof(MenuButton));

        internal Badge Badge { get; } = new Badge();

        /// <summary>
        ///     Buttons that can be placed in a <see cref="FloatingActionMenuBehaviour" />.
        /// </summary>
        public MenuButton()
        {
            InitializeComponent();
        }

        public Color BadgeColor
        {
            get => (Color)GetValue(BadgeColorProperty);
            set => SetValue(BadgeColorProperty, value);
        }

        public int BadgeCounter
        {
            get => (int)GetValue(BadgeCounterProperty);
            set => SetValue(BadgeCounterProperty, value);
        }

        public bool AttachBadge
        {
            get => (bool)GetValue(AttachBadgeProperty);
            set => SetValue(AttachBadgeProperty, value);
        }

        /// <summary>
        ///     Font family of the title.
        ///     This is a bindable property.
        /// </summary>
        public string TitleFontFamily
        {
            get => (string)GetValue(TitleFontFamilyProperty);
            set => SetValue(TitleFontFamilyProperty, value);
        }

        /// <summary>
        ///     The font size of the text in the button.
        ///     This is a bindable property.
        /// </summary>
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        /// <summary>
        ///     The font size of the title.
        ///     This is a bindable property.
        /// </summary>
        public double TitleFontSize
        {
            get => (double)GetValue(TitleFontSizeProperty);
            set => SetValue(TitleFontSizeProperty, value);
        }

        internal Internal.Xaml.FloatingActionMenu? FloatingActionMenuParent { get; set; }

        /// <summary>
        ///     Disables the button command.
        ///     This is a bindable property.
        /// </summary>
        public new bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        /// <summary>
        ///     The font family of the text in the button and the title.
        ///     This is a bindable property.
        /// </summary>
        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        /// <summary>
        ///     The text in the button.
        ///     This is a bindable property.
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        ///     The color of the text in the button.
        ///     This is a bindable property.
        /// </summary>
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        /// <summary>
        ///     The color of the text in the title.
        ///     This is a bindable property.
        /// </summary>
        public Color TitleTextColor
        {
            get => (Color)GetValue(TitleTextColorProperty);
            set => SetValue(TitleTextColorProperty, value);
        }

        /// <summary>
        ///     The background color of the button.
        ///     This is a bindable property.
        /// </summary>
        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        /// <summary>
        ///     The parameter sent with the <see cref="Command" />.
        ///     This is a bindable property.
        /// </summary>
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        ///     The command that is executed when the button is tapped.
        ///     This is a bindable property.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        ///     The text of the title next to the button.
        ///     This is a bindable property.
        /// </summary>
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        private void MenuButton_OnClicked(object sender, EventArgs e)
        {
            if (FloatingActionMenuParent != null && !IsEnabled) FloatingActionMenuParent.m_behaviour.IsOpen = false;

            if (IsEnabled) Command?.Execute(CommandParameter);
        }
    }
}