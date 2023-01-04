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
    [ContentProperty(nameof(Children))]
    public partial class ContextMenuButton : Button
    {
        private readonly List<ContextMenuItem> m_children = new List<ContextMenuItem>();
        public IList<ContextMenuItem> Children => m_children;

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            Children.ForEach(c => c.BindingContext = BindingContext);
        }


        public void ResetIsCheckedForTheRest(ContextMenuItem contextMenuItem)
        {
            if (Children.Contains(contextMenuItem))
            {
                contextMenuItem.IsChecked = true;
            }
            else
            {
                foreach (var child in Children)
                {
                    if (child == contextMenuItem) //this is the item that should not be reseted
                    {
                        continue;
                    }

                    if (child is ContextMenuGroup contextMenuGroup)
                    {
                        if (contextMenuGroup.Children.Contains(contextMenuItem)) //Its added to a group so we need to uncheck the rest in the group before checking the item
                        {
                            contextMenuGroup.Children.ForEach(c =>
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
        }
    }
}