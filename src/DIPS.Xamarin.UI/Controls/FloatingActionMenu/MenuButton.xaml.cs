using System;
using System.Windows.Input;
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

        /// <summary>
        ///     <see cref="IsBadgeVisible" />
        /// </summary>
        public static readonly BindableProperty IsBadgeVisibleProperty = BindableProperty.Create(
            nameof(IsBadgeVisible),
            typeof(bool),
            typeof(MenuButton),
            propertyChanged: IsBadgeVisiblePropertyChanged);

        private static async void IsBadgeVisiblePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!Library.PreviewFeatures.MenuButtonBadgeAnimation) return;
            
            if (bindable is MenuButton menuButton)
            {
                if ((bool) newvalue)
                {

                    if (menuButton.FloatingActionMenuParent != null )
                    {   
                        menuButton.BadgeFrame?.FadeTo(menuButton.FloatingActionMenuParent.m_behaviour.IsOpen ? .95 : .5, 100);

                        await menuButton.BadgeFrame?.TranslateTo(0, -20, 200, Easing.CubicIn);
                        menuButton.BadgeFrame?.TranslateTo(0, 0, 200, Easing.BounceOut);
                    }

                }
            }
        }

        /// <summary>
        ///     <see cref="BadgeCount" />
        /// </summary>
        public static readonly BindableProperty BadgeCountProperty = BindableProperty.Create(nameof(BadgeCount), typeof(string), typeof(MenuButton), propertyChanged: BadgeCountPropertyChanged, coerceValue: CoerceValue);

        private static object CoerceValue(BindableObject bindable, object value)
        {
            if (bindable is MenuButton menuButton && menuButton.BadgeFrame != null)
            {
                if (value is string count && int.TryParse(count, out var newCount))
                {
                    if (newCount > 99)
                    {
                        menuButton.BadgeFrame.HeightRequest = 25;
                        menuButton.BadgeFrame.WidthRequest = 25;
                        menuButton.BadgeFrame.CornerRadius = 12.5f;
                        return "99+";
                    }
                }
                menuButton.BadgeFrame.HeightRequest = 22;
                menuButton.BadgeFrame.WidthRequest = 22;
                menuButton.BadgeFrame.CornerRadius = 11;
            }
            return value;
        }

        private static async void BadgeCountPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is MenuButton menuButton)) return;

            if (!Library.PreviewFeatures.MenuButtonBadgeAnimation) return;
            await menuButton.BadgeFrame?.TranslateTo(0, -5, 150, Easing.CubicIn);
            menuButton.BadgeFrame?.TranslateTo(0, 0, 150, Easing.CubicInOut);
        }

        /// <summary>
        ///     <see cref="BadgeColor" />
        /// </summary>
        public static readonly BindableProperty BadgeColorProperty = BindableProperty.Create(nameof(BadgeColor), typeof(Color), typeof(MenuButton));

        /// <summary>
        ///     <see cref="BadgeTextColor"/>s
        /// </summary>
        public static readonly BindableProperty BadgeTextColorProperty = BindableProperty.Create(
            nameof(BadgeTextColor),
            typeof(Color),
            typeof(MenuButton),
            Color.Black);

        /// <summary>
        ///     Buttons that can be placed in a <see cref="FloatingActionMenuBehaviour" />.
        /// </summary>
        public MenuButton()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Color of the text in the badge.
        ///     This is a bindable property.
        /// </summary>
        public Color BadgeTextColor
        {
            get => (Color)GetValue(BadgeTextColorProperty);
            set => SetValue(BadgeTextColorProperty, value);
        }

        /// <summary>
        ///     Badge color.
        ///     This is a bindable porperty.
        /// </summary>
        public Color BadgeColor
        {
            get => (Color)GetValue(BadgeColorProperty);
            set => SetValue(BadgeColorProperty, value);
        }

        /// <summary>
        ///     Contents of badge.
        ///     This is a bindable porperty.
        /// </summary>
        public string BadgeCount
        {
            get => (string)GetValue(BadgeCountProperty);
            set => SetValue(BadgeCountProperty, value);
        }

        /// <summary>
        ///     Toggles badge on button.
        ///     This is a bindable porperty.
        /// </summary>
        public bool IsBadgeVisible
        {
            get => (bool)GetValue(IsBadgeVisibleProperty);
            set => SetValue(IsBadgeVisibleProperty, value);
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
            if (FloatingActionMenuParent != null && !IsEnabled)
            {
                FloatingActionMenuParent.m_behaviour.IsOpen = false;
            }

            if (IsEnabled)
            {
                Command?.Execute(CommandParameter);
            }
        }
    }
}