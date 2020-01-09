using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.Popup
{
    /// <summary>
    /// Layout used to add content showing popups
    /// </summary>
    [ContentProperty(nameof(MainContent))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [ExcludeFromCodeCoverage]
    public partial class PopupLayout : ContentView
    {
        private readonly TapGestureRecognizer m_closePopupRecognizer;
        private readonly Lazy<Frame> m_blockingFrame;
        private const int m_animationTime = 100;

        private PopupBehavior? m_popupBehavior;
        private Task? m_animation;
        private View? m_content;
        private bool m_slideUp;

        /// <summary>
        /// Create an instance
        /// </summary>
        public PopupLayout()
        {
            InitializeComponent();
            m_closePopupRecognizer = new TapGestureRecognizer { Command = new Command(HidePopup) };
            m_blockingFrame = new Lazy<Frame>(CreateBlockingFrame);
        }

        /// <summary>
        /// <see cref="MainContent" />
        /// </summary>
        public static readonly BindableProperty MainContentProperty =
            BindableProperty.Create(nameof(MainContent), typeof(View), typeof(PopupLayout), propertyChanged: OnMainContentPropertyChanged);

        /// <summary>
        /// Main Content of the layout. This is routed from the Content property, so you don't have to use it.
        /// </summary>
        public View MainContent
        {
            get { return (View)GetValue(MainContentProperty); }
            set { SetValue(MainContentProperty, value); }
        }

        private static void OnMainContentPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (!(bindable is PopupLayout popupLayout)) return;
            if (!(newvalue is View newView)) return;
            if (newvalue == popupLayout.relativeLayout)
            {
                popupLayout.Content = popupLayout.relativeLayout;
            }
            else
            {
                popupLayout.relativeLayout.Children.Clear();
                popupLayout.relativeLayout.Children.Add(newView, Constraint.RelativeToParent(parent => parent.X), Constraint.RelativeToParent(parent => parent.Y), Constraint.RelativeToParent(parent => parent.Width), Constraint.RelativeToParent(parent => parent.Height));
            }
        }
        
        internal void ShowPopup(View popupView, View relativeView, PopupBehavior behavior)
        {
            var prevAnimation = m_animation;
            if (prevAnimation != null && !prevAnimation.IsCompleted && !prevAnimation.IsCanceled)
            {
                return;
            }

            m_animation = null;
            m_popupBehavior = behavior;
            behavior.IsOpen = true;

            relativeLayout.Children.Add(m_blockingFrame.Value,
                widthConstraint: Constraint.RelativeToParent(r => r.Width),
                heightConstraint: Constraint.RelativeToParent(r => r.Height),
                xConstraint: Constraint.RelativeToParent(r => 0.0),
                yConstraint: Constraint.RelativeToParent(r => 0.0));

            relativeLayout.Children.Add(m_content = popupView,
                yConstraint: Constraint.RelativeToParent((r) => relativeView.GetY(this) + relativeView.Height));

            if (behavior.Direction != PopupDirection.None)
            {
                PlaceItemLegacy(popupView, relativeView, behavior);
            }
            else
            {
                PlaceItem(popupView, relativeView, behavior);
            }

            m_animation = Animate(popupView, popupView.Height, behavior, m_slideUp);
        }

        private void PlaceItem(View popupView, View relativeView, PopupBehavior behavior)
        {
            var sumMarginX = popupView.Margin.Left + popupView.Margin.Right;
            switch (behavior.HorizontalOptions)
            {
                case HorizontalPopupOptions.LeftAlign:
                    RelativeLayout.SetXConstraint(popupView, Constraint.RelativeToParent((r) => Math.Max(0, Math.Min(r.Width - popupView.Width - sumMarginX, relativeView.GetX(this)))));
                    break;
                case HorizontalPopupOptions.RightAlign:
                    RelativeLayout.SetXConstraint(popupView, Constraint.RelativeToParent((r) => Math.Max(0, Math.Min(r.Width - popupView.Width - sumMarginX, relativeView.GetX(this) + relativeView.Width - popupView.Width - sumMarginX))));
                    break;
                case HorizontalPopupOptions.Center:
                    RelativeLayout.SetXConstraint(popupView, Constraint.RelativeToParent((r) => Math.Max(0, Math.Min(r.Width - popupView.Width - sumMarginX, relativeView.GetX(this) + relativeView.Width / 2 - popupView.Width / 2 - popupView.Margin.Left))));
                    break;
                case HorizontalPopupOptions.Left:
                    RelativeLayout.SetXConstraint(popupView, Constraint.RelativeToParent((r) => Math.Max(0, Math.Min(r.Width - popupView.Width - sumMarginX, relativeView.GetX(this) - popupView.Width - sumMarginX))));
                    break;
                case HorizontalPopupOptions.Right:
                    RelativeLayout.SetXConstraint(popupView, Constraint.RelativeToParent((r) => Math.Max(0, Math.Min(r.Width - popupView.Width - sumMarginX, relativeView.GetX(this) + relativeView.Width))));
                    break;
            }

            var sumMarginY = popupView.Margin.Top + popupView.Margin.Bottom;
            var verticalDirection = behavior.VerticalOptions;
            if (behavior.VerticalOptions == VerticalPopupOptions.Auto)
            {
                var height = Height;
                var center = height / 2.0;
                var itemPosition = relativeView.GetY(this) + relativeView.Height / 2.0;
                if (itemPosition > center)
                {
                    verticalDirection = VerticalPopupOptions.Above;
                }
                else
                {
                    verticalDirection = VerticalPopupOptions.Below;
                }
            }

            var diffY = 0.0;

            if (verticalDirection == VerticalPopupOptions.Above)
            {
                diffY = -popupView.Height - sumMarginY;
            }
            else if (verticalDirection == VerticalPopupOptions.Below)
            {
                diffY = relativeView.Height;
            }
            else if (verticalDirection == VerticalPopupOptions.Center)
            {
                diffY = relativeView.Height/2 -popupView.Height/2-popupView.Margin.Top;
            }

            m_slideUp = diffY < 0;
            RelativeLayout.SetYConstraint(popupView, Constraint.RelativeToParent((r) => Math.Max(0, Math.Min(r.Height - popupView.Height - sumMarginY, relativeView.GetY(this) + diffY))));
        }

        [Obsolete]
        private void PlaceItemLegacy(View popupView, View relativeView, PopupBehavior behavior)
        {
            var sumMarginY = popupView.Margin.Top + popupView.Margin.Bottom;
            var sumMarginX = popupView.Margin.Left + popupView.Margin.Right;

            var direction = behavior.Direction;
            if (direction == PopupDirection.Auto)
            {
                var height = Height;
                var center = height / 2.0;
                var itemPosition = relativeView.GetY(this) + relativeView.Height / 2.0;
                if (itemPosition > center)
                {
                    direction = PopupDirection.Above;
                }
                else
                {
                    direction = PopupDirection.Below;
                }
            }

            var diffY = direction == PopupDirection.Below ? relativeView.Height : (-popupView.Height - sumMarginY);
            m_slideUp = diffY < 0;

            RelativeLayout.SetYConstraint(popupView, Constraint.RelativeToParent((r) => Math.Max(0, Math.Min(r.Height - popupView.Height - sumMarginY, relativeView.GetY(this) + diffY))));
            RelativeLayout.SetXConstraint(popupView, Constraint.RelativeToParent((r) => Math.Max(0, Math.Min(r.Width - popupView.Width - sumMarginX, relativeView.GetX(this)))));
        }

        private async Task Animate(View popupView, double height, PopupBehavior behavior, bool slideUp)
        {
            popupView.Opacity = 0.0;
            var fade = popupView.FadeTo(1.0, m_animationTime * 2);
            if(behavior.Animation == PopupAnimation.Slide)
            {
                if(slideUp)
                {
                    var y = popupView.Y;
                    popupView.Layout(new Rectangle(popupView.X, y + height, popupView.Width, 0));
                    await popupView.LayoutTo(new Rectangle(popupView.X, y, popupView.Width, height), m_animationTime);
                }
                else
                {
                    popupView.Layout(new Rectangle(popupView.X, popupView.Y, popupView.Width, 0));
                    await popupView.LayoutTo(new Rectangle(popupView.X, popupView.Y, popupView.Width, height), m_animationTime);
                }
            }

            await fade;
        }

        private async Task AnimateBack(View popupView, double height, PopupBehavior behavior, bool slideUp)
        {
            var fade = popupView.FadeTo(0.0, m_animationTime);
            if (behavior.Animation == PopupAnimation.Slide)
            {
                if (slideUp)
                {
                    var y = popupView.Y;
                    await popupView.LayoutTo(new Rectangle(popupView.X, y + height, popupView.Width, 0), m_animationTime);
                }
                else
                {
                    await popupView.LayoutTo(new Rectangle(popupView.X, popupView.Y, popupView.Width, 0), m_animationTime);
                }
            }

            await fade;
        }

        internal async void HidePopup()
        {
            var behavior = m_popupBehavior;
            var content = m_content;
            relativeLayout.Children.Remove(m_blockingFrame.Value);

            if (behavior != null)
            {
                behavior.IsOpen = false;
            }

            if (behavior != null && content != null)
            {
                var t = m_animation = AnimateBack(content, content.Height, behavior, m_slideUp);
                await Task.Delay(m_animationTime);
                await t;
            }

            if (content != null)
            {
                relativeLayout.Children.Remove(content);
            }
        }

        internal void AddOnCloseRecognizer(View view)
        {
            if (view is Button button)
            {
                button.Clicked -= OnClicked;
                button.Clicked += OnClicked;
            }
            else
            {
                view.GestureRecognizers.Remove(m_closePopupRecognizer);
                view.GestureRecognizers.Add(m_closePopupRecognizer);
            }
        }

        private void OnClicked(object sender, EventArgs args)
        {
            HidePopup();
        }

        private Frame CreateBlockingFrame()
        {
            var background = new Frame
            {
                BackgroundColor = Color.Gray,
                IsVisible = true,
                Opacity = 0.5
            };

            background.GestureRecognizers.Add(m_closePopupRecognizer);
            return background;
        }
    }
}
