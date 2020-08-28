using System;
using DIPS.Xamarin.UI.Controls.Slidable;
using DIPS.Xamarin.UI.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SlidableLayout), typeof(SlidableLayoutContentView))]

namespace DIPS.Xamarin.UI.iOS
{
    internal class SlidableLayoutContentView : VisualElementRenderer<ContentView>
    {
        private SlidableLayout m_elem;
        private UIView m_uiView;

        protected override void OnElementChanged(ElementChangedEventArgs<ContentView> e)
        {
            base.OnElementChanged(e);

            if (Element is SlidableLayout element) m_elem = element;

            m_uiView = GetControl();
            m_uiView?.AddGestureRecognizer(new UITapGestureRecognizer(OnTap){CancelsTouchesInView = true, ShouldRecognizeSimultaneously = ShouldRecognizeSimultaneously});
        }

        private bool ShouldRecognizeSimultaneously(UIGestureRecognizer target, UIGestureRecognizer other)
        {
            return true;
        }

        private void OnTap(UITapGestureRecognizer recognizer)
        {
            var superView = UIApplication.SharedApplication.KeyWindow.RootViewController.View;
            var point = recognizer.LocationInView(m_uiView);
            var pointOnScreen = m_uiView.ConvertPointToView(point, superView);
            m_elem.SendTapped((float)pointOnScreen.X, (float)pointOnScreen.Y);
        }
    }
}