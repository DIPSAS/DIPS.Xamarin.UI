using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.ContextMenu
{
    public partial class ContextMenuItem : View
    {
        public event EventHandler Clicked;

        public void Click()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }
    }
}