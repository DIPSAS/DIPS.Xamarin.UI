using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.ContextMenu
{
    public partial class ContextMenuButton : IContextMenu
    {
        /// <see cref="Title"/>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(ContextMenuItem), defaultValue: string.Empty);

        /// <summary>
        /// The title of the context menu button
        /// </summary>
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        /// <summary>
        /// <see cref="ItemClickedCommand"/>
        /// </summary>
        public static readonly BindableProperty ItemClickedCommandProperty = BindableProperty.Create(
            nameof(ItemClickedCommand),
            typeof(ICommand),
            typeof(ContextMenuButton));

        /// <summary>
        /// <see cref="ItemsSource"/>
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IList<ContextMenuItem>),
            typeof(ContextMenuButton),
            defaultValueCreator:(bindable => new List<ContextMenuItem>()));

        /// <summary>
        /// The context menu items to display in the context menu when its opened
        /// </summary>
        public IList<ContextMenuItem>? ItemsSource
        {
            get => (IList<ContextMenuItem>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        /// Command that gets invoked with a parameter when a <see cref="ContextMenuItem"/> was clicked by the user
        /// </summary>
        public ICommand? ItemClickedCommand
        {
            get => (ICommand)GetValue(ItemClickedCommandProperty);
            set => SetValue(ItemClickedCommandProperty, value);
        }

        /// <summary>
        /// Invoked when the context menu opened
        /// </summary>
        public event EventHandler? ContextMenuOpened;

        /// <summary>
        /// <see cref="ContextMenuHorizontalOptions"/>
        /// </summary>
        public static readonly BindableProperty ContextMenuHorizontalOptionsProperty = BindableProperty.Create(
            nameof(ContextMenuHorizontalOptions),
            typeof(ContextMenuHorizontalOptions),
            typeof(ContextMenuControl));

        /// <summary>
        /// <see cref="ContextMenuHorizontalOptions"/>
        /// </summary>
        public ContextMenuHorizontalOptions ContextMenuHorizontalOptions
        {
            get => (ContextMenuHorizontalOptions)GetValue(ContextMenuHorizontalOptionsProperty);
            set => SetValue(ContextMenuHorizontalOptionsProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler? ItemClicked;

        internal void SendClicked(ContextMenuItem item)
        {
            ItemClickedCommand?.Execute(item);
            ItemClicked?.Invoke(item, EventArgs.Empty);
        }
    }
}