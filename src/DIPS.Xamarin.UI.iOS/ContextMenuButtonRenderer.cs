using System.Collections.Generic;
using System.Linq;
using DIPS.Xamarin.UI.Controls.ContextMenu;
using DIPS.Xamarin.UI.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContextMenuButton), typeof(ContextMenuButtonRenderer))]

namespace DIPS.Xamarin.UI.iOS
{
    public class ContextMenuButtonRenderer : ButtonRenderer
    {
        private ContextMenuButton m_contextMenuButton;
        internal static void Initialize() { }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is ContextMenuButton contextMenuButton)
            {
                m_contextMenuButton = contextMenuButton;
                if (Control != null)
                {
                    {
                        Control.Menu = UIMenu.Create(contextMenuButton.Title,
                            CreateMenuItems(m_contextMenuButton.Children));
                        Control.ShowsMenuAsPrimaryAction = true;
                    }
                }
            }
            else
            {
                //Dispose here
            }
        }

        private UIMenuElement[] CreateMenuItems(IEnumerable<ContextMenuItem> contextMenuItems)
        {
            var uiMenuElements = new List<UIMenuElement>();
            
            foreach (var contextMenuItem in contextMenuItems.Reverse()) //Reverse so it matched the XAML direction
            {
                if (contextMenuItem is ContextMenuGroup contextMenuGroup)
                {
                    uiMenuElements.Add(UIMenu.Create(contextMenuGroup.Title, CreateMenuItems(contextMenuGroup.Children)));
                }
                else
                {
                    uiMenuElements.Add(UIAction.Create(contextMenuItem.Title, null, null, (args) =>
                    {
                        m_contextMenuButton.CommandParameter =  (contextMenuItem.CommandParameter == null) ? contextMenuItem.Title : contextMenuItem.CommandParameter;
                        m_contextMenuButton.SendClicked();
                        contextMenuItem.Click();
                    }));   
                }
               
            }

            return uiMenuElements.ToArray();
        }
    }
}