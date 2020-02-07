using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.FloatingActionMenu
{
    /// <summary>
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [ContentProperty(nameof(Children))]
    public partial class FloatingActionMenu : ContentView
    {
        /// <summary>
        /// </summary>
        public static readonly BindableProperty SizeProperty = BindableProperty.Create(
            nameof(Size),
            typeof(double),
            typeof(FloatingActionMenu),
            60.0);

        /// <summary>
        /// </summary>
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
            nameof(FontSize),
            typeof(double),
            typeof(FloatingActionMenu),
            12.0);

        /// <summary>
        /// </summary>
        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
            nameof(FontFamily),
            typeof(string),
            typeof(FloatingActionMenu));

        /// <summary>
        /// </summary>
        public static readonly BindableProperty ExpandButtonTextProperty = BindableProperty.Create(
            nameof(ExpandButtonText),
            typeof(string),
            typeof(FloatingActionMenu),
            string.Empty);

        /// <summary>
        /// </summary>
        public static readonly BindableProperty ExpandButtonBackgroundColorProperty = BindableProperty.Create(
            nameof(ExpandButtonBackgroundColor),
            typeof(Color),
            typeof(FloatingActionMenu),
            Color.MediumPurple);

        /// <summary>
        /// </summary>
        public static readonly BindableProperty ExpandButtonTextColorProperty = BindableProperty.Create(
            nameof(ExpandButtonTextColor),
            typeof(Color),
            typeof(FloatingActionMenu),
            Color.White);

        /// <summary>
        /// </summary>
        public static readonly BindableProperty CloseMenuProperty = BindableProperty.Create(
            nameof(CloseMenu),
            typeof(bool),
            typeof(FloatingActionMenu),
            false,
            propertyChanged: CloseMenuPropertyChanged);

        private readonly IGestureRecognizer m_closeMenuRecognizer;
        private readonly Frame m_overLay;

        private readonly double m_spaceBetween = 10;
        private bool m_animationComplete = true;
        private bool m_first = true;
        private bool m_isExpanded;
        private Layout? m_parent;
        private double m_yTranslate;

        /// <summary>
        ///     A floating action menu that can be added to either a RelativeLayout or an AbsoluteLayout. Add
        ///     <value>MenuButtons</value>
        ///     as content.
        /// </summary>
        public FloatingActionMenu()
        {
            Children = new List<MenuButton>();
            InitializeComponent();
            m_overLay = new Frame { BackgroundColor = Color.Gray, Opacity = 0.0, IsVisible = false };
            m_closeMenuRecognizer = new TapGestureRecognizer { Command = new Command(Hide) };
        }

        /// <summary>
        ///     FontSize of the text in the expand button.
        /// </summary>
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        /// <summary>
        ///     The Z-position of the control. Is proportional to the Layout it's added to. Values between 0-1.
        /// </summary>
        public double XConstraint { get; set; }

        /// <summary>
        ///     The Y-position of the control. Is proportional to the Layout it's added to. Values between 0-1.
        /// </summary>
        public double YConstraint { get; set; }

        /// <summary>
        /// </summary>
        public new List<MenuButton> Children { get; set; }

        /// <summary>
        /// </summary>
        public Color ExpandButtonTextColor
        {
            get => (Color)GetValue(ExpandButtonTextColorProperty);
            set => SetValue(ExpandButtonTextColorProperty, value);
        }

        /// <summary>
        /// </summary>
        public Color ExpandButtonBackgroundColor
        {
            get => (Color)GetValue(ExpandButtonBackgroundColorProperty);
            set => SetValue(ExpandButtonBackgroundColorProperty, value);
        }

        /// <summary>
        /// </summary>
        public string ExpandButtonText
        {
            get => (string)GetValue(ExpandButtonTextProperty);
            set => SetValue(ExpandButtonTextProperty, value);
        }

        /// <summary>
        ///     FontFamily of the text in the expand button.
        /// </summary>
        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        /// <summary>
        /// </summary>
        public double Size
        {
            get => (double)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        /// <summary>
        ///     Set to true to close the menu if its open.
        /// </summary>
        public bool CloseMenu
        {
            get => (bool)GetValue(CloseMenuProperty);
            set => SetValue(CloseMenuProperty, value);
        }

        private void Hide()
        {
            if (m_animationComplete) AnimateAll(true);
        }

        private static void CloseMenuPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is FloatingActionMenu menu && newvalue is true)
            {
                menu.Hide();
                menu.CloseMenu = false;
            }
        }

        private async Task AnimateAll(bool isExpanded)
        {
            m_animationComplete = false;

            if (!isExpanded)
            {
                m_overLay.IsVisible = true;
                m_overLay.FadeTo(.5);
            }
            else
            {
                m_overLay.FadeTo(0);
            }

            var multiplier = 1;

            foreach (var menuButton in Children)
            {
                menuButton.InputTransparent = isExpanded;
                var maxOpacity = menuButton.IsEnabled ? 1 : .5;
                menuButton.TranslateTo(0, isExpanded ? 0 : -m_yTranslate * multiplier, 250, Easing.CubicInOut);
                menuButton.FadeTo(isExpanded ? 0 : maxOpacity, 250, Easing.CubicInOut);
                multiplier += 1;
            }

            if (isExpanded) ExpandButton.FadeTo(.5, 250, Easing.CubicInOut);
            else ExpandButton.FadeTo(1, 250, Easing.CubicInOut);

            await ExpandButton.RelRotateTo(180, 250, Easing.CubicInOut);
            if (isExpanded) m_overLay.IsVisible = false;
            m_isExpanded = !isExpanded;
            m_animationComplete = true;
        }

        private async void ExpandButton_OnClicked(object sender, EventArgs e)
        {
            if (m_first) AdjustXPositions();
            if (m_animationComplete) await AnimateAll(m_isExpanded);
        }

        internal void AddTo(Layout layout)
        {
            switch (layout)
            {
                case RelativeLayout relativeLayout:
                    AddButtonsToRelative(relativeLayout);
                    break;
                case AbsoluteLayout absoluteLayout:
                    AddButtonsToAbsolute(absoluteLayout);
                    break;
                default:
                    return;
            }

            m_overLay.GestureRecognizers.Add(m_closeMenuRecognizer);

            m_parent = layout;
            m_yTranslate = Size + m_spaceBetween;
        }

        private void AddButtonsToAbsolute(AbsoluteLayout parent)
        {
            parent.Children.Add(m_overLay, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.SizeProportional);
            parent.Children.Add(
                ExpandButton,
                new Rectangle(XConstraint, YConstraint, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize),
                AbsoluteLayoutFlags.PositionProportional);
            foreach (var child in Children)
            {
                child.FloatingActionMenuParent = this;
                child.Size = Size;
                parent.Children.Add(
                    child,
                    new Rectangle(XConstraint, YConstraint, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize),
                    AbsoluteLayoutFlags.PositionProportional);
            }
        }

        private void AddButtonsToRelative(RelativeLayout parent)
        {
            parent.Children.Add(
                m_overLay,
                Constraint.RelativeToParent(p => p.X),
                Constraint.RelativeToParent(p => p.Y),
                Constraint.RelativeToParent(p => p.Width),
                Constraint.RelativeToParent(p => p.Height));
            parent.Children.Add(
                ExpandButton,
                Constraint.RelativeToParent(p => p.Width * XConstraint),
                Constraint.RelativeToParent(p => p.Height * YConstraint));
            foreach (var child in Children)
            {
                child.FloatingActionMenuParent = this;
                child.Size = Size;
                parent.Children.Add(
                    child,
                    Constraint.RelativeToParent(p => p.Width * XConstraint),
                    Constraint.RelativeToParent(p => p.Height * YConstraint));
            }
        }

        private void AdjustXPositions()
        {
            foreach (var child in Children)
                if (m_parent is AbsoluteLayout)
                {
                    AbsoluteLayout.SetLayoutFlags(child, AbsoluteLayoutFlags.None);
                    AbsoluteLayout.SetLayoutBounds(
                        child,
                        new Rectangle(ExpandButton.X + Size - child.Width, ExpandButton.Y, child.Width, child.Height));
                }
                else if (m_parent is RelativeLayout)
                {
                    RelativeLayout.SetXConstraint(child, Constraint.Constant(ExpandButton.X + Size - child.Width));
                }

            m_first = false;
        }

        internal void RaiseMenu()
        {
            m_parent?.RaiseChild(m_overLay);
            foreach (var child in Children) m_parent?.RaiseChild(child);
            m_parent?.RaiseChild(ExpandButton);
        }
    }
}