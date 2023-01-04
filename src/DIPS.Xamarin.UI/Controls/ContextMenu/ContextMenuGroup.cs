using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace DIPS.Xamarin.UI.Controls.ContextMenu
{
    [ContentProperty(nameof(Children))]

    public partial class ContextMenuGroup : ContextMenuItem
    {
        public IList<ContextMenuItem> Children { get; private set; } = new List<ContextMenuItem>();

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            Children.ForEach(c => c.BindingContext = BindingContext);
        }
        
        private static void OnItemsSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is not ContextMenuGroup contextMenuGroup) return;
            if (contextMenuGroup.ItemsSource != null && contextMenuGroup.ItemsSource.Any())
            {
                contextMenuGroup.Children = contextMenuGroup.ItemsSource.ToList();
            }
        }
    }
}