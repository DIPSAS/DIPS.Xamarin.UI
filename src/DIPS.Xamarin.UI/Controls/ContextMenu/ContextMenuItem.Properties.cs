using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.ContextMenu
{
    public partial class ContextMenuItem
    {
        /// <summary>
        /// The clicked event when the item was clicked
        /// </summary>
        public event EventHandler? Clicked;
        
        /// <summary>
        /// <see cref="Title"/>
        /// </summary>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(ContextMenuItem), defaultValue: string.Empty);

        /// <summary>
        /// The title of the context menu item
        /// </summary>
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        /// <summary>
        /// <see cref="CommandParameter"/>
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(ContextMenuItem), defaultValueCreator: bindable => bindable is not ContextMenuItem contextMenuItem ? string.Empty : contextMenuItem.Title);

        
        /// <summary>
        /// This command parameter gets sent to <see cref="ContextMenuButton"/> command 
        /// </summary>
        public object? CommandParameter
        {
            get => (object)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        /// Determines if the native check mark should be added to the item when its tapped
        /// </summary>
        public bool IsCheckable { get; set; }
        
        /// <summary>
        /// Determines if the item should be default checked when its opened for the first time
        /// </summary>
        public bool IsChecked { get; set; }
    }
}