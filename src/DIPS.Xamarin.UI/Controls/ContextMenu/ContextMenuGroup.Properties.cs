using System.Collections.Generic;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.ContextMenu
{
    public partial class ContextMenuGroup
    {
        /// <summary>
        /// <see cref="ItemsSource"/>
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            nameof(ItemsSource),
            typeof(IList<ContextMenuItem>),
            typeof(ContextMenuGroup), defaultValueCreator:(bindable => new List<ContextMenuItem>()));

        /// <summary>
        /// Items to be used as context menu items for the group
        /// </summary>
        /// <remarks>Changes to the items source will only apply the moment when the context menu is opened. If it changes when its open it will not apply until its re-opened.</remarks>
        public IList<ContextMenuItem>? ItemsSource
        {
            get => (IList<ContextMenuItem>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }
    }
}