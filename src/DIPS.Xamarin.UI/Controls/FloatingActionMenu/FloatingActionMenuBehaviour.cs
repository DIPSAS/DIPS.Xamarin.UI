using System;
using System.Collections.Generic;
using System.Windows.Input;
using DIPS.Xamarin.UI.Controls.Modality;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.FloatingActionMenu
{
    /// <summary>
    /// A behaviour that can be added to <see cref="ModalityLayout"/> to enable a floating action menu. 
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
            BindingMode.TwoWay,
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

        /// <summary>
        ///     <see cref="IsVisible" />
        /// </summary>
        public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(
            nameof(IsVisible),
            typeof(bool),
            typeof(FloatingActionMenuBehaviour),
            true,
            propertyChanged: IsVisiblePropertyChanged);

        private static void IsVisiblePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is FloatingActionMenuBehaviour behaviour)
            {
                behaviour.Children.ForEach(mb => mb.IsVisible = (bool) newvalue);
            }
        }

        /// <summary>
        ///     Determines the visibility of the floating action menu.
        ///     This is a bindable property.
        /// </summary>
        public bool IsVisible
        {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }

        private Internal.Xaml.FloatingActionMenu? m_floatingActionMenu;
        private bool m_first = true;
        private ModalityLayout? m_modalityLayout;

        /// <summary>
        ///     Raise before opening animation starts.
        /// </summary>
        public event EventHandler BeforeOpen;

        /// <summary>
        ///     Raised after opening animation completes.
        /// </summary>
        public event EventHandler AfterOpen;

        /// <summary>
        ///     Raised before closing animation starts.
        /// </summary>
        public event EventHandler BeforeClose;

        /// <summary>
        ///     Raised after closing animation completes.
        /// </summary>
        public event EventHandler AfterClose;

        /// <summary>
        /// <see cref="AfterCloseCommandParameter"/>
        /// </summary>
        public static readonly BindableProperty AfterCloseCommandParameterProperty = BindableProperty.Create(nameof(AfterCloseCommandParameter), typeof(object), typeof(FloatingActionMenuBehaviour));

        /// <summary>
        /// Parameter passed to <see cref="AfterCloseCommand"/>
        /// This is a bindable property.
        /// </summary>
        public object AfterCloseCommandParameter
        {
            get => (object)GetValue(AfterCloseCommandParameterProperty);
            set => SetValue(AfterCloseCommandParameterProperty, value);
        }

        /// <summary>
        /// <see cref="BeforeCloseCommandParameter"/>
        /// </summary>
        public static readonly BindableProperty BeforeCloseCommandParameterProperty = BindableProperty.Create(nameof(BeforeCloseCommandParameter), typeof(object), typeof(FloatingActionMenuBehaviour));


        /// <summary>
        /// Parameter passed to <see cref="BeforeCloseCommand"/>
        /// This is a bindable property.
        /// </summary>
        public object BeforeCloseCommandParameter
        {
            get => (object)GetValue(BeforeCloseCommandParameterProperty);
            set => SetValue(BeforeCloseCommandParameterProperty, value);
        }


        /// <summary>
        /// <see cref="AfterOpenCommandParameter"/>
        /// </summary>
        public static readonly BindableProperty AfterOpenCommandParameterProperty = BindableProperty.Create(nameof(AfterOpenCommandParameter), typeof(object), typeof(FloatingActionMenuBehaviour));

        /// <summary>
        /// Parameter passed to <see cref="AfterOpenCommand"/>
        /// This is a bindable property.
        /// </summary>
        public object AfterOpenCommandParameter
        {
            get => (object)GetValue(AfterOpenCommandParameterProperty);
            set => SetValue(AfterOpenCommandParameterProperty, value);
        }

        /// <summary>
        /// <see cref="BeforeOpenCommandParameter"/>
        /// </summary>
        public static readonly BindableProperty BeforeOpenCommandParameterProperty = BindableProperty.Create(nameof(BeforeOpenCommandParameter), typeof(object), typeof(FloatingActionMenuBehaviour));

        /// <summary>
        /// Parameter passed to <see cref="BeforeOpenCommand"/>
        /// This is a bindable property.
        /// </summary>
        public object BeforeOpenCommandParameter
        {
            get => (object)GetValue(BeforeOpenCommandParameterProperty);
            set => SetValue(BeforeOpenCommandParameterProperty, value);
        }

        /// <summary>
        /// <see cref="AfterCloseCommand"/>
        /// </summary>
        public static readonly BindableProperty AfterCloseCommandProperty = BindableProperty.Create(nameof(AfterCloseCommand), typeof(ICommand), typeof(FloatingActionMenuBehaviour));

        /// <summary>
        /// <see cref="AfterClose"/>
        /// This is a bindable property.
        /// </summary>
        public ICommand AfterCloseCommand
        {
            get => (ICommand)GetValue(AfterCloseCommandProperty);
            set => SetValue(AfterCloseCommandProperty, value);
        }

        /// <summary>
        /// <see cref="BeforeCloseCommand"/>
        /// </summary>
        public static readonly BindableProperty BeforeCloseCommandProperty = BindableProperty.Create(nameof(BeforeCloseCommand), typeof(ICommand), typeof(FloatingActionMenuBehaviour));

        /// <summary>
        ///    <see cref="BeforeClose"/>
        /// This is a bindable property.
        /// </summary>
        public ICommand BeforeCloseCommand
        {
            get => (ICommand)GetValue(BeforeCloseCommandProperty);
            set => SetValue(BeforeCloseCommandProperty, value);
        }

        /// <summary>
        ///  <see cref="BeforeOpenCommand"/>
        /// </summary>
        public static readonly BindableProperty BeforeOpenCommandProperty = BindableProperty.Create(nameof(BeforeOpenCommand), typeof(ICommand), typeof(FloatingActionMenuBehaviour));

        /// <summary>
        ///     <see cref="BeforeOpen"/>
        /// This is a bindable property.
        /// </summary>
        public ICommand BeforeOpenCommand
        {
            get => (ICommand)GetValue(BeforeOpenCommandProperty);
            set => SetValue(BeforeOpenCommandProperty, value);
        }

        /// <summary>
        /// <see cref="AfterOpenCommand"/>
        /// </summary>
        public static readonly BindableProperty AfterOpenCommandProperty = BindableProperty.Create(nameof(AfterOpenCommand), typeof(ICommand), typeof(FloatingActionMenuBehaviour));


        /// <summary>
        /// <see cref="AfterOpen"/>
        /// This is a bindable property.
        /// </summary>
        public ICommand AfterOpenCommand
        {
            get => (ICommand)GetValue(AfterOpenCommandProperty);
            set => SetValue(AfterOpenCommandProperty, value);
        }

        /// <summary>
        ///     Add this behaviour to add a floating action menu.
        /// </summary>
        public FloatingActionMenuBehaviour()
        {
            Children = new List<MenuButton>();
        }

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
        ///     List of menu button children.
        /// </summary>
        public List<MenuButton> Children { get; set; }

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

        /// <summary>
        /// <see cref="CloseOnOverlayTapped"/>
        /// </summary>
        public static readonly BindableProperty CloseOnOverlayTappedProperty = BindableProperty.Create(nameof(CloseOnOverlayTapped), typeof(bool), typeof(FloatingActionMenuBehaviour), true, propertyChanged: OnCloseOnOverlayTappedPropertyChanged);

        private static void OnCloseOnOverlayTappedPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is FloatingActionMenuBehaviour floatingActionMenuBehaviour)) return;
            if (!(bool.TryParse(newvalue.ToString(), out var newBoolValue))) return;
            if (floatingActionMenuBehaviour.m_floatingActionMenu == null) return;
            floatingActionMenuBehaviour.m_floatingActionMenu.CloseOnOverlayTapped = newBoolValue;
        }

        /// <see cref="IModalityHandler.CloseOnOverlayTapped"/>
        public bool CloseOnOverlayTapped
        {
            get => (bool)GetValue(CloseOnOverlayTappedProperty);
            set => SetValue(CloseOnOverlayTappedProperty, value);
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
            base.OnAttachedTo(modalityLayout);
            m_modalityLayout = modalityLayout;
            m_floatingActionMenu = new Internal.Xaml.FloatingActionMenu(this);
            SubscribeToEvents();
            m_modalityLayout.SizeChanged += OnModalityLayoutSizeChanged;
            m_modalityLayout.BindingContextChanged += OnModalityBindingContextChanged;
        }

        private void SubscribeToEvents()
        {
            if (m_floatingActionMenu != null)
            {
                m_floatingActionMenu.BeforeOpen += FloatingActionMenuOnBeforeOpen;
                m_floatingActionMenu.AfterOpen += FloatingActionMenuOnAfterOpen;
                m_floatingActionMenu.BeforeClose += FloatingActionMenuOnBeforeClose;
                m_floatingActionMenu.AfterClose += FloatingActionMenuOnAfterClose;
            }
        }

        private void FloatingActionMenuOnAfterClose(object sender, EventArgs e)
        {
            AfterClose?.Invoke(this, EventArgs.Empty);   
        }

        private void FloatingActionMenuOnBeforeClose(object sender, EventArgs e)
        {
            BeforeClose?.Invoke(this, EventArgs.Empty);
        }

        private void FloatingActionMenuOnAfterOpen(object sender, EventArgs e)
        {
            AfterOpen?.Invoke(this, EventArgs.Empty);
        }

        private void FloatingActionMenuOnBeforeOpen(object sender, EventArgs e)
        {
            BeforeOpen?.Invoke(this, EventArgs.Empty);
        }

        private void OnModalityBindingContextChanged(object sender, EventArgs e)
        {
            BindingContext = m_modalityLayout?.BindingContext;
        }

        private void OnModalityLayoutSizeChanged(object sender, EventArgs e)
        {
            if (m_modalityLayout == null) return;

            if (m_first)
            {
                m_first = false;
                m_floatingActionMenu?.AddTo(m_modalityLayout);
            }
        }

        /// <inheritdoc />
        protected override void OnDetachingFrom(ModalityLayout modalityLayout)
        {
            if (m_modalityLayout == null) return;

            base.OnDetachingFrom(m_modalityLayout);
            UnsubscribeToEvents();
            m_modalityLayout.SizeChanged -= OnModalityLayoutSizeChanged;
            m_modalityLayout.BindingContextChanged -= OnModalityBindingContextChanged;
        }

        private void UnsubscribeToEvents()
        {
            if (m_floatingActionMenu != null)
            {
                m_floatingActionMenu.BeforeOpen -= FloatingActionMenuOnBeforeOpen;
                m_floatingActionMenu.AfterOpen -= FloatingActionMenuOnAfterOpen;
                m_floatingActionMenu.BeforeClose -= FloatingActionMenuOnBeforeClose;
                m_floatingActionMenu.AfterClose -= FloatingActionMenuOnAfterClose;
            }
        }
    }
}