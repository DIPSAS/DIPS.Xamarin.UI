using System.Windows.Input;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.ContextMenu
{
    public partial class ContextMenuButton
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
        /// <see cref="ContextItemClickedCommand"/>
        /// </summary>
        public static readonly BindableProperty ContextItemClickedCommandProperty = BindableProperty.Create(
            nameof(ContextItemClickedCommand),
            typeof(ICommand),
            typeof(ContextMenuButton));
        
        /// <summary>
        /// Command that gets invoked with a paramter when a <see cref="ContextMenuItem"/> was clicked by the user
        /// </summary>
        public ICommand? ContextItemClickedCommand
        {
            get => (ICommand)GetValue(ContextItemClickedCommandProperty);
            set => SetValue(ContextItemClickedCommandProperty, value);
        }
    }
}