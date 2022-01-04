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

            var uiViewSubviews = m_uiView.Subviews;
        }
    }
}