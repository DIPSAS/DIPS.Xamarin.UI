using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Popup
{
    /// <summary>
    /// Behavior to be added to an item you want to open a popup from. This item _has_ to be inside a PopupLayout somehow. Else, nothing would happend.
    /// </summary>
    [ContentProperty(nameof(Content))]
    [ExcludeFromCodeCoverage]
    public class PopupBehavior : Behavior<View>
    {
        private readonly Command m_onTappedCommand;
        private View? m_attachedTo;
        /// <summary>
        /// Creates the instance
        /// </summary>
        public PopupBehavior()
        {
            m_onTappedCommand = new Command(ShowPopup);
        }

        /// <summary>
        /// Handels attaching to item
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnAttachedTo(View bindable)
        {
            m_attachedTo = bindable;
            BindingContext = bindable.BindingContext;
            bindable.BindingContextChanged += (s, e) => BindingContext = bindable.BindingContext;
            if (bindable is Button button)
            {
                button.Clicked += (s,e) => ShowPopup();
            }
            else
            {
                bindable.GestureRecognizers.Add(new TapGestureRecognizer { Command = m_onTappedCommand });
            }

            base.OnAttachedTo(bindable);
        }

        /// <summary>
        /// <see cref="BindingContextFactory" />
        /// </summary>
        public static readonly BindableProperty BindingContextFactoryProperty =
            BindableProperty.Create(nameof(BindingContextFactory), typeof(Func<object>), typeof(PopupBehavior), null);

        /// <summary>
        /// Used to set the binding context of the popup content. If this is null, the binding context is innherited from the attached element.
        /// </summary>
        public Func<object> BindingContextFactory
        {
            get { return (Func<object>)GetValue(BindingContextFactoryProperty); }
            set { SetValue(BindingContextFactoryProperty, value); }
        }

        /// <summary>
        /// <see cref="Direction" />
        /// </summary>
        [Obsolete("Use VerticalPopupOptions and HorizontalPopupOptions instead.")]
        public static readonly BindableProperty DirectionProperty =
            BindableProperty.Create(nameof(Direction), typeof(PopupDirection), typeof(PopupBehavior), PopupDirection.None);

        /// <summary>
        /// Direction of where the popup will show, auto is default.
        /// </summary>
        [Obsolete("Use VerticalPopupOptions and HorizontalPopupOptions instead.")]
        public PopupDirection Direction
        {
            get { return (PopupDirection)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }

        /// <summary>
        /// <see cref="HorizontalOptions" />
        /// </summary>
        public static readonly BindableProperty HorizontalOptionsProperty =
            BindableProperty.Create(nameof(HorizontalOptions), typeof(HorizontalPopupOptions), typeof(PopupBehavior), HorizontalPopupOptions.LeftAlign);

        /// <summary>
        /// Horizontal direction of where the popup will show
        /// </summary>
        public HorizontalPopupOptions HorizontalOptions
        {
            get { return (HorizontalPopupOptions)GetValue(HorizontalOptionsProperty); }
            set { SetValue(HorizontalOptionsProperty, value); }
        }

        /// <summary>
        /// <see cref="VerticalOptions" />
        /// </summary>
        public static readonly BindableProperty VerticalOptionsProperty =
            BindableProperty.Create(nameof(VerticalOptions), typeof(VerticalPopupOptions), typeof(PopupBehavior), VerticalPopupOptions.Auto);

        /// <summary>
        /// Vertical direction of where the popup will show
        /// </summary>
        public VerticalPopupOptions VerticalOptions
        {
            get { return (VerticalPopupOptions)GetValue(VerticalOptionsProperty); }
            set { SetValue(VerticalOptionsProperty, value); }
        }

        /// <summary>
        /// <see cref="Animation" />
        /// </summary>
        public static readonly BindableProperty AnimationProperty =
            BindableProperty.Create(nameof(Animation), typeof(PopupAnimation), typeof(PopupBehavior), PopupAnimation.None);

        /// <summary>
        /// How the popup will animate into view. Either none, sliding or fading.
        /// </summary>
        public PopupAnimation Animation
        {
            get { return (PopupAnimation)GetValue(AnimationProperty); }
            set { SetValue(AnimationProperty, value); }
        }

        /// <summary>
        ///  <see cref="Content" />
        /// </summary>
        public static readonly BindableProperty ContentProperty =
            BindableProperty.Create(nameof(Content), typeof(View), typeof(PopupBehavior));

        /// <summary>
        /// The content of the popup when it's showing.
        /// </summary>
        public View Content
        {
            get { return (View)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        /// <summary>
        ///  <see cref="IsOpen" />
        /// </summary>
        public static readonly BindableProperty IsOpenProperty =
            BindableProperty.Create(nameof(IsOpen), typeof(bool), typeof(PopupBehavior), false, defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnIsOpenChanged);

        /// <summary>
        /// Indicating if this popup is open. Set this from a binding to open a popup.
        /// Please be carefull if you want to use the same property for multiple popups on the same page.
        /// </summary>
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        private static void OnIsOpenChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is PopupBehavior behavior)) return;
            if (oldValue == newValue) return;
            if (newValue as bool? == true)
            {
                behavior.ShowPopup();
            }
            else
            {
                behavior.HidePopup();
            }
        }

        private void ShowPopup()
        {
            if (m_attachedTo == null)
            {
                return;
            }

            var layout = m_attachedTo.GetParentOfType<PopupLayout>();
            if (layout == null) throw new InvalidProgramException("Can't have a popup behavior without a PopupLayout around the element");
            var content = Content;
            layout.ShowPopup(content, m_attachedTo, this);
            content.BindingContext = BindingContextFactory?.Invoke() ?? BindingContext;
        }

        private void HidePopup()
        {
            if (m_attachedTo == null)
            {
                return;
            }


            var layout = m_attachedTo.GetParentOfType<PopupLayout>();
            if (layout == null) throw new InvalidProgramException("Can't have a popup behavior without a PopupLayout around the element");
            layout.HidePopup();
        }
    }

    public enum HorizontalPopupOptions
    {
        LeftAlign,
        RightAlign,
        Center,
        Left,
        Right
    }

    public enum VerticalPopupOptions
    {
        Auto,
        Above,
        Below,
        Center,
        Top,
        Bottom
    }

    /// <summary>
    /// Directions of the popup
    /// </summary>
    [Obsolete("Use VerticalPopupOptions and HorizontalPopupOptions instead.")]
    public enum PopupDirection
    {
        /// <summary>
        /// None is the default and enables the usage of the Vertidal/Horizontal-Options.
        /// </summary>
        None,

        /// <summary>
        /// Automatically based on the location on screen
        /// </summary>
        Auto,

        /// <summary>
        /// Below the placement target
        /// </summary>
        Below,
        
        /// <summary>
        /// Above the placement target
        /// </summary>
        Above,
    }

    /// <summary>
    /// Animations of the popup
    /// </summary>
    public enum PopupAnimation
    {
        /// <summary>
        /// Instantly shows the popup
        /// </summary>
        None,
        /// <summary>
        /// Slides from the element and outwards
        /// </summary>
        Slide,
        /// <summary>
        /// Fading the popup in
        /// </summary>
        Fade
    }
}
