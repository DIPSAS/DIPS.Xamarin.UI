using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Popup
{
    public partial class PopupLayout : RelativeLayout
    {
        private TapGestureRecognizer m_closePopupRecognizer;
        private View? m_content;
        private Lazy<Frame> m_blockingFrame;

        public PopupLayout()
        {
            InitializeComponent();
            m_closePopupRecognizer = new TapGestureRecognizer { Command = new Command(HidePopup) };
            m_blockingFrame = new Lazy<Frame>(CreateBlockingFrame);
        }

        public void ShowPopup(View popupView, View relativeView, PopupDirection popupDirection)
        {
            Children.Add(m_blockingFrame.Value,
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

            Children.Add(m_content = popupView,
                yConstraint: Constraint.RelativeToView(relativeView, (r, v) => v.Y + v.Height));

            var diffY = direction == PopupDirection.Below ? relativeView.Height : -popupView.Height;

            SetYConstraint(popupView, Constraint.RelativeToView(relativeView, (r, v) => v.Y + diffY));
            SetXConstraint(popupView, Constraint.RelativeToView(relativeView, (r, v) => Math.Max(0, Math.Min(r.Width - popupView.Width, v.X))));
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
            Children.Remove(m_blockingFrame.Value);
            if (m_content != null)
                Children.Remove(m_content);
            m_content = null;
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
