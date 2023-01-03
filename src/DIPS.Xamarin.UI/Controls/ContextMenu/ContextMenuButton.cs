using System.Collections.Generic;
using Xamarin.Forms;

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
    }
}