using System.Collections.Generic;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.ContextMenu
{
    [ContentProperty(nameof(Children))]

    public class ContextMenuGroup : ContextMenuItem
    {
        private readonly List<ContextMenuItem> m_children = new();
        public IList<ContextMenuItem> Children => m_children;
    }
}