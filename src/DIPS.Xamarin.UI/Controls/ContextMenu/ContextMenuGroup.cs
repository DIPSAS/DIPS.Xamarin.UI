using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace DIPS.Xamarin.UI.Controls.ContextMenu
{
    /// <summary>
    /// A context menu group with multiple items
    /// </summary>
    [ContentProperty(nameof(ItemsSource))]
    
    public partial class ContextMenuGroup : ContextMenuItem
    {
        
        /// <summary>
        /// <inheritdoc cref="BindableObject"/>
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            ItemsSource.ForEach(c => c.BindingContext = BindingContext);
        }
    }
}