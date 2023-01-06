using System;
using CoreGraphics;
using DIPS.Xamarin.UI.Controls;
using DIPS.Xamarin.UI.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SheetPage), typeof(SheetPageRenderer))]

namespace DIPS.Xamarin.UI.iOS
{
    public class SheetPageRenderer : PageRenderer
    {
        private SheetPage m_sheetPage;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (e.NewElement is SheetPage sheetPage)
                {
                    m_sheetPage = sheetPage;
                    m_sheetPage.SheetOpened += OpenSheet;
                }
            }
        }

        private void OpenSheet(object sender, EventArgs e)
        {
            var window= UIApplication.SharedApplication.KeyWindow;

            var currentViewController = window?.RootViewController;

            while (currentViewController?.PresentedViewController != null)
            {
                currentViewController = currentViewController.PresentedViewController;
            }
            
            var viewController = m_sheetPage.m_sheetPage.CreateViewController();

            viewController.ModalPresentationStyle = UIModalPresentationStyle.PageSheet;
            var sheet = viewController.SheetPresentationController;
            if (sheet != null)
            {
                sheet.Detents = new[]
                {
                    UISheetPresentationControllerDetent.CreateMediumDetent(),
                    UISheetPresentationControllerDetent.CreateLargeDetent()
                };
                sheet.SelectedDetentIdentifier = UISheetPresentationControllerDetentIdentifier.Medium;
                sheet.PrefersGrabberVisible = true;
                sheet.PrefersScrollingExpandsWhenScrolledToEdge = false;
                sheet.PrefersEdgeAttachedInCompactHeight = true;
                sheet.WidthFollowsPreferredContentSizeWhenEdgeAttached = true;

            }
            currentViewController.PresentViewController(viewController, true, null);
        }
    }

    public class MyViewController : UIViewController
    {
        private readonly UIView m_uiView;

        public MyViewController(UIView uiView)
        {
            m_uiView = uiView;
        }
        
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.View.AddSubview(m_uiView);
        }
    }
}