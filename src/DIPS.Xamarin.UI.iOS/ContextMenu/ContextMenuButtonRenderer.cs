using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CoreGraphics;
using DIPS.Xamarin.UI.Controls.ContextMenu;
using DIPS.Xamarin.UI.iOS.ContextMenu;
using Foundation;
using ObjCRuntime;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContextMenuButton), typeof(ContextMenuButtonRenderer))]

namespace DIPS.Xamarin.UI.iOS.ContextMenu
{
    internal class ContextMenuButtonRenderer : ButtonRenderer
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
                        this.SetNativeControl(new ContextMenuUIButton(m_contextMenuButton));
                        Control.Menu =
                            CreateMenu(); //Create the menu the first time so it shows up the first time the user taps the button
                        Control.TouchDown += OnTouchDown;
                        Control.ShowsMenuAsPrimaryAction = true;
                        NSNotificationCenter.DefaultCenter.AddObserver(UIApplication.DidEnterBackgroundNotification, notification =>
                        {
                            Control.Menu = null;
                            Control.Menu = CreateMenu(); //Recreate the menu to close it, and to make it possible to re-open it in one tap after it went to the background
                        });
                    }
                }
            }
            else
            {
                if (Control != null)
                {
                    Control.TouchDown -= OnTouchDown;
                    NSNotificationCenter.DefaultCenter.RemoveObserver(UIApplication.DidEnterBackgroundNotification);
                }
            }
        }

        private void OnTouchDown(object sender, EventArgs e)
        {
            Control.Menu =
                CreateMenu(); //Recreate the menu so the visuals of the items of the menu are able to change between each time the user opens the menu
            m_contextMenuButton.SendContextMenuOpened();
        }

        private UIMenu CreateMenu()
        {
            if (m_contextMenuButton.ItemsSource == null) return null;
            
            var dict = ContextMenuHelper.CreateMenuItems(
                m_contextMenuButton.ItemsSource,
                m_contextMenuButton);
            return  UIMenu.Create(m_contextMenuButton.Title, dict.Select(k => k.Value).ToArray());
        }
    }

    internal class ContextMenuUIButton : UIButton
    {
        private readonly ContextMenuButton m_contextMenuButton;

        public ContextMenuUIButton(ContextMenuButton contextMenuButton)
        {
            m_contextMenuButton = contextMenuButton;
        }

        public override CGPoint GetMenuAttachmentPoint(UIContextMenuConfiguration configuration)
        {
            var original = base.GetMenuAttachmentPoint(configuration);
            return m_contextMenuButton.ContextMenuHorizontalOptions switch
            {
                ContextMenuHorizontalOptions.Right => new CGPoint(9999, original.Y),
                ContextMenuHorizontalOptions.Left => new CGPoint(0, original.Y),
                _ => new CGPoint(9999, original.Y)
            };
        }
    }
}