using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.ContextMenu
{
    public partial class ContextMenuItem
    {
        /// <summary>
        /// <see cref="Command"/>
        /// </summary>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(ContextMenuItem));

        /// <summary>
        /// The command to run when the item was clicked
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// <see cref="CommandParameter"/>
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(ContextMenuItem));

        /// <summary>
        /// The command parameter to send to the command when the item was clicked
        /// </summary>
        public object CommandParameter
        {
            get => (object)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
        
        /// <summary>
        /// The clicked event when the item was clicked
        /// </summary>
        public event EventHandler? Clicked;
        
        /// <summary>
        /// The title of the context menu item
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Determines if the native check mark should be added to the item when its tapped
        /// </summary>
        public bool IsCheckable { get; set; }
        
        /// <summary>
        /// Determines if the item should be default checked when its opened for the first time
        /// </summary>
        public bool IsChecked { get; set; }
        
        /// <summary>
        /// The parent of the context menu item
        /// </summary>
        public object Parent { get; internal set; }
        
        /// <summary>
        /// The subtitle of the menu item
        /// </summary>
        /// <remarks>Only works on iOS</remarks>
        public string? Subtitle { get; set; }
    }
}