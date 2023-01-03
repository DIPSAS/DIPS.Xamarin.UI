using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.ContextMenu
{
    public partial class ContextMenuItem : BindableObject
    {
        internal void SendClicked(ContextMenuButton contextMenuButton)
        {
            contextMenuButton.ContextItemClickedCommand?.Execute(this);
            Clicked?.Invoke(this, EventArgs.Empty);
        }
    }
}