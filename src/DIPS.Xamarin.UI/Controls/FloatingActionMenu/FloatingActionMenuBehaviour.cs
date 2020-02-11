using System;
using System.Collections.Generic;
using DIPS.Xamarin.UI.Controls.Modality;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.FloatingActionMenu
{
    /// <summary>
    /// </summary>
    [ContentProperty(nameof(Children))]
    public class FloatingActionMenuBehaviour : Behavior<ModalityLayout>
    {
        /// <summary>
        ///     <see cref="IsOpen" />
        /// </summary>
        public static readonly BindableProperty IsOpenProperty = BindableProperty.Create(
            nameof(IsOpen),
            typeof(bool),
            typeof(Internal.Xaml.FloatingActionMenu),
            false,
            propertyChanged: IsOpenPropertyChanged);

        /// <summary>
        ///     <see cref="Size" />
        /// </summary>
        public static readonly BindableProperty SizeProperty = BindableProperty.Create(
            nameof(global::Xamarin.Forms.Size),
            typeof(double),
            typeof(Internal.Xaml.FloatingActionMenu),
            60.0);

        /// <summary>
        ///     <see cref="ExpandButtonFontSize" />
        /// </summary>
        public static readonly BindableProperty ExpandButtonFontSizeProperty = BindableProperty.Create(
            nameof(ExpandButtonFontSize),
            typeof(double),
            typeof(Internal.Xaml.FloatingActionMenu),
            12.0);

        /// <summary>
        ///     <see cref="ExpandButtonFontFamily" />
        /// </summary>
        public static readonly BindableProperty ExpandButtonFontFamilyProperty = BindableProperty.Create(
            nameof(ExpandButtonFontFamily),
            typeof(string),
            typeof(Internal.Xaml.FloatingActionMenu));

        /// <summary>
        ///     <see cref="ExpandButtonText" />
        /// </summary>
        public static readonly BindableProperty ExpandButtonTextProperty = BindableProperty.Create(
            nameof(ExpandButtonText),
            typeof(string),
            typeof(Internal.Xaml.FloatingActionMenu),
            string.Empty);

        /// <summary>
        ///     <see cref="ExpandButtonBackgroundColor" />
        /// </summary>
        public static readonly BindableProperty ExpandButtonBackgroundColorProperty = BindableProperty.Create(
            nameof(ExpandButtonBackgroundColor),
            typeof(Color),
            typeof(Internal.Xaml.FloatingActionMenu),
            Color.MediumPurple);

        /// <summary>
        ///     <see cref="ExpandButtonTextColor" />
        /// </summary>
        public static readonly BindableProperty ExpandButtonTextColorProperty = BindableProperty.Create(
            nameof(ExpandButtonTextColor),
            typeof(Color),
            typeof(Internal.Xaml.FloatingActionMenu),
            Color.White);

        private Internal.Xaml.FloatingActionMenu? m_floatingActionMenu;
        private ModalityLayout? m_modaliyLayout;
        private bool m_first = true;

        /// <summary>
        ///     Describes the current state of the menu.
        ///     This is a bindable property.
        /// </summary>
        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }

        /// <summary>
        /// <see cref="Children"/>
        /// </summary>
        public static readonly BindableProperty ChildrenProperty = BindableProperty.Create(nameof(Children), typeof(List<MenuButton>), typeof(FloatingActionMenuBehaviour), new List<MenuButton>(), propertyChanged:OnChildrenChanged);

        private static void OnChildrenChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is FloatingActionMenuBehaviour floatingActionMenuBehaviour)) return;
            if (floatingActionMenuBehaviour.m_modaliyLayout != null)
            {
                floatingActionMenuBehaviour.m_floatingActionMenu?.AddTo(floatingActionMenuBehaviour.m_modaliyLayout);
            }
        }

        /// <summary>
        ///     List of menu button children.
        /// </summary>
        public List<MenuButton> Children
        {
            get => (List<MenuButton>)GetValue(ChildrenProperty);
            set => SetValue(ChildrenProperty, value);
        }

        /// <summary>
        ///     The X-position of the control. Is proportional to the Layout it's added to. Values between 0-1.
        /// </summary>
        public double XPosition { get; set; }

        /// <summary>
        ///     The Y-position of the control. Is proportional to the Layout it's added to. Values between 0-1.
        /// </summary>
        public double YPosition { get; set; }

        /// <summary>
        ///     The text color of the text in the expand button.
        ///     This is a bindable property.
        /// </summary>
        public Color ExpandButtonTextColor
        {
            get => (Color)GetValue(ExpandButtonTextColorProperty);
            set => SetValue(ExpandButtonTextColorProperty, value);
        }

        /// <summary>
        ///     The background color of the expand button.
        ///     This is a bindable property.
        /// </summary>
        public Color ExpandButtonBackgroundColor
        {
            get => (Color)GetValue(ExpandButtonBackgroundColorProperty);
            set => SetValue(ExpandButtonBackgroundColorProperty, value);
        }

        /// <summary>
        ///     The text in the expand button.
        ///     This is a bindable property.
        /// </summary>
        public string ExpandButtonText
        {
            get => (string)GetValue(ExpandButtonTextProperty);
            set => SetValue(ExpandButtonTextProperty, value);
        }

        /// <summary>
        ///     FontFamily of the text in the expand button.
        ///     This is a bindable property.
        /// </summary>
        public string ExpandButtonFontFamily
        {
            get => (string)GetValue(ExpandButtonFontFamilyProperty);
            set => SetValue(ExpandButtonFontFamilyProperty, value);
        }

        /// <summary>
        ///     The size of the buttons.
        ///     This is a bindable property.
        /// </summary>
        public double Size
        {
            get => (double)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        /// <summary>
        ///     FontSize of the text in the expand button.
        ///     This is a bindable property.
        /// </summary>
        public double ExpandButtonFontSize
        {
            get => (double)GetValue(ExpandButtonFontSizeProperty);
            set => SetValue(ExpandButtonFontSizeProperty, value);
        }

        private static void IsOpenPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is FloatingActionMenuBehaviour menuBehaviour)
            {
                menuBehaviour.m_floatingActionMenu?.ShowMenu((bool)newvalue);
            }
        }

        /// <inheritdoc />
        protected override void OnAttachedTo(ModalityLayout modalityLayout)
        {
            m_modaliyLayout = modalityLayout;
            base.OnAttachedTo(m_modaliyLayout);
            m_floatingActionMenu = new Internal.Xaml.FloatingActionMenu(this);
            m_modaliyLayout.SizeChanged += OnModalityLayoutSizeChanged;
            m_modaliyLayout.BindingContextChanged += OnModalityLayoutBindingContextChanged;
        }

        /// <inheritdoc />
        protected override void OnDetachingFrom(ModalityLayout bindable)
        {
            base.OnDetachingFrom(bindable);
            if (m_modaliyLayout == null) return;
            m_modaliyLayout.SizeChanged -= OnModalityLayoutSizeChanged;
            m_modaliyLayout.BindingContextChanged -= OnModalityLayoutBindingContextChanged;
        }

        private void OnModalityLayoutBindingContextChanged(object sender, EventArgs e)
        {
            BindingContext = m_modaliyLayout?.BindingContext;
        }

        private void OnModalityLayoutSizeChanged(object sender, EventArgs e)
        {
            if (m_modaliyLayout != null && m_first)
            {
                m_first = false;
                m_floatingActionMenu?.AddTo(m_modaliyLayout);
            }
        }
    }
}