using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.FloatingActionMenu
{
    /// <summary>
    /// 
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [ContentProperty(nameof(Children))]
    public partial class FloatingActionMenu : ContentView
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly BindableProperty SizeProperty =
            BindableProperty.Create(nameof(Size), typeof(double), typeof(FloatingActionMenu), 60.0);

        /// <summary>
        /// 
        /// </summary>
        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(FloatingActionMenu));

        /// <summary>
        /// 
        /// </summary>
        public static readonly BindableProperty ExpandButtonTextProperty =
            BindableProperty.Create(nameof(ExpandButtonText), typeof(string), typeof(FloatingActionMenu), string.Empty);

        /// <summary>
        /// 
        /// </summary>
        public static readonly BindableProperty ExpandButtonBackgroundColorProperty =
            BindableProperty.Create(nameof(ExpandButtonBackgroundColor), typeof(Color), typeof(FloatingActionMenu),
                Color.MediumPurple);

        /// <summary>
        /// 
        /// </summary>
        public static readonly BindableProperty ExpandButtonFontIconColorProperty =
            BindableProperty.Create(nameof(ExpandButtonTextColor), typeof(Color), typeof(FloatingActionMenu),
                Color.White);

        /// <summary>
        /// 
        /// </summary>
        public static readonly BindableProperty ExpandButtonFontFamilyProperty =
            BindableProperty.Create(nameof(ExpandButtonFontFamily), typeof(string), typeof(FloatingActionMenu));

        /// <summary>
        /// 
        /// </summary>
        public static readonly BindableProperty CloseMenuProperty =
            BindableProperty.Create(nameof(CloseMenu), typeof(bool), typeof(FloatingActionMenu), false,
                propertyChanged: CloseMenuPropertyChanged);

        private readonly double m_spaceBetween = 10;
        private bool m_animationComplete = true;
        private bool m_isExpanded;
        private double m_yTranslate;

        /// <summary>
        /// 
        /// </summary>
        public FloatingActionMenu()
        {
            Children = new List<View>();

            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public string ExpandButtonFontFamily
        {
            get => (string)GetValue(ExpandButtonFontFamilyProperty);
            set => SetValue(ExpandButtonFontFamilyProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public new List<View> Children { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Color ExpandButtonTextColor
        {
            get => (Color)GetValue(ExpandButtonFontIconColorProperty);
            set => SetValue(ExpandButtonFontIconColorProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public Color ExpandButtonBackgroundColor
        {
            get => (Color)GetValue(ExpandButtonBackgroundColorProperty);
            set => SetValue(ExpandButtonBackgroundColorProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public string ExpandButtonText
        {
            get => (string)GetValue(ExpandButtonTextProperty);
            set => SetValue(ExpandButtonTextProperty, value);
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
        public double Size
        {
            get => (double)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CloseMenu
        {
            get => (bool)GetValue(CloseMenuProperty);
            set => SetValue(CloseMenuProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            AddButtons();
        }

        private static void CloseMenuPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is FloatingActionMenu menu && newvalue is true)
            {
                menu.AnimateAll(true);
                menu.CloseMenu = false;
            }
        }

        private async Task AnimateAll(bool isExpanded)
        {
            m_animationComplete = false;
            var multiplier = 1;

            foreach (var menuButton in Children)
                if (menuButton is MenuButton button)
                {
                    var maxOpacity = 1.0;
                    if (!button.IsEnabled) maxOpacity = .5;
                    button.TranslateTo(0, isExpanded ? 0 : -m_yTranslate * multiplier, 250, Easing.CubicInOut);
                    button.FadeTo(isExpanded ? 0 : maxOpacity, 250, Easing.CubicInOut);
                    multiplier += 1;
                }

            if (isExpanded) ExpandButton.FadeTo(.5, 250, Easing.CubicInOut);
            else ExpandButton.FadeTo(1, 250, Easing.CubicInOut);

            await ExpandButton.RelRotateTo(180, 250, Easing.CubicInOut);
            m_animationComplete = true;
            m_isExpanded = !isExpanded;
        }

        private async void ExpandButton_OnTapped(object sender, EventArgs e)
        {
            if (m_animationComplete) await AnimateAll(m_isExpanded);
        }

        private void AddButtons()
        {
            if (!(Parent is RelativeLayout modalityLayout)) return;
            var currentPage = Navigation.NavigationStack.Last();

            foreach (var child in Children)
            {
                if (child is MenuButton menuButton)
                {
                    menuButton.FloatingActionMenuParent = this;
                    menuButton.Size = Size;
                    menuButton.Opacity = 0;

                    if (menuButton.Page == currentPage.GetType()) menuButton.IsEnabled = false;
                }

                modalityLayout.Children.Add(child, RelativeLayout.GetXConstraint(this),
                    RelativeLayout.GetYConstraint(this));
            }

            AdjustXPositions();
            modalityLayout.RaiseChild(ExpandButton);
            m_yTranslate = Size + m_spaceBetween;
        }

        private void AdjustXPositions()
        {
            foreach (var child in Children)
                if (child is MenuButton menuButton)
                    RelativeLayout.SetXConstraint(menuButton,
                        Constraint.Constant(ExpandButton.X + Size - menuButton.Width));
        }
    }
}