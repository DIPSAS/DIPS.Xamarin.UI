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
            typeof(IEnumerable<ContextMenuItem>),
            typeof(ContextMenuGroup), defaultValue: new List<ContextMenuItem>(), propertyChanged:OnItemsSourceChanged);

        /// <summary>
        /// Items to be used as context menu items for the group
        /// </summary>
        /// <remarks>Changes to the items source will only apply the moment when the context menu is opened. If it changes when its open it will not apply until its re-opened.</remarks>
        public IEnumerable<ContextMenuItem>? ItemsSource
        {
            get => (IEnumerable<ContextMenuItem>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }
    }
}