using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.ContextMenu
{
    public partial class ContextMenuItem
    {
        /// <summary>
        /// The clicked event when the item was clicked
        /// </summary>
        public event EventHandler? Clicked;
        
        /// <summary>
        /// The title of the context menu item
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// Determines if the native check mark should be added to the item when its tapped
        /// </summary>
        public bool IsCheckable { get; set; }
        
        /// <summary>
        /// Determines if the item should be default checked when its opened for the first time
        /// </summary>
        public bool IsChecked { get; set; }
    }
}