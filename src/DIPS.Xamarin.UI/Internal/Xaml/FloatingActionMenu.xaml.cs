using System;
using System.Collections.Generic;
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
    public partial class FloatingActionMenu : ContentView, IModalityHandler
    {
        private readonly double m_spaceBetween = 10;
        private bool m_animationComplete = true;
        private bool m_first = true;
        private bool m_isExpanded;
        private ModalityLayout? m_parent;
        private double m_yTranslate;
        internal readonly FloatingActionMenuBehaviour m_behaviour;

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

        internal void ShowMenu(bool shouldShow)
        {
            if (m_animationComplete && m_isExpanded != shouldShow)
            {
                AdjustXPositions();
                AnimateAll();
            }
        }

        private void DisplayOverlay()
        {
            m_parent?.Show(this, Children.First());
        }

        private async Task AnimateAll()
        {
            m_animationComplete = false;

            if (!m_isExpanded)
            {
                DisplayOverlay();
            }
            else
            {
                HideOverlay();
            }

            var multiplier = 1;
            foreach (var menuButton in Children)
            {
                var maxOpacity = menuButton.IsEnabled ? 1 : .5;
                menuButton.TranslateTo(0, m_isExpanded ? 0 : -m_yTranslate * multiplier, 250, Easing.CubicInOut);
                menuButton.FadeTo(m_isExpanded ? 0 : maxOpacity, 250, Easing.CubicInOut);
                multiplier += 1;
            }

            if (m_isExpanded)
            {
                ExpandButton.FadeTo(.5, 250, Easing.CubicInOut);
            }
            else
            {
                ExpandButton.FadeTo(1, 250, Easing.CubicInOut);
            }
            await ExpandButton.RelRotateTo(180, 250, Easing.CubicInOut);
            m_isExpanded = !m_isExpanded;
            m_animationComplete = true;
        }

        private void HideOverlay()
        {
            m_parent?.HideOverlay();
        }

        private async void ExpandButton_OnClicked(object sender, EventArgs e)
        {
            if (!m_animationComplete) return;
            AdjustXPositions();
            await AnimateAll();

            m_behaviour.IsOpen = m_isExpanded;
            Children.ForEach(mb => mb.InputTransparent = !m_isExpanded);
        }

        internal void AddTo(ModalityLayout layout)
        {
            Children = m_behaviour.Children;
            ExpandButton.BindingContext = m_behaviour;

            AddButtonsToRelative(layout.relativeLayout);

            m_parent = layout;
            m_yTranslate = m_behaviour.Size + m_spaceBetween;
        }

        private void AddButtonsToRelative(RelativeLayout parent)
        {
            foreach (var child in Children)
            {
                child.FloatingActionMenuParent = this;
                child.button.WidthRequest = m_behaviour.Size;
                child.button.HeightRequest = m_behaviour.Size;
                child.button.CornerRadius = (int)m_behaviour.Size / 2;
                parent.Children.Add(
                    child,
                    Constraint.RelativeToParent(p => p.Width * m_behaviour.XPosition),
                    Constraint.RelativeToParent(p => (p.Height * m_behaviour.YPosition) - ExpandButton.HeightRequest));
            }

            parent.Children.Add(
                ExpandButton,
                Constraint.RelativeToParent(p => (p.Width * m_behaviour.XPosition) - ExpandButton.WidthRequest),
                Constraint.RelativeToParent(p => (p.Height * m_behaviour.YPosition) - ExpandButton.HeightRequest));
        }

        private void AdjustXPositions()
        {
            if (!m_first) return;

            foreach (var child in Children)
            {
                RelativeLayout.SetXConstraint(child, Constraint.Constant(ExpandButton.X + m_behaviour.Size - child.Width));
            }

            m_first = false;
        }

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
    }
}