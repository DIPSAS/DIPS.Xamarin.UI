using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Controls.Modality;
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
        /// 
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
        private readonly Lazy<Frame> m_overLay;

        private readonly double m_spaceBetween = 10;
        private bool m_animationComplete = true;
        private bool m_first = true;
        private bool m_isExpanded;
        private Layout? m_parent;
        private double m_yTranslate;

        /// <summary>
        /// </summary>
        public FloatingActionMenu()
        {
            Children = new List<View>();
            InitializeComponent();
            m_overLay = new Lazy<Frame>(CreateOverlay);
            m_closeMenuRecognizer = new TapGestureRecognizer { Command = new Command(Hide) };
        }

        /// <summary>
        /// 
        /// </summary>
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        /// <summary>
        /// </summary>
        public double XConstraint { get; set; }

        /// <summary>
        /// </summary>
        public double YConstraint { get; set; }

        /// <summary>
        /// </summary>
        public new List<View> Children { get; set; }

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
        /// </summary>
        public bool CloseMenu
        {
            get => (bool)GetValue(CloseMenuProperty);
            set => SetValue(CloseMenuProperty, value);
        }

        private Frame CreateOverlay()
        {
            var overlayFrame = new Frame { BackgroundColor = Color.Gray, InputTransparent = true, Opacity = 0.0 };

            overlayFrame.GestureRecognizers.Add(m_closeMenuRecognizer);
            return overlayFrame;
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
                m_overLay.Value.InputTransparent = false;
                m_overLay.Value.FadeTo(.5);
            }
            else
            {
                m_overLay.Value.InputTransparent = true;
                m_overLay.Value.FadeTo(0);
            }

            var multiplier = 1;

            foreach (var menuButton in Children)
                if (menuButton is MenuButton button)
                {
                    button.InputTransparent = isExpanded;
                    var maxOpacity = button.IsEnabled ? 1 : .5;
                    button.TranslateTo(0, isExpanded ? 0 : -m_yTranslate * multiplier, 250, Easing.CubicInOut);
                    button.FadeTo(isExpanded ? 0 : maxOpacity, 250, Easing.CubicInOut);
                    multiplier += 1;
                }

            if (isExpanded) ExpandButton.FadeTo(.5, 250, Easing.CubicInOut);
            else ExpandButton.FadeTo(1, 250, Easing.CubicInOut);

            await ExpandButton.RelRotateTo(180, 250, Easing.CubicInOut);
            m_isExpanded = !isExpanded;
            m_animationComplete = true;
        }

        private async void ExpandButton_OnClicked(object sender, EventArgs e)
        {
            if (m_first) AdjustXPositions();
            if (m_animationComplete) await AnimateAll(m_isExpanded);
        }

        /// <summary>
        /// </summary>
        /// <param name="layout"></param>
        public void AddTo(Layout layout)
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

            m_parent = layout;
            m_yTranslate = Size + m_spaceBetween;
        }

        private void AddButtonsToAbsolute(AbsoluteLayout parent)
        {
            parent.Children.Add(m_overLay.Value, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.SizeProportional);
            parent.Children.Add(
                ExpandButton,
                new Rectangle(XConstraint, YConstraint, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize),
                AbsoluteLayoutFlags.PositionProportional);
            foreach (var child in Children)
                if (child is MenuButton menuButton)
                {
                    menuButton.FloatingActionMenuParent = this;
                    menuButton.Size = Size;
                    parent.Children.Add(
                        child,
                        new Rectangle(XConstraint, YConstraint, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize),
                        AbsoluteLayoutFlags.PositionProportional);
                }
        }

        private void AddButtonsToRelative(RelativeLayout parent)
        {
            parent.Children.Add(
                m_overLay.Value,
                Constraint.RelativeToParent(p => p.X),
                Constraint.RelativeToParent(p => p.Y),
                Constraint.RelativeToParent(p => p.Width),
                Constraint.RelativeToParent(p => p.Height));
            parent.Children.Add(
                ExpandButton,
                Constraint.RelativeToParent(p => p.Width * XConstraint),
                Constraint.RelativeToParent(p => p.Height * YConstraint));
            foreach (var child in Children)
                if (child is MenuButton menuButton)
                {
                    menuButton.FloatingActionMenuParent = this;
                    menuButton.Size = Size;
                    parent.Children.Add(
                        menuButton,
                        Constraint.RelativeToParent(p => p.Width * XConstraint),
                        Constraint.RelativeToParent(p => p.Height * YConstraint));
                }
        }

        private void AdjustXPositions()
        {
            foreach (var child in Children)
                if (child is MenuButton menuButton)
                {
                    if (m_parent is AbsoluteLayout)
                    {
                        AbsoluteLayout.SetLayoutFlags(child, AbsoluteLayoutFlags.None);
                        AbsoluteLayout.SetLayoutBounds(
                            child,
                            new Rectangle(ExpandButton.X + Size - menuButton.Width, ExpandButton.Y, menuButton.Width, menuButton.Height));
                    }
                    else if (m_parent is RelativeLayout)
                    {
                        RelativeLayout.SetXConstraint(menuButton, Constraint.Constant(ExpandButton.X + Size - menuButton.Width));
                    }
                }
            m_first = false;
        }

        /// <summary>
        /// </summary>
        public void RaiseMenu()
        {
            m_parent?.RaiseChild(m_overLay.Value);
            foreach (var child in Children) m_parent?.RaiseChild(child);
            m_parent?.RaiseChild(ExpandButton);
        }
    }
}