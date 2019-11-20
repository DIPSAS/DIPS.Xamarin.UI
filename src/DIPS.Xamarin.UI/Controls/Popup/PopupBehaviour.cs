using System;
using System.Windows.Input;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Popup
{
    public class PopupBehaviour : Behavior<View>
    {
        private readonly Command m_onTappedCommand;
        private View? m_attachedTo;
        public PopupBehaviour()
        {
            m_onTappedCommand = new Command(ShowPopup);
        }

        protected override void OnAttachedTo(View bindable)
        {
            m_attachedTo = bindable;
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

        protected override void OnDetachingFrom(View bindable)
        {
            base.OnDetachingFrom(bindable);
        }

        public static readonly BindableProperty CloseOnClickProperty =
            BindableProperty.CreateAttached("CloseOnClick", typeof(bool), typeof(PopupBehaviour), false, propertyChanged: OnCloseOnClickChanged);

        public static void OnCloseOnClickChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = (View)bindable;
            view.BindingContextChanged += (s, e) =>
            {
                var popupLayout = view.GetParentOfType<PopupLayout>();
                if (popupLayout != null)
                {
                    popupLayout.AddOnCloseRecognizer(view);
                }
            };
        }

        public static void SetCloseOnClick(BindableObject view, bool value)
        {
            view.SetValue(CloseOnClickProperty, value);
        }

        public static bool GetCloseOnClick(BindableObject view)
        {
            return (bool)view.GetValue(CloseOnClickProperty);
        }

        public static readonly BindableProperty PopupBindingContextFactoryProperty =
            BindableProperty.Create(nameof(PopupBindingContextFactory), typeof(Func<object>), typeof(PopupBehaviour), null);

        public Func<object> PopupBindingContextFactory
        {
            get { return (Func<object>)GetValue(PopupBindingContextFactoryProperty); }
            set { SetValue(PopupBindingContextFactoryProperty, value); }
        }

        public static readonly BindableProperty SaveCommandProperty =
            BindableProperty.Create(nameof(SaveCommand), typeof(ICommand), typeof(PopupBehaviour), null);

        public ICommand SaveCommand
        {
            get { return (ICommand)GetValue(SaveCommandProperty); }
            set { SetValue(SaveCommandProperty, value); }
        }

        public static readonly BindableProperty SaveTextProperty =
            BindableProperty.Create(nameof(SaveText), typeof(string), typeof(PopupBehaviour), null);

        public string SaveText
        {
            get { return (string)GetValue(SaveTextProperty); }
            set { SetValue(SaveTextProperty, value); }
        }

        public static readonly BindableProperty DirectionProperty =
            BindableProperty.Create(nameof(Direction), typeof(PopupDirection), typeof(PopupBehaviour), PopupDirection.Auto);

        public PopupDirection Direction
        {
            get { return (PopupDirection)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }

        public static readonly BindableProperty PopupContentProperty =
            BindableProperty.Create(nameof(PopupContent), typeof(View), typeof(PopupBehaviour));

        public View PopupContent
        {
            get { return (View)GetValue(PopupContentProperty); }
            set { SetValue(PopupContentProperty, value); }
        }

        private void ShowPopup()
        {
            if (m_attachedTo == null)
            {
                return;
            }

            var layout = m_attachedTo.GetParentOfType<PopupLayout>();
            if (layout == null) throw new InvalidProgramException("Can't have a popup behavior without a PopupLayout around the element");
            var content = PopupContent;
            layout.ShowPopup(content, m_attachedTo, Direction);
            content.BindingContext = PopupBindingContextFactory?.Invoke() ?? BindingContext;
        }
    }

    public enum PopupDirection
    {
        Auto, Below, Above
    }
}
