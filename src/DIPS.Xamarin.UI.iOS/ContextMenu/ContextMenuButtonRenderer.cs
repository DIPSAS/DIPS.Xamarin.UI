using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CoreGraphics;
using DIPS.Xamarin.UI.Controls.ContextMenu;
using DIPS.Xamarin.UI.iOS.ContextMenu;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContextMenuButton), typeof(ContextMenuButtonRenderer))]

namespace DIPS.Xamarin.UI.iOS.ContextMenu
{
    public class ContextMenuButtonRenderer : ButtonRenderer
    {
        private Dictionary<ContextMenuItem, UIMenuElement> m_contextMenuDict;
        internal static void Initialize() { }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is ContextMenuButton contextMenuButton)
            {
                if (Control != null)
                {
                    {
                        m_contextMenuDict = ContextMenuHelper.CreateMenuItems(contextMenuButton.Children.Reverse(),
                            contextMenuButton);
                        SubscribeToPropertyChangedForAllMenuItems();
                        var uiMenuElements = m_contextMenuDict.Select(k => k.Value).ToArray();
                        Control.Menu = UIMenu.Create(contextMenuButton.Title, uiMenuElements);
                        Control.ShowsMenuAsPrimaryAction = true;
                    }
                }
            }
            else
            {
                UnSubscribeToPropertyChangedForAllMenuItems();
            }
        }
        private void SubscribeToPropertyChangedForAllMenuItems()
        {
            m_contextMenuDict.Select(k => k.Key).ForEach(menuItem => menuItem.PropertyChanged += OnMenuItemPropertyChanged);
        }

        private void OnMenuItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is ContextMenuItem theContextMenuItem)
            {
                if (!theContextMenuItem.IsVisible)
                {
                    var (contextMenuItem,uiMenuElement) = m_contextMenuDict.FirstOrDefault(kv => kv.Key == theContextMenuItem);
                    if (contextMenuItem != null)
                    {
                        if (uiMenuElement != null)
                        {
                        }
                    } 
                }
            }
        }

        private void UnSubscribeToPropertyChangedForAllMenuItems()
        {
            m_contextMenuDict.Select(k => k.Key).ForEach(menuItem => menuItem.PropertyChanged -= OnMenuItemPropertyChanged);
        }
    }
}