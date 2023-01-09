using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Support.V4.View;
using Android.Views;
using AndroidX.AppCompat.Widget;
using DIPS.Xamarin.UI.Controls.ContextMenu;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Shapes;
using Menu = Android.Views.Menu;

namespace DIPS.Xamarin.UI.Android.ContextMenu
{
    internal static class ContextMenuHelper
    {
        internal static Dictionary<ContextMenuItem, IMenuItem> CreateMenuItems(Context context,
            IEnumerable<ContextMenuItem> contextMenuItems,
            ContextMenuButton contextMenuButton, PopupMenu popupMenu, int groupIndex = 0)
        {
            var dict = new Dictionary<ContextMenuItem, IMenuItem>();
            var items = contextMenuItems.ToList();

            foreach (var contextMenuItem in items)
            {
                var index = items.IndexOf(contextMenuItem);
                if (contextMenuItem is ContextMenuGroup contextMenuGroup)
                {
                    contextMenuGroup.Parent = contextMenuButton;
                    groupIndex += 1;

                    if (items.Count(i => i is ContextMenuGroup) >
                        1) //If there is more than one group, add the group title and group the items
                    {
                        var groupMenu = popupMenu.Menu.AddSubMenu(groupIndex, index, Menu.None,
                            contextMenuGroup.Title);

                        if (groupMenu == null) continue;
                        if (contextMenuGroup.ItemsSource == null) continue;

                        foreach (var contextMenuItemInGroup in contextMenuGroup.ItemsSource)
                        {
                            var contextMenuItemInGroupIndex =
                                contextMenuGroup.ItemsSource.IndexOf(contextMenuItemInGroup);
                            var menuItem = groupMenu.Add(groupIndex, contextMenuItemInGroupIndex, Menu.None,
                                contextMenuItemInGroup.Title);
                            UpdateMenuItem(context, contextMenuButton, groupIndex, contextMenuItemInGroup, menuItem);
                            dict.Add(contextMenuItemInGroup, menuItem);
                        }


                        if (contextMenuGroup.IsCheckable)
                        {
                            groupMenu.SetGroupCheckable(groupIndex, contextMenuGroup.IsCheckable, false);
                        }
                    }
                    else //Only one group, add this to the root of the menu so the user does not have to tap an extra time to get to the items.
                    {
                        var newDict = CreateMenuItems(context, contextMenuGroup.ItemsSource, contextMenuButton,
                            popupMenu,
                            groupIndex);
                        newDict.ForEach(pair =>
                        {
                            var contextMenuItemInGroup = pair.Key;
                            contextMenuItemInGroup.Parent = contextMenuGroup;
                            dict.Add(contextMenuItemInGroup, pair.Value);
                        });
                        if (contextMenuGroup.IsCheckable)
                        {
                            popupMenu.Menu.SetGroupCheckable(groupIndex, contextMenuGroup.IsCheckable, false);
                        }
                    }
                }
                else
                {
                    var menuItem = popupMenu.Menu.Add(groupIndex, index, Menu.None, contextMenuItem.Title);
                    UpdateMenuItem(context, contextMenuButton, groupIndex, contextMenuItem, menuItem);

                    dict.Add(contextMenuItem, menuItem);
                }
            }

            MenuCompat.SetGroupDividerEnabled(popupMenu.Menu, true);

            return dict;
        }

        private static void UpdateMenuItem(Context context, ContextMenuButton contextMenuButton, int groupIndex,
            ContextMenuItem contextMenuItem, IMenuItem menuItem)
        {
            if (!string.IsNullOrEmpty(contextMenuItem.Icon))
            {
                var id = context.Resources?.GetIdentifier(contextMenuItem.Icon, "drawable", context.PackageName);
                var androidResourceId = context.Resources?.GetIdentifier(contextMenuItem.AndroidOptions.IconResourceName, "drawable",context.PackageName);
                id = androidResourceId ?? id;
                if (id != null)
                {
                    menuItem.SetIcon((int)id);   
                }
            }

            TrySetChecked(contextMenuButton, menuItem, contextMenuItem);
            if (groupIndex == 0) //Not in a group
            {
                contextMenuItem.Parent = contextMenuButton;
            }
        }

        private static void TrySetChecked(ContextMenuButton contextMenuButton, IMenuItem menuItem,
            ContextMenuItem contextMenuItem)
        {
            menuItem?.SetCheckable(contextMenuItem.IsCheckable);
            if (contextMenuItem.IsChecked) //If an item is checked, reset the rest in the group
            {
                contextMenuButton.ResetIsCheckedForTheRest(contextMenuItem);
            }

            menuItem?.SetChecked(contextMenuItem.IsChecked);
        }
    }
}