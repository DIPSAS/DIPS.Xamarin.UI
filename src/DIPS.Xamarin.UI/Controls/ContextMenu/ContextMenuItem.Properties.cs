using System.Windows.Input;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.ContextMenu
{
    public partial class ContextMenuItem
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(ContextMenuItem), defaultValue: string.Empty);

        public string Title
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// This command parameter gets sent to <see cref="ContextMenuButton"/> command 
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(ContextMenuItem));

        public object CommandParameter
        {
            get => (object)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
    }
}