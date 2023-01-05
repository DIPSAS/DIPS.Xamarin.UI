using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace DIPS.Xamarin.UI.Controls.ContextMenu
{
    /// <summary>
    /// The Android idea: https://developer.android.com/develop/ui/views/components/menus#PopupMenu
    /// The iOS idea: https://developer.apple.com/design/human-interface-guidelines/components/menus-and-actions/context-menus/
    /// </summary>
    [ContentProperty(nameof(ItemsSource))]
    public partial class ContextMenuButton : Button
    {
        /// <summary>
        /// <inheritdoc cref="Button"/>
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            ItemsSource?.ForEach(c => c.BindingContext = BindingContext);
        }

        internal void ResetIsCheckedForTheRest(ContextMenuItem contextMenuItem)
        {
            if (ItemsSource.Contains(contextMenuItem)) //its on the root, and others on the root should not be resetted
            {
                return;
            }
            
            foreach (var child in ItemsSource)
            {
                if (child == contextMenuItem) //this is the item that should not be reseted
                {
                    continue;
                }

                if (child is ContextMenuGroup contextMenuGroup)
                {
                    if (contextMenuGroup.ItemsSource
                        .Contains(
                            contextMenuItem)) //Its added to a group so we need to uncheck the rest in the group before checking the item
                    {
                        contextMenuGroup.ItemsSource.ForEach(c =>
                        {
                            if (c != contextMenuItem)
                            {
                                c.IsChecked = false;
                            }
                        });
                        continue;
                    }
                }

                child.IsChecked = false;
            }
        }

        internal void SendContextMenuOpened()
        {
            ContextMenuOpened?.Invoke(this, EventArgs.Empty);
        }
    }
}