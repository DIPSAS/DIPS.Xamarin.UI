using System.Threading.Tasks;
using DIPS.Xamarin.UI.BottomSheet;
using UIKit;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.iOS.BottomSheet
{
    internal class iOSBottomSheet : IBottomSheet
    {
        public Task PushBottomSheet(ContentPage contentPage)
        {
            var window = UIApplication.SharedApplication.KeyWindow;

            var currentViewController = window?.RootViewController;

            while (currentViewController?.PresentedViewController != null)
            {
                currentViewController = currentViewController.PresentedViewController;
            }

            if (currentViewController == null) return Task.CompletedTask;

            var viewController = contentPage.CreateViewController();


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
            }

            return currentViewController.PresentViewControllerAsync(viewController, true);
        }
    }
}