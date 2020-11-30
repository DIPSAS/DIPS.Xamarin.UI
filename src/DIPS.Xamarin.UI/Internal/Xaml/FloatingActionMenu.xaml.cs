using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Controls.FloatingActionMenu;
using DIPS.Xamarin.UI.Controls.Modality;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Internal.Xaml
{
    /// <summary>
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [ContentProperty(nameof(Children))]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public partial class FloatingActionMenu : ContentView, IModalityHandler
    {
        /// <summary>
        ///     <see cref="CloseOnOverlayTapped" />
        /// </summary>
        public static readonly BindableProperty CloseOnOverlayTappedProperty = BindableProperty.Create(
            nameof(CloseOnOverlayTapped),
            typeof(bool),
            typeof(FloatingActionMenu),
            true);

        internal readonly FloatingActionMenuBehaviour m_behaviour;
        private readonly double m_spaceBetween = 10;
        private bool m_animationComplete = true;
        private bool m_first = true;
        private bool m_isExpanded;
        private ModalityLayout? m_parent;
        private double m_yTranslate;

        /// <summary>
        ///     A floating action menu that can be added to either a RelativeLayout or an AbsoluteLayout. Add
        ///     <see cref="MenuButton" />.
        ///     as content.
        /// </summary>
        public FloatingActionMenu(FloatingActionMenuBehaviour behaviour)
        {
            m_behaviour = behaviour;
            Children = new List<MenuButton>();
            InitializeComponent();
        }

        private new List<MenuButton> Children { get; set; }

        /// <inheritdoc />
        public void Hide()
        {
            m_behaviour.IsOpen = false;
        }

        /// <inheritdoc />
        public Task BeforeRemoval()
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task AfterRemoval()
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public bool CloseOnOverlayTapped
        {
            get => (bool)GetValue(CloseOnOverlayTappedProperty);
            set => SetValue(CloseOnOverlayTappedProperty, value);
        }

        /// <summary>
        ///     Raise before opening animation starts.
        /// </summary>
        internal event EventHandler OnBeforeOpen;

        /// <summary>
        ///     Raised after opening animation completes.
        /// </summary>
        internal event EventHandler OnAfterOpen;

        /// <summary>
        ///     Raised before closing animation starts.
        /// </summary>
        internal event EventHandler OnBeforeClose;

        /// <summary>
        ///     Raised after closing animation completes.
        /// </summary>
        internal event EventHandler OnAfterClose;

        internal void ShowMenu(bool shouldShow)
        {
            if (m_animationComplete && m_isExpanded != shouldShow)
            {
                AnimateAll();
            }
        }

        private void DisplayOverlay()
        {
            m_parent?.Show(this, ExpandButton);
        }

        private async Task AnimateAll()
        {
            m_animationComplete = false;

            m_isExpanded = !m_isExpanded;
            m_behaviour.IsOpen = m_isExpanded;

            InvokeBeforeEvents();

            if (m_isExpanded)
            {
                DisplayOverlay();
            }
            else
            {
                HideOverlay();
            }

            var position = 0;
            foreach (var menuButton in Children.Where(menuButton => menuButton.IsVisible))
            {
                TranslateMenuButton(menuButton, position, !m_isExpanded);
                position++;
            }

            ExpandButton.FadeTo(!m_isExpanded ? .5 : 1, 250, Easing.CubicInOut);

            var rotateTask = ExpandButton.RelRotateTo(180, 250, Easing.CubicInOut);
            await Task.Delay(250);
            await rotateTask;

            InvokeAfterEvents();

            m_animationComplete = true;
        }

        private void InvokeAfterEvents()
        {
            if (!m_isExpanded)
            {
                OnAfterClose?.Invoke(null, EventArgs.Empty);
                m_behaviour.OnAfterCloseCommand?.Execute(m_behaviour.OnAfterCloseCommandParameter);
            }
            else
            {
                OnAfterOpen?.Invoke(null, EventArgs.Empty);
                m_behaviour.OnAfterOpenCommand?.Execute(m_behaviour.OnAfterOpenCommandParameter);
            }
        }

        private void InvokeBeforeEvents()
        {
            if (!m_isExpanded)
            {
                OnBeforeClose?.Invoke(null, EventArgs.Empty);
                m_behaviour.OnBeforeCloseCommand?.Execute(m_behaviour.OnBeforeCloseCommandParameter);
            }
            else
            {
                OnBeforeOpen?.Invoke(null, EventArgs.Empty);
                m_behaviour.OnBeforeOpenCommand?.Execute(m_behaviour.OnBeforeOpenCommandParameter);
            }
        }

        private void TranslateMenuButton(MenuButton menuButton, int position, bool hide)
        {
            menuButton.TranslateTo(0, hide ? 0 : -m_yTranslate * (position + 1), 250, Easing.CubicInOut);
            menuButton.BadgeFrame.FadeTo(hide ? .5 : .95, 250, Easing.CubicInOut);
            AnimateFade(menuButton, hide, 250);
        }

        private void ToggleMenuButtonVisibility(MenuButton menuButton, int position, bool hide)
        {
            menuButton.BadgeFrame.FadeTo(hide ? 0 : .95, 150, Easing.CubicInOut);
            if (Library.PreviewFeatures.MenuButtonAnimations)
            {
                menuButton.TranslateTo(!hide ? 0 : m_behaviour.XPosition <= .5 ? -m_parent.Width : m_parent.Width, -m_yTranslate * (position + 1), 150, Easing.CubicInOut);
                AnimateFade(menuButton, hide, 150);
            }
            else
            {
                menuButton.TranslationY = -m_yTranslate * (position + 1);
                AnimateFade(menuButton, hide, 150);
            }
        }

        private void AnimateFade(MenuButton menuButton, bool hide, uint animationTime)
        {
            var maxOpacity = menuButton.IsEnabled ? 1 : .5;
            menuButton.Button.FadeTo(hide ? 0 : maxOpacity, animationTime, Easing.CubicInOut);
            menuButton.imageButton.FadeTo(hide ? 0 : maxOpacity, animationTime, Easing.CubicInOut);
            menuButton.TitleFrame.FadeTo(hide ? 0 : 1, animationTime, Easing.CubicInOut);
            menuButton.InputTransparent = hide;
        }

        private void HideOverlay()
        {
            m_parent?.HideOverlay();
        }

        private async void ExpandButton_OnClicked(object sender, EventArgs e)
        {
            if (!m_animationComplete) return;
            await AnimateAll();
        }

        internal void AddTo(ModalityLayout layout)
        {
            Children = m_behaviour.Children;
            ExpandButton.BindingContext = m_behaviour;

            AddButtonsToLayout(layout.relativeLayout);

            m_parent = layout;
            m_yTranslate = m_behaviour.Size + m_spaceBetween;
        }

        private void AddButtonsToLayout(RelativeLayout parent)
        {
            parent.Children.Add(
                ExpandButton,
                Constraint.RelativeToParent(p => p.Width * m_behaviour.XPosition - ExpandButton.WidthRequest),
                Constraint.RelativeToParent(p => p.Height * m_behaviour.YPosition - ExpandButton.HeightRequest));

            for (var index = Children.Count - 1; index >= 0; index--)
            {
                var child = Children[index];

                SetButtonSize(child);

                parent.Children.Add(
                    child,
                    Constraint.RelativeToParent(p => p.Width * m_behaviour.XPosition),
                    Constraint.RelativeToParent(p => p.Height * m_behaviour.YPosition - ExpandButton.HeightRequest));
            }

            AdjustXPositions();
        }

        private void SetButtonSize(MenuButton child)
        {
            child.FloatingActionMenuParent = this;

            child.Button.WidthRequest = m_behaviour.Size;
            child.Button.HeightRequest = m_behaviour.Size;
            child.Button.CornerRadius = (int)m_behaviour.Size / 2;

            child.imageButton.WidthRequest = m_behaviour.Size;
            child.imageButton.HeightRequest = m_behaviour.Size;
            child.imageButton.CornerRadius = (int)m_behaviour.Size / 2;
        }

        private void AdjustXPositions()
        {
            if (!m_first) return;

            foreach (var child in Children)
            {
                RelativeLayout.SetXConstraint(child, Constraint.RelativeToParent(p => ExpandButton.X + m_behaviour.Size - child.Width));
            }

            m_first = false;
        }

        internal void UpdateMenuButtonVisibility(MenuButton button, bool newvalue)
        {
            if (!m_isExpanded) return;

            var position = Children.FindIndex(mb => mb == button);
            for (var i = position + 1; i < Children.Count; i++)
            {
                Children[i].TranslateTo(0, newvalue ? -m_yTranslate * (i + 1) : -m_yTranslate * i, 150, Easing.CubicInOut);
            }

            ToggleMenuButtonVisibility(button, position, !newvalue);
        }
    }
}