using System;
using System.Windows.Input;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Popup
{
    /// <summary>
    /// Behavior to be added to an item you want to open a popup from. This item _has_ to be inside a PopupLayout somehow. Else, nothing would happend.
    /// </summary>
    [ContentProperty(nameof(Content))]
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
                button.Command = m_onTappedCommand;
            }
            else
            {
                bindable.GestureRecognizers.Add(new TapGestureRecognizer { Command = m_onTappedCommand });
            }

            base.OnAttachedTo(bindable);
        }

        /// <summary>
        /// Handles detaching to item
        /// </summary>
        /// <param name="bindable"></param>
        protected override void OnDetachingFrom(View bindable)
        {
            base.OnDetachingFrom(bindable);
        }

        /// <summary>
        /// <see cref="BindingContextFactory" />
        /// </summary>
        public static readonly BindableProperty BindingContextFactoryProperty =
            BindableProperty.Create(nameof(BindingContextFactory), typeof(Func<object>), typeof(PopupBehavior), null);

        /// <summary>
        /// A func which should return the requested viewModel for the popup when it opens.
        /// Can be used to create a new instance of a filter, and have the save command send it back with CommandParameter="{Binding}".
        /// Removing unwanted state from your viewModel
        /// </summary>
        public Func<object> BindingContextFactory
        {
            get { return (Func<object>)GetValue(BindingContextFactoryProperty); }
            set { SetValue(BindingContextFactoryProperty, value); }
        }

        /// <summary>
        /// <see cref="Direction" />
        /// </summary>
        public static readonly BindableProperty DirectionProperty =
            BindableProperty.Create(nameof(Direction), typeof(PopupDirection), typeof(PopupBehavior), PopupDirection.Auto);

        /// <summary>
        /// Direction of where the popup will show, auto is default
        /// </summary>
        public PopupDirection Direction
        {
            get { return (PopupDirection)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
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

        private void ShowPopup()
        {
            if (m_attachedTo == null)
            {
                return;
            }

            var layout = m_attachedTo.GetParentOfType<PopupLayout>();
            if (layout == null) throw new InvalidProgramException("Can't have a popup behavior without a PopupLayout around the element");
            var content = Content;
            layout.ShowPopup(content, m_attachedTo, Direction);
            content.BindingContext = BindingContextFactory?.Invoke() ?? BindingContext;
        }
    }

    /// <summary>
    /// Directions of the popup
    /// </summary>
    public enum PopupDirection
    {
        /// <summary>
        /// Automatically based on the location at the screen
        /// </summary>
        Auto,
        /// <summary>
        /// Below the placement target
        /// </summary>
        Below,
        /// <summary>
        /// Above the placement target
        /// </summary>
        Above
    }
}
