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
            MenuItemCommand = new Command<ContextMenuItem>(MenuItemClicked);
        }

        private void MenuItemClicked(ContextMenuItem clickedMenuItem)
        {
            Console.WriteLine();
        }

        public string Text => "test 123";

        public Command MenuItemCommand { get; }
        public IEnumerable<ContextMenuItem> Items
        {
            get => m_items;
            private set => PropertyChanged?.RaiseWhenSet(ref m_items, value);
        }

        public async Task Initialize()
        {
            await Task.Delay(1000);
            Items = new[]
            {
                new ContextMenuItem(){Title = "Option 1", IsChecked = true},
                new ContextMenuItem(){Title = "Option 2", IsChecked = true},
                new ContextMenuItem(){Title = "Option 3"},
                new ContextMenuItem(){Title = "Option 4"}
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}