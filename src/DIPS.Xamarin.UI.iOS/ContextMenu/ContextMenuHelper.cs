using System.Collections.Generic;
using System.Linq;
using DIPS.Xamarin.UI.Controls.ContextMenu;
using UIKit;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;

namespace DIPS.Xamarin.UI.iOS.ContextMenu
{
    internal static class ContextMenuHelper
    {
        internal static Dictionary<ContextMenuItem, UIMenuElement> CreateMenuItems(IEnumerable<ContextMenuItem> contextMenuItems,
            ContextMenuButton contextMenuButton)
        {
            var dict = new Dictionary<ContextMenuItem, UIMenuElement>();
            foreach (var contextMenuItem in contextMenuItems)
            {
                UIMenuElement uiMenuElement;
                if (contextMenuItem is ContextMenuGroup contextMenuGroup)
                {
                    var uiElements = CreateMenuItems(contextMenuGroup.Children, contextMenuButton).Select(k => k.Value)
                        .ToArray();
                    uiMenuElement = UIMenu.Create(contextMenuGroup.Title,uiElements);
                }
                else
                {
                    uiMenuElement = UIAction.Create(contextMenuItem.Title, null, null, (args) =>
                    {
                        contextMenuItem.SendClicked(contextMenuButton);
                    });
                }
                dict.Add(contextMenuItem, uiMenuElement);
            }

            return dict;
        }
    }
}