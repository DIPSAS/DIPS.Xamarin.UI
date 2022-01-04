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

            if (e.OldElement is SheetView oldElement)
            {
                oldElement.StateChanged -= OnSheetStateChanged;
            }

            if (e.NewElement is SheetView newElement)
            {
                newElement.StateChanged += OnSheetStateChanged;
            }

            var subviews = m_uiView.Subviews;

            ToggleScrollViews(subviews, false);
        }

        private void OnSheetStateChanged(object sender, SheetView.SheetState state)
        {
            var subviews = m_uiView.Subviews;
            
            ToggleScrollViews(subviews, state is SheetView.SheetState.Maximized);
        }

        private static void ToggleScrollViews(UIView[] subviews, bool scrollEnabled)
        {
            foreach (var view in subviews)
            {
                if (view is UIScrollView scrollView)
                {
                    scrollView.ScrollEnabled = scrollEnabled;

                    return;
                }

                ToggleScrollViews(view.Subviews, scrollEnabled);
            }
        }
    }
}