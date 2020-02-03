using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.FloatingActionMenu
{
    /// <summary>
    /// 
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuButton : ContentView
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(MenuButton), string.Empty);

        /// <summary>
        /// 
        /// </summary>
        public static readonly BindableProperty TapCommandProperty =
            BindableProperty.Create(nameof(TapCommand), typeof(ICommand), typeof(MenuButton));

        /// <summary>
        /// 
        /// </summary>
        public static readonly BindableProperty TapCommandParameterProperty =
            BindableProperty.Create(nameof(TapCommandParameter), typeof(object), typeof(MenuButton));

        /// <summary>
        /// 
        /// </summary>
        public new static readonly BindableProperty BackgroundColorProperty =
            BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MenuButton), Color.White);

        /// <summary>
        /// 
        /// </summary>
        public static readonly BindableProperty TitleTextColorProperty =
            BindableProperty.Create(nameof(TitleTextColor), typeof(Color), typeof(MenuButton), Color.White);

        /// <summary>
        /// 
        /// </summary>
        public static readonly BindableProperty IconColorProperty =
            BindableProperty.Create(nameof(FontIconColor), typeof(Color), typeof(MenuButton), Color.White);

        /// <summary>
        /// 
        /// </summary>
        public static readonly BindableProperty FontIconProperty =
            BindableProperty.Create(nameof(FontIcon), typeof(string), typeof(MenuButton), string.Empty);

        /// <summary>
        /// 
        /// </summary>
        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MenuButton));

        /// <summary>
        /// 
        /// </summary>
        public static readonly BindableProperty SizeProperty =
            BindableProperty.Create(nameof(Size), typeof(double), typeof(MenuButton), .0);

        /// <summary>
        /// 
        /// </summary>
        public new static readonly BindableProperty IsEnabledProperty =
            BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(MenuButton), true);

        /// <summary>
        /// 
        /// </summary>
        public MenuButton()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public FloatingActionMenu? FloatingActionMenuParent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Type? Page { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Size
        {
            get => (double)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public new bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public string FontIcon
        {
            get => (string)GetValue(FontIconProperty);
            set => SetValue(FontIconProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public Color FontIconColor
        {
            get => (Color)GetValue(IconColorProperty);
            set => SetValue(IconColorProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public Color TitleTextColor
        {
            get => (Color)GetValue(TitleTextColorProperty);
            set => SetValue(TitleTextColorProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public object TapCommandParameter
        {
            get => GetValue(TapCommandParameterProperty);
            set => SetValue(TapCommandParameterProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand TapCommand
        {
            get => (ICommand)GetValue(TapCommandProperty);
            set => SetValue(TapCommandProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MenuButton_OnClicked(object sender, EventArgs e)
        {
            if (TapCommand != null && IsEnabled && FloatingActionMenuParent != null)
            {
                TapCommand?.Execute(Page);
                FloatingActionMenuParent.CloseMenu = true;
            }
        }

    }
}