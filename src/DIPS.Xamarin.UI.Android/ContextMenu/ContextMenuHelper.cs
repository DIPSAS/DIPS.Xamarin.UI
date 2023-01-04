using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Android.Support.V7.View.Menu;
using Android.Support.V7.Widget;
using Android.Views;
using DIPS.Xamarin.UI.Controls.ContextMenu;
using Xamarin.Forms.Internals;

namespace DIPS.Xamarin.UI.Android.ContextMenu
{
    internal static class ContextMenuHelper
    {
        internal static Dictionary<ContextMenuItem, IMenuItem> CreateMenuItems(
            IEnumerable<ContextMenuItem> contextMenuItems,
            ContextMenuButton contextMenuButton, PopupMenu popupMenu, int groupIndex = 0)
        {
            var dict = new Dictionary<ContextMenuItem, IMenuItem>();
            var menuItems = contextMenuItems.ToList();

            foreach (var contextMenuItem in menuItems)
            {
                var index = menuItems.IndexOf(contextMenuItem);
                if (contextMenuItem is ContextMenuGroup contextMenuGroup)
                {
                    groupIndex += 1;
                    var newDict = CreateMenuItems(contextMenuGroup.Children, contextMenuButton, popupMenu, groupIndex);
                    newDict.ForEach(pair => dict.Add(pair.Key, pair.Value));
                    if (contextMenuGroup.IsCheckable)
                    {
                        popupMenu.Menu.SetGroupCheckable(groupIndex, contextMenuGroup.IsCheckable, false);
                    }
                }
                else
                {
                    var menuItem = popupMenu.Menu.Add(groupIndex, index, Menu.None, contextMenuItem.Title);
                    
                    menuItem?.SetCheckable(contextMenuItem.IsCheckable);
                    if (contextMenuItem.IsChecked) //If an item is checked, reset the rest in the group
                    {
                        contextMenuButton.ResetIsCheckedForTheRest(contextMenuItem);    
                    }
                    
                    
                    menuItem?.SetChecked(contextMenuItem.IsChecked);
                    dict.Add(contextMenuItem, menuItem);
                }
            }

            return dict;
        }
    }
}