using System.Linq;
using DIPS.Xamarin.UI.Controls.ContextMenu;
using DIPS.Xamarin.UI.iOS.ContextMenu;
using DIPS.Xamarin.UI.Pages;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContextMenuContentPage), typeof(ContextMenuContentPageRenderer))]

namespace DIPS.Xamarin.UI.iOS.ContextMenu
{
    public class ContextMenuContentPageRenderer : PageRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            AddContextMenuToolbarItem();
        }

        private void AddContextMenuToolbarItem()
        {
            if (Element is ContextMenuContentPage contextMenuContentPage)
            {
                var contextMenuButton = contextMenuContentPage.ContextMenuToolbarButton;
                if (NavigationController != null && contextMenuButton != null)
                {
                    var uiElements = ContextMenuHelper.CreateMenuItems(contextMenuButton.ItemsSource, contextMenuButton)
                        .Select(k => k.Value).ToArray();
                    var item = new UIBarButtonItem()
                    {
                        Title = contextMenuButton.Title, Menu = UIMenu.Create(uiElements), PrimaryAction = null
                    };
                    NavigationController.TopViewController.NavigationItem.RightBarButtonItems = new[]
                    {
                       item
                    };
                }
            }
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            RemovedContextItemsNotVisible();
        }

        private void RemovedContextItemsNotVisible()
        {
            if (Element is ContextMenuContentPage contextMenuContentPage)
            {
                var contextMenuButton = contextMenuContentPage.ContextMenuToolbarButton;
                if (NavigationController != null && contextMenuButton != null)
                {
                    
                }
            }
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
        }
    }
}