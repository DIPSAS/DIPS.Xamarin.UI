using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace DIPS.Xamarin.UI.Controls.ContextMenu
{
    [ContentProperty(nameof(ItemsSource))]

    public partial class ContextMenuGroup : ContextMenuItem
    {
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            ItemsSource.ForEach(c => c.BindingContext = BindingContext);
        }
    }
}