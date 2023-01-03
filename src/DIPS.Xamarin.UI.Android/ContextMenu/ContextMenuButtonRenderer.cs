using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using DIPS.Xamarin.UI.Android.ContextMenu;
using DIPS.Xamarin.UI.Controls.ContextMenu;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ContextMenuButton), typeof(ContextMenuButtonRenderer))]

namespace DIPS.Xamarin.UI.Android.ContextMenu
{
    public class ContextMenuButtonRenderer : ButtonRenderer, PopupMenu.IOnMenuItemClickListener
    {
        private readonly Context m_context;
        private ContextMenuButton m_contextMenuButton;
        private Dictionary<ContextMenuItem, IMenuItem> m_menuItems;

        public ContextMenuButtonRenderer(Context context) : base(context)
        {
            m_context = context;
        }

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
                        contextMenuButton.Clicked += OpenContextMenu;
                    }
                }
                else
                {
                    contextMenuButton.Clicked -= OpenContextMenu;
                }
            }
        }

        private void OpenContextMenu(object sender, EventArgs e)
        {
            var popupMenu = new PopupMenu(m_context, Control);
            m_menuItems = ContextMenuHelper.CreateMenuItems(m_contextMenuButton.Children, m_contextMenuButton, popupMenu);
            popupMenu.SetOnMenuItemClickListener(this);
            popupMenu.Show();
        }

        public bool OnMenuItemClick(IMenuItem theTappedNativeItem)
        {
            var theTappedSharedItem = m_menuItems?.FirstOrDefault(m => m.Value == theTappedNativeItem).Key;
            if (theTappedSharedItem != null)
            {
                if (theTappedNativeItem.IsCheckable) //check the item
                {
                    m_menuItems.ForEach(pair => pair.Value.SetChecked(false));
                    m_menuItems?.ForEach(pair => pair.Key.IsChecked = false);
                    theTappedSharedItem.IsChecked = true;
                    theTappedNativeItem.SetChecked(true);
                }
                
                theTappedSharedItem.SendClicked(m_contextMenuButton);
                return true;
            }

            return false;
        }
    }
}