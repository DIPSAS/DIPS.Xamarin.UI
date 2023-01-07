using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DIPS.Xamarin.UI.Controls.ContextMenu;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Controls.ContextMenu
{
    public class ContextMenuPageViewModel : INotifyPropertyChanged
    {
        private IEnumerable<ContextMenuItem> m_items;

        public ContextMenuPageViewModel()
        {
            ItemClickedCommand = new Command<ContextMenuItem>(MenuItemClicked);
        }

        private void MenuItemClicked(ContextMenuItem clickedMenuItem)
        {
            Console.WriteLine(clickedMenuItem.Title);
        }
        
        public Command ItemClickedCommand { get; }
        public Command Test1Command { get; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}