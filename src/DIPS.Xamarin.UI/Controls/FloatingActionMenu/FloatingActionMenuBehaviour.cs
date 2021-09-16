using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <see cref="Source"/>
        /// </summary>
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(nameof(Source), typeof(string), typeof(FloatingActionMenuBehaviour), string.Empty);

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
                behaviour.Children.ForEach(mb => mb.IsVisible = (bool)newvalue);

                if (!(bool)newvalue)
                {
                    behaviour.Children.ForEach(mb => mb.IsBadgeVisible = false);
                }
                else
                {
                    if (behaviour.Children.Any(mb => !string.IsNullOrEmpty(mb.BadgeCount)))
                    {
                        behaviour.Children.ForEach(mb => mb.IsBadgeVisible = true);
                    }
                    else
                    {
                        behaviour.Children.ForEach(mb => mb.IsBadgeVisible = false);
                    }
                }
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
        ///     Raised before opening animation starts.
        /// </summary>
        public event EventHandler OnBeforeOpen;

        /// <summary>
        ///     Raised after opening animation completes.
        /// </summary>
        public event EventHandler OnAfterOpen;

        /// <summary>
        ///     Raised before closing animation starts.
        /// </summary>
        public event EventHandler OnBeforeClose;

        /// <summary>
        ///     Raised after closing animation completes.
        /// </summary>
        public event EventHandler OnAfterClose;

        /// <summary>
        /// <see cref="OnAfterCloseCommandParameter"/>
        /// </summary>
        public static readonly BindableProperty OnAfterCloseCommandParameterProperty = BindableProperty.Create(nameof(OnAfterCloseCommandParameter), typeof(object), typeof(FloatingActionMenuBehaviour));

        /// <summary>
        /// Parameter passed to <see cref="OnAfterCloseCommand"/>
        /// This is a bindable property.
        /// </summary>
        public object OnAfterCloseCommandParameter
        {
            get => (object)GetValue(OnAfterCloseCommandParameterProperty);
            set => SetValue(OnAfterCloseCommandParameterProperty, value);
        }

        /// <summary>
        /// The source of the image in the expand button. This is a bindable property.
        /// </summary>
        public string Source
        {
            get => (string)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        /// <summary>
        /// <see cref="OnBeforeCloseCommandParameter"/>
        /// </summary>
        public static readonly BindableProperty OnBeforeCloseCommandParameterProperty = BindableProperty.Create(nameof(OnBeforeCloseCommandParameter), typeof(object), typeof(FloatingActionMenuBehaviour));


        /// <summary>
        /// Parameter passed to <see cref="OnBeforeCloseCommand"/>
        /// This is a bindable property.
        /// </summary>
        public object OnBeforeCloseCommandParameter
        {
            get => (object)GetValue(OnBeforeCloseCommandParameterProperty);
            set => SetValue(OnBeforeCloseCommandParameterProperty, value);
        }


        /// <summary>
        /// <see cref="OnAfterOpenCommandParameter"/>
        /// </summary>
        public static readonly BindableProperty OnAfterOpenCommandParameterProperty = BindableProperty.Create(nameof(OnAfterOpenCommandParameter), typeof(object), typeof(FloatingActionMenuBehaviour));

        /// <summary>
        /// Parameter passed to <see cref="OnAfterOpenCommand"/>
        /// This is a bindable property.
        /// </summary>
        public object OnAfterOpenCommandParameter
        {
            get => (object)GetValue(OnAfterOpenCommandParameterProperty);
            set => SetValue(OnAfterOpenCommandParameterProperty, value);
        }

        /// <summary>
        /// <see cref="OnBeforeOpenCommandParameter"/>
        /// </summary>
        public static readonly BindableProperty OnBeforeOpenCommandParameterProperty = BindableProperty.Create(nameof(OnBeforeOpenCommandParameter), typeof(object), typeof(FloatingActionMenuBehaviour));

        /// <summary>
        /// Parameter passed to <see cref="OnBeforeOpenCommand"/>
        /// This is a bindable property.
        /// </summary>
        public object OnBeforeOpenCommandParameter
        {
            get => (object)GetValue(OnBeforeOpenCommandParameterProperty);
            set => SetValue(OnBeforeOpenCommandParameterProperty, value);
        }

        /// <summary>
        /// <see cref="OnAfterCloseCommand"/>
        /// </summary>
        public static readonly BindableProperty OnAfterCloseCommandProperty = BindableProperty.Create(nameof(OnAfterCloseCommand), typeof(ICommand), typeof(FloatingActionMenuBehaviour));

        /// <summary>
        /// <see cref="OnAfterClose"/>
        /// This is a bindable property.
        /// </summary>
        public ICommand OnAfterCloseCommand
        {
            get => (ICommand)GetValue(OnAfterCloseCommandProperty);
            set => SetValue(OnAfterCloseCommandProperty, value);
        }

        /// <summary>
        /// <see cref="OnBeforeCloseCommand"/>
        /// </summary>
        public static readonly BindableProperty OnBeforeCloseCommandProperty = BindableProperty.Create(nameof(OnBeforeCloseCommand), typeof(ICommand), typeof(FloatingActionMenuBehaviour));

        /// <summary>
        ///    <see cref="OnBeforeClose"/>
        /// This is a bindable property.
        /// </summary>
        public ICommand OnBeforeCloseCommand
        {
            get => (ICommand)GetValue(OnBeforeCloseCommandProperty);
            set => SetValue(OnBeforeCloseCommandProperty, value);
        }

        /// <summary>
        ///  <see cref="OnBeforeOpenCommand"/>
        /// </summary>
        public static readonly BindableProperty BeforeOpenCommandProperty = BindableProperty.Create(nameof(OnBeforeOpenCommand), typeof(ICommand), typeof(FloatingActionMenuBehaviour));

        /// <summary>
        ///     <see cref="OnBeforeOpen"/>
        /// This is a bindable property.
        /// </summary>
        public ICommand OnBeforeOpenCommand
        {
            get => (ICommand)GetValue(BeforeOpenCommandProperty);
            set => SetValue(BeforeOpenCommandProperty, value);
        }

        /// <summary>
        /// <see cref="OnAfterOpenCommand"/>
        /// </summary>
        public static readonly BindableProperty OnAfterOpenCommandProperty = BindableProperty.Create(nameof(OnAfterOpenCommand), typeof(ICommand), typeof(FloatingActionMenuBehaviour));


        /// <summary>
        /// <see cref="OnAfterOpen"/>
        /// This is a bindable property.
        /// </summary>
        public ICommand OnAfterOpenCommand
        {
            get => (ICommand)GetValue(OnAfterOpenCommandProperty);
            set => SetValue(OnAfterOpenCommandProperty, value);
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
                m_floatingActionMenu.OnBeforeOpen += FloatingActionMenuOnBeforeOpen;
                m_floatingActionMenu.OnAfterOpen += FloatingActionMenuOnAfterOpen;
                m_floatingActionMenu.OnBeforeClose += FloatingActionMenuOnBeforeClose;
                m_floatingActionMenu.OnAfterClose += FloatingActionMenuOnAfterClose;
            }
        }

        private void FloatingActionMenuOnAfterClose(object sender, EventArgs e)
        {
            OnAfterClose?.Invoke(this, EventArgs.Empty);   
        }

        private void FloatingActionMenuOnBeforeClose(object sender, EventArgs e)
        {
            OnBeforeClose?.Invoke(this, EventArgs.Empty);
        }

        private void FloatingActionMenuOnAfterOpen(object sender, EventArgs e)
        {
            OnAfterOpen?.Invoke(this, EventArgs.Empty);
        }

        private void FloatingActionMenuOnBeforeOpen(object sender, EventArgs e)
        {
            OnBeforeOpen?.Invoke(this, EventArgs.Empty);
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
                m_floatingActionMenu.OnBeforeOpen -= FloatingActionMenuOnBeforeOpen;
                m_floatingActionMenu.OnAfterOpen -= FloatingActionMenuOnAfterOpen;
                m_floatingActionMenu.OnBeforeClose -= FloatingActionMenuOnBeforeClose;
                m_floatingActionMenu.OnAfterClose -= FloatingActionMenuOnAfterClose;
            }
        }
    }
}