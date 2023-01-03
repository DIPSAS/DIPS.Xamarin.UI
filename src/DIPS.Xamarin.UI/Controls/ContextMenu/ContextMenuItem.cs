using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.ContextMenu
{
    public partial class ContextMenuItem : View
    {
        internal void SendClicked(ContextMenuButton contextMenuButton)
        {
            var commandParameter = CommandParameter ?? Title;
            contextMenuButton.ContextItemClickedCommand?.Execute(commandParameter);
            Clicked?.Invoke(this, EventArgs.Empty);
        }
    }
}