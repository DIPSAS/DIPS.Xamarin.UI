using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using DIPS.Xamarin.UI.Controls.ContextMenu;
using DIPS.Xamarin.UI.Extensions;
using UIKit;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;

namespace DIPS.Xamarin.UI.iOS.ContextMenu
{
    internal static class ContextMenuHelper
    {
        internal static Dictionary<ContextMenuItem, UIMenuElement> CreateMenuItems(
            IEnumerable<ContextMenuItem> contextMenuItems,
            ContextMenuButton contextMenuButton, ContextMenuGroup menuGroup = null)
        {
            var dict = new Dictionary<ContextMenuItem, UIMenuElement>();
            var items = contextMenuItems.ToArray();
            foreach (var contextMenuItem in items)
            {
                UIMenuElement uiMenuElement;
                if (contextMenuItem is ContextMenuGroup contextMenuGroup) //Recursively add menu items from a group
                {
                    contextMenuGroup.Parent = contextMenuButton;
                    //Inherit isCheckable context menu group group to all menu items in the group
                    contextMenuGroup.ItemsSource.ForEach(c => c.IsCheckable = contextMenuGroup.IsCheckable);

                    var newDict = CreateMenuItems(contextMenuGroup.ItemsSource, contextMenuButton, contextMenuGroup);
                    if (items.Count(i => i is ContextMenuGroup) >
                        1) //If there is more than one group, add the group title and group the items
                    {
                        uiMenuElement = UIMenu.Create(contextMenuGroup.Title, newDict.Select(k => k.Value).ToArray());
                    }
                    else //Only one group, add this to the root of the menu so the user does not have to tap an extra time to get to the items.
                    {
                        newDict.ForEach(newD =>
                        {
                            dict.Add(newD.Key, newD.Value);
                        });
                        continue;
                    }
                }

                else
                {
                    UIImage image = null;
                    if(!string.IsNullOrEmpty(contextMenuItem.iOSOptions.SystemIconName))
                    {
                        image = UIImage.GetSystemImage(contextMenuItem.iOSOptions.SystemIconName);
                    }
                    var uiAction = UIAction.Create(contextMenuItem.Title, image, null,
                        uiAction => OnMenuItemClick(uiAction, contextMenuItem, contextMenuButton));

                    if (contextMenuItem.IsChecked)
                    {
                        contextMenuButton.ResetIsCheckedForTheRest(contextMenuItem);
                    }

                    uiAction.Subtitle = contextMenuItem.Subtitle;

                    SetCorrectUiActionState(contextMenuItem, uiAction); //Setting the correct check mark if it can

                    uiMenuElement = uiAction;

                    if (menuGroup != null)
                    {
                        contextMenuItem.Parent = menuGroup;
                    }
                    else
                    {
                        contextMenuItem.Parent = contextMenuButton;
                    }
                }

                dict.Add(contextMenuItem, uiMenuElement);
            }

            return dict;
        }

        private static void OnMenuItemClick(UIAction action, ContextMenuItem contextMenuItem,
            ContextMenuButton contextMenuButton)
        {
            
            if (contextMenuItem.IsCheckable)
            {
                if (contextMenuItem.Parent is not ContextMenuGroup || !contextMenuItem.IsChecked)
                { //Only check if if unchecked if its a part of a group
                    contextMenuButton.ResetIsCheckedForTheRest(contextMenuItem);

                    contextMenuItem.IsChecked =
                        !contextMenuItem
                            .IsChecked; //Can not change the visuals when the menu is showing as the items are immutable when they are showing    
                }
            }

            contextMenuItem.SendClicked(contextMenuButton);
        }

        private static void SetCorrectUiActionState(ContextMenuItem contextMenuItem, UIAction uiAction)
        {
            if (contextMenuItem.IsCheckable)
            {
                uiAction.State = contextMenuItem.IsChecked ? UIMenuElementState.On : UIMenuElementState.Off;
            }
        }
    }
}