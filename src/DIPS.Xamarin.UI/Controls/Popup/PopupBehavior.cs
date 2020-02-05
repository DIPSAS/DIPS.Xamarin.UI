using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Controls.Modality;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Popup
{
    /// <summary>
    ///     Behavior to use on a view element that needs to show a popup close to it. The view element has to be inside of a <see cref="ModalityLayout"/>
    /// </summary>
    [ContentProperty(nameof(Content))]
    [ExcludeFromCodeCoverage]
    public class PopupBehavior : Behavior<View>, IModalityHandler
    {
        private readonly Command m_onTappedCommand;

        private Task? m_animation;
        private View? m_attachedTo;
        private bool m_slideUp;
        private const int AnimationTime = 100;

        /// <summary>
        ///     <see cref="BindingContextFactory" />
        /// </summary>
        public static readonly BindableProperty BindingContextFactoryProperty = BindableProperty.Create(
            nameof(BindingContextFactory),
            typeof(Func<object>),
            typeof(PopupBehavior));

        /// <summary>
        ///     <see cref="IsOpen" />
        /// </summary>
        public static readonly BindableProperty IsOpenProperty = BindableProperty.Create(
            nameof(IsOpen),
            typeof(bool),
            typeof(PopupBehavior),
            false,
            BindingMode.TwoWay,
            propertyChanged: OnIsOpenChanged);

        /// <summary>
        ///     Creates the instance
        /// </summary>
        public PopupBehavior()
        {
            m_onTappedCommand = new Command(OnClick);
        }

        /// <summary>
        ///     How the popup will animate into view. Either none, sliding or fading.
        /// </summary>
        public PopupAnimation Animation { get; set; } = PopupAnimation.None;

        /// <summary>
        ///     Used to set the binding context of the popup content. If this is null, the binding context is innherited from the
        ///     attached element.
        /// </summary>
        public Func<object> BindingContextFactory
        {
            get => (Func<object>)GetValue(BindingContextFactoryProperty);
            set => SetValue(BindingContextFactoryProperty, value);
        }

        /// <summary>
        ///     The content of the popup when it's showing.
        /// </summary>
        public View? Content { get; set; }

        /// <summary>
        ///     Direction of where the popup will show, auto is default.
        /// </summary>
        [Obsolete("Use VerticalPopupOptions and HorizontalPopupOptions instead.")]
        public PopupDirection Direction { get; set; }

        /// <summary>
        ///     Horizontal direction of where the popup will show
        /// </summary>
        public HorizontalPopupOptions HorizontalOptions { get; set; } = HorizontalPopupOptions.LeftAlign;

        /// <summary>
        ///     Indicating if this popup is open. Set this from a binding to open a popup.
        ///     Please be carefull if you want to use the same property for multiple popups on the same page.
        /// </summary>
        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
        }

        /// <summary>
        ///     Vertical direction of where the popup will show
        /// </summary>
        public VerticalPopupOptions VerticalOptions { get; set; } = VerticalPopupOptions.Auto;

        /// <summary>
        ///     Hides the popup
        /// </summary>
        public void Hide()
        {
            IsOpen = false;
        }

        /// <inheritdoc />
        public async Task BeforeRemoval()
        {
            if (Content == null) return;

            await AnimateBack(Content);
        }

        /// <inheritdoc />
        public Task AfterRemoval() => Task.CompletedTask;

        /// <summary>
        ///     Handels attaching to item.
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnAttachedTo(View bindable)
        {
            m_attachedTo = bindable;
            BindingContext = bindable.BindingContext;
            bindable.BindingContextChanged += (s, e) => BindingContext = bindable.BindingContext;
            if (bindable is Button button)
                button.Clicked += (s, e) => OnClick();
            else
                bindable.GestureRecognizers.Add(new TapGestureRecognizer { Command = m_onTappedCommand });

            base.OnAttachedTo(bindable);
        }

        private void OnClick()
        {
            if (m_attachedTo == null) return;
            if (!Popup.GetOpenOnClick(m_attachedTo)) return;
            ShowPopup();
        }

        private static void OnIsOpenChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is PopupBehavior behavior)) return;
            if (oldValue == newValue) return;
            if (newValue as bool? == true)
                behavior.ShowPopup();
            else
                behavior.HidePopup();
        }

        private void ShowPopup()
        {
            if (m_attachedTo == null || Content == null) return;

            var layout = m_attachedTo.GetParentOfType<ModalityLayout>();
            if (layout == null) throw new InvalidProgramException("Can't have a popup behavior without a ModalityLayout around the element");

            var prevAnimation = m_animation;
            if (prevAnimation != null && !prevAnimation.IsCompleted && !prevAnimation.IsCanceled) return;

            m_animation = null;
            IsOpen = true;

            layout.Show(this, Content, m_attachedTo);

            if (Direction != PopupDirection.None)
            {
                PlaceItemLegacy(Content, m_attachedTo, layout);
            }
            else
            {
                PlaceItem(Content, m_attachedTo, layout);
            }

            m_animation = Animate(Content);

            Content.BindingContext = BindingContextFactory?.Invoke() ?? BindingContext;
        }

        private void PlaceItemLegacy(View popupView, View relativeView, ModalityLayout layout)
        {
            var sumMarginY = popupView.Margin.Top + popupView.Margin.Bottom;
            var sumMarginX = popupView.Margin.Left + popupView.Margin.Right;

            var direction = Direction;
            if (direction == PopupDirection.Auto)
            {
                var height = layout.Height;
                var center = height / 2.0;
                var itemPosition = relativeView.GetY(layout) + relativeView.Height / 2.0;
                if (itemPosition > center)
                    direction = PopupDirection.Above;
                else
                    direction = PopupDirection.Below;
            }

            var diffY = direction == PopupDirection.Below ? relativeView.Height : -popupView.Height - sumMarginY;
            m_slideUp = diffY < 0;

            RelativeLayout.SetYConstraint(
                popupView,
                Constraint.RelativeToParent(r => Math.Max(0, Math.Min(r.Height - popupView.Height - sumMarginY, relativeView.GetY(layout) + diffY))));
            RelativeLayout.SetXConstraint(
                popupView,
                Constraint.RelativeToParent(r => Math.Max(0, Math.Min(r.Width - popupView.Width - sumMarginX, relativeView.GetX(layout)))));
        }

        private void PlaceItem(View popupView, View relativeView, ModalityLayout layout)
        {
            var sumMarginX = popupView.Margin.Left + popupView.Margin.Right;
            switch (HorizontalOptions)
            {
                case HorizontalPopupOptions.LeftAlign:
                    RelativeLayout.SetXConstraint(
                        popupView,
                        Constraint.RelativeToParent(r => Math.Max(0, Math.Min(r.Width - popupView.Width - sumMarginX, relativeView.GetX(layout)))));
                    break;
                case HorizontalPopupOptions.RightAlign:
                    RelativeLayout.SetXConstraint(
                        popupView,
                        Constraint.RelativeToParent(
                            r => Math.Max(
                                0,
                                Math.Min(
                                    r.Width - popupView.Width - sumMarginX,
                                    relativeView.GetX(layout) + relativeView.Width - popupView.Width - sumMarginX))));
                    break;
                case HorizontalPopupOptions.Center:
                    RelativeLayout.SetXConstraint(
                        popupView,
                        Constraint.RelativeToParent(
                            r => Math.Max(
                                0,
                                Math.Min(
                                    r.Width - popupView.Width - sumMarginX,
                                    relativeView.GetX(layout) + relativeView.Width / 2 - popupView.Width / 2 - popupView.Margin.Left))));
                    break;
                case HorizontalPopupOptions.Left:
                    RelativeLayout.SetXConstraint(
                        popupView,
                        Constraint.RelativeToParent(
                            r => Math.Max(
                                0,
                                Math.Min(r.Width - popupView.Width - sumMarginX, relativeView.GetX(layout) - popupView.Width - sumMarginX))));
                    break;
                case HorizontalPopupOptions.Right:
                    RelativeLayout.SetXConstraint(
                        popupView,
                        Constraint.RelativeToParent(
                            r => Math.Max(0, Math.Min(r.Width - popupView.Width - sumMarginX, relativeView.GetX(layout) + relativeView.Width))));
                    break;
            }

            var sumMarginY = popupView.Margin.Top + popupView.Margin.Bottom;
            var verticalDirection = VerticalOptions;
            if (VerticalOptions == VerticalPopupOptions.Auto)
            {
                var height = layout.Height;
                var center = height / 2.0;
                var itemPosition = relativeView.GetY(layout) + relativeView.Height / 2.0;
                verticalDirection = itemPosition > center ? VerticalPopupOptions.Above : VerticalPopupOptions.Below;
            }

            Func<double> diffY = ()=>0.0;

            if (verticalDirection == VerticalPopupOptions.Above)
            {
                diffY = ()=>-popupView.Height - sumMarginY;
            }
            else if (verticalDirection == VerticalPopupOptions.Below)
            {
                diffY = ()=>relativeView.Height;
            }
            else if (verticalDirection == VerticalPopupOptions.Center)
            {
                diffY = ()=>relativeView.Height / 2 - popupView.Height / 2 - popupView.Margin.Top;
            }

            m_slideUp = diffY() < 0;
            RelativeLayout.SetYConstraint(
                popupView,
                Constraint.RelativeToParent(r => Math.Max(0, Math.Min(r.Height - popupView.Height - sumMarginY, relativeView.GetY(layout) + diffY()))));
        }

        private async Task Animate(View popupView)
        {
            popupView.Opacity = 0.0;
            var fade = popupView.FadeTo(1.0, AnimationTime * 2);
            if (Animation == PopupAnimation.Slide)
            {
                if (Content == null) return;
                var height = Content.Height;
                if (m_slideUp)
                {
                    var y = popupView.Y;
                    popupView.Layout(new Rectangle(popupView.X, y + height, popupView.Width, 0));
                    await popupView.LayoutTo(new Rectangle(popupView.X, y, popupView.Width, height), AnimationTime);
                }
                else
                {
                    popupView.Layout(new Rectangle(popupView.X, popupView.Y, popupView.Width, 0));
                    await popupView.LayoutTo(new Rectangle(popupView.X, popupView.Y, popupView.Width, height), AnimationTime);
                }
            }

            await fade;
        }

        private async Task AnimateBack(View popupView)
        {
            if (Content == null) return;

            var fade = popupView.FadeTo(0.0, AnimationTime);
            if (Animation == PopupAnimation.Slide)
            {
                if (m_slideUp)
                {
                    var y = popupView.Y;
                    await popupView.LayoutTo(new Rectangle(popupView.X, y + Content.Height, popupView.Width, 0), AnimationTime);
                }
                else
                {
                    await popupView.LayoutTo(new Rectangle(popupView.X, popupView.Y, popupView.Width, 0), AnimationTime);
                }
            }

            await fade;
        }

        private void HidePopup()
        {
            if (m_attachedTo == null) return;

            var layout = m_attachedTo.GetParentOfType<ModalityLayout>();
            if (layout == null) throw new InvalidProgramException("Can't have a popup behavior without a ModalityLayout around the element");

            if (Content == null) return;

            layout.Hide(Content);
        }
    }

    /// <summary>
    ///     Horizontal location of the popup relative to the attached element.
    /// </summary>
    public enum HorizontalPopupOptions
    {
        /// <summary>
        ///     Left side of the popup is aligned with the left side of the attached element.
        /// </summary>
        LeftAlign,

        /// <summary>
        ///     Right side of the popup is aligned with the right side of the attached element.
        /// </summary>
        RightAlign,

        /// <summary>
        ///     Popup is centered above the attached element.
        /// </summary>
        Center,

        /// <summary>
        ///     Popup is placed to the left of the attached element.
        /// </summary>
        Left,

        /// <summary>
        ///     Popup is placed to the right of the attached element.
        /// </summary>
        Right
    }

    /// <summary>
    ///     Vertical orientation of the popup relative to the attached element.
    /// </summary>
    public enum VerticalPopupOptions
    {
        /// <summary>
        ///     Automatically selects above or below based on the attached element.
        /// </summary>
        Auto,

        /// <summary>
        ///     Popup is placed above the attached element.
        /// </summary>
        Above,

        /// <summary>
        ///     Popup is placed below the attached element.
        /// </summary>
        Below,

        /// <summary>
        ///     Popup is placed on top of the attached element.
        /// </summary>
        Center
    }

    /// <summary>
    ///     Directions of the popup
    /// </summary>
    [Obsolete("Use VerticalPopupOptions and HorizontalPopupOptions instead.")]
    public enum PopupDirection
    {
        /// <summary>
        ///     As PopupDirection is obsolete, setting Direction to None makes the layout placement
        ///     use HorizontalOptions/VerticalOptions.
        /// </summary>
        None,

        /// <summary>
        ///     Automatically based on the location on screen
        /// </summary>
        Auto,

        /// <summary>
        ///     Below the placement target
        /// </summary>
        Below,

        /// <summary>
        ///     Above the placement target
        /// </summary>
        Above
    }

    /// <summary>
    ///     Animations of the popup
    /// </summary>
    public enum PopupAnimation
    {
        /// <summary>
        ///     Instantly shows the popup
        /// </summary>
        None,

        /// <summary>
        ///     Slides from the element and outwards
        /// </summary>
        Slide,

        /// <summary>
        ///     Fading the popup in
        /// </summary>
        Fade
    }
}