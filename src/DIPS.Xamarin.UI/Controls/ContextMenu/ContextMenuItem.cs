using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.ContextMenu
{
    /// <summary>
    /// A context menu item to use in a context menu
    /// </summary>
    public partial class ContextMenuItem : BindableObject
    {
        internal void SendClicked(ContextMenuButton contextMenuButton)
        {
            contextMenuButton.SendClicked(this);
            Command?.Execute(CommandParameter);
            Clicked?.Invoke(this, EventArgs.Empty);
        }
    }
}