using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CoreGraphics;
using DIPS.Xamarin.UI.Controls.ContextMenu;
using DIPS.Xamarin.UI.iOS.ContextMenu;
using ObjCRuntime;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContextMenuButton), typeof(ContextMenuButtonRenderer))]

namespace DIPS.Xamarin.UI.iOS.ContextMenu
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
                        Control.Menu = CreateMenu(); //Create the menu the first time so it shows up the first time the user taps the button
                        Control.TouchDown += OnTouchDown;
                        Control.ShowsMenuAsPrimaryAction = true;
                    }
                }
            }
            else
            {
                if (Control != null)
                {
                    Control.TouchDown -= OnTouchDown;    
                }
            }
        }

        private void OnTouchDown(object sender, EventArgs e)
        {
            Control.Menu = CreateMenu(); //Recreate the menu so the visuals of the items of the menu are able to change between each time the user opens the menu
        }

        private UIMenu CreateMenu()
        {
            var dict = ContextMenuHelper.CreateMenuItems(
                m_contextMenuButton.Children.Reverse(),
                m_contextMenuButton);
            return UIMenu.Create(m_contextMenuButton.Title, dict.Select(k => k.Value).ToArray());
        }
    }
}