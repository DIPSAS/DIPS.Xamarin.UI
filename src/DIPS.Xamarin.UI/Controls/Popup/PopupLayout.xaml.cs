using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.Popup
{
    [ContentProperty(nameof(MainContent))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupLayout : ContentView
    {
        private TapGestureRecognizer m_closePopupRecognizer;
        private View? m_content;
        private Lazy<Frame> m_blockingFrame;
        private ContentView m_dummyView = new ContentView() { IsVisible = false, };

        public PopupLayout()
        {
            InitializeComponent();
            m_closePopupRecognizer = new TapGestureRecognizer { Command = new Command(HidePopup) };
            m_blockingFrame = new Lazy<Frame>(CreateBlockingFrame);
        }

        public static readonly BindableProperty MainContentProperty =
            BindableProperty.Create(nameof(MainContent), typeof(View), typeof(PopupLayout), propertyChanged: OnMainContentPropertyChanged);

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

        public void ShowPopup(View popupView, View relativeView, PopupDirection popupDirection)
        {
            relativeLayout.Children.Add(m_blockingFrame.Value,
                widthConstraint: Constraint.RelativeToParent(r => r.Width),
                heightConstraint: Constraint.RelativeToParent(r => r.Height),
                xConstraint: Constraint.RelativeToParent(r => 0.0),
                yConstraint: Constraint.RelativeToParent(r => 0.0));

            var direction = popupDirection;
            if (direction == PopupDirection.Auto)
            {
                var height = Height;
                var center = height / 2.0;
                var itemPosition = GetY(relativeView);
                if (itemPosition > center) direction = PopupDirection.Above;
                else direction = PopupDirection.Below;
            }

            var x = GetX(relativeView);
            var y = GetY(relativeView);

            relativeLayout.Children.Add(m_dummyView,
                yConstraint: Constraint.RelativeToParent((r) => y),
                xConstraint: Constraint.RelativeToParent((r) => x),
                widthConstraint: Constraint.RelativeToParent((r) => relativeView.Width),
                heightConstraint: Constraint.RelativeToParent((r) => relativeView.Height));

            relativeLayout.Children.Add(m_content = popupView,
                yConstraint: Constraint.RelativeToView(m_dummyView, (r, v) => v.Y + v.Height));

            var diffY = direction == PopupDirection.Below ? relativeView.Height : -popupView.Height;

            RelativeLayout.SetYConstraint(popupView, Constraint.RelativeToView(m_dummyView, (r, v) => v.Y + diffY));
            RelativeLayout.SetXConstraint(popupView, Constraint.RelativeToView(m_dummyView, (r, v) => Math.Max(0, Math.Min(r.Width - popupView.Width, v.X))));
        }

        private double GetX(View item)
        {
            var x = item.X;
            var parent = (View)item.Parent;
            while (parent != this && parent != null)
            {
                x += parent.X;
                parent = (View)parent.Parent;
            }

            return x;
        }

        private double GetY(View item)
        {
            var y = item.Y;
            var parent = (View)item.Parent;
            while (parent != this && parent != null)
            {
                y += parent.Y;
                parent = (View)parent.Parent;
            }

            return y;
        }

        public void HidePopup()
        {

            relativeLayout.Children.Remove(m_blockingFrame.Value);
            if (m_content != null)
            {
                relativeLayout.Children.Remove(m_content);
            }
            m_content = null;
            relativeLayout.Children.Remove(m_dummyView);
        }

        public void AddOnCloseRecognizer(View view)
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
