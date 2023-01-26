using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.View.Menu;
using Android.Views;
using AndroidX.AppCompat.Widget;
using DIPS.Xamarin.UI.Android.ContextMenu;
using DIPS.Xamarin.UI.Controls.ContextMenu;
using Java.Lang.Reflect;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;
using Application = Android.App.Application;

[assembly: ExportRenderer(typeof(ContextMenuButton), typeof(ContextMenuButtonRenderer))]

namespace DIPS.Xamarin.UI.Android.ContextMenu
{
    internal class ContextMenuButtonRenderer : ButtonRenderer, PopupMenu.IOnMenuItemClickListener, Application.IActivityLifecycleCallbacks
    {
        private readonly Context m_context;
        private ContextMenuButton m_contextMenuButton;
        private Dictionary<ContextMenuItem, IMenuItem> m_menuItems;
        private MenuPopupHelper? m_menuHelper; //Might be null if the user never tapped the button to open it

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
                        m_contextMenuButton.Clicked += OpenContextMenu;
                        (((Activity)Context!)).RegisterActivityLifecycleCallbacks(this);
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            m_contextMenuButton.Clicked -= OpenContextMenu;
            (((Activity)Context!)).UnregisterActivityLifecycleCallbacks(this);
            base.Dispose(disposing);
        }

        private void OpenContextMenu(object sender, EventArgs e)
        {
            var gravity = (m_contextMenuButton.ContextMenuHorizontalOptions == ContextMenuHorizontalOptions.Right)
                ? (int)GravityFlags.Right
                : (int)GravityFlags.Left;
            var popupMenu = new PopupMenu(m_context, Control);
                m_menuItems = ContextMenuHelper.CreateMenuItems(m_context, m_contextMenuButton.ItemsSource,
                m_contextMenuButton, popupMenu);
            popupMenu.SetOnMenuItemClickListener(this);
            m_menuHelper = new MenuPopupHelper(Context,(MenuBuilder) popupMenu.Menu, Control);
            m_menuHelper.Gravity = gravity;
            m_menuHelper.SetForceShowIcon(m_menuItems.Keys.Any(contextMenuItem => !string.IsNullOrEmpty(contextMenuItem.Icon) || !string.IsNullOrEmpty(contextMenuItem.AndroidOptions.IconResourceName))); //Show icons if there is any context menu items with a icon added
            m_menuHelper.Show();
            m_contextMenuButton.SendContextMenuOpened();
        }

        public bool OnMenuItemClick(IMenuItem theTappedNativeItem)
        {
            var contextMenuItem = m_menuItems?.FirstOrDefault(m => m.Value == theTappedNativeItem).Key;
            if (contextMenuItem != null)
            {
                if (theTappedNativeItem.IsCheckable) //check the item
                {
                    if (contextMenuItem.Parent is ContextMenuGroup &&
                        theTappedNativeItem
                            .IsChecked) //You are unchecking a grouped item, which means its single mode and it should not be able to uncheck
                    {
                        return true;
                    }

                    m_menuItems.ForEach(pair =>
                    {
                        if (pair.Value.GroupId == theTappedNativeItem.GroupId) //Uncheck previous items
                        {
                            pair.Value.SetChecked(false);
                        }
                    });

                    m_contextMenuButton.ResetIsCheckedForTheRest(contextMenuItem);
                    contextMenuItem.IsChecked = !contextMenuItem.IsChecked;
                    theTappedNativeItem.SetChecked(contextMenuItem.IsChecked);
                }

                contextMenuItem.SendClicked(m_contextMenuButton);
                return true;
            }

            return false;
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
            if (m_menuHelper is {IsShowing: true})
            {
                m_menuHelper.Dismiss();    
            }
        }

        public void OnActivityResumed(Activity activity)
        {
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
        }

        public void OnActivityStopped(Activity activity)
        {
        }
    }
}