using DIPS.Xamarin.UI.Internal.Xaml.Sheet;
using DIPS.Xamarin.UI.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SheetView), typeof(PanAwareContentView))]

namespace DIPS.Xamarin.UI.iOS
{
    internal class PanAwareContentView : VisualElementRenderer<ContentView>
    {
        private UIView m_uiView;

        protected override void OnElementChanged(ElementChangedEventArgs<ContentView> e)
        {
            base.OnElementChanged(e);

            m_uiView = GetControl();

            var subviews = m_uiView.Subviews;

            foreach (var view in subviews)
            {
                if (view is UIScrollView scrollView)
                {
                    scrollView.ScrollEnabled = false;
                }
            }
        }
    }
}