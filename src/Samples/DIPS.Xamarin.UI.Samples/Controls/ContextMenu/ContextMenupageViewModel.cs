using System;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Controls.ContextMenu
{
    public class ContextMenuPageViewModel
    {
        public ContextMenuPageViewModel()
        {
            MenuItemCommand = new Command<string>(MenuItemClicked);
        }

        private void MenuItemClicked(string clickedMenuItem)
        {
            Console.WriteLine();
        }

        public string Text => "test 123";

        public Command MenuItemCommand { get; }
        public bool EditShouldBeVisible => false;
    }
}