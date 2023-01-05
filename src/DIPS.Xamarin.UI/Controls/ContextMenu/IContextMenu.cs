using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.ContextMenu
{
    public interface IContextMenu
    {
        /// <summary>
        /// The title of the context menu button
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// The context menu items
        /// </summary>
        public IList<ContextMenuItem> ItemsSource { get; }

        /// <summary>
        /// Command that gets invoked with a paramter when a <see cref="ContextMenuItem"/> was clicked by the user
        /// </summary>
        public ICommand? ItemClickedCommand { get; set; }

        /// <summary>
        /// Invoked when the context menu opened
        /// </summary>
        public event EventHandler? ContextMenuOpened;
        
        /// <summary>
        /// Invoked when a item was clicked from the context menu
        /// </summary>
        public event EventHandler? ItemClicked;

        public ContextMenuHorizontalOptions ContextMenuHorizontalOptions { get; set; }
    }
}