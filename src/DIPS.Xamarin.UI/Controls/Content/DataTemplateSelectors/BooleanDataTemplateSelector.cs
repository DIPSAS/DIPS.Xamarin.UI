using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.Content.DataTemplateSelectors
{
    /// <summary>
    /// A boolean template selector that returns a true or false template depending on a item
    /// </summary>
    public class BooleanDataTemplateSelector : DataTemplateSelector, IMarkupExtension
    {
        /// <inheritdoc />
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (!bool.TryParse(item.ToString(), out var booleanItem)) throw new ArgumentException("Selector item has to be of type boolean");
            if(TrueTemplate == null || FalseTemplate == null) throw new ArgumentException("True/False data templates can not be null");
            return (booleanItem) ? TrueTemplate : FalseTemplate;
        }

        /// <summary>
        /// The <see cref="DataTemplate"/> to use when the selector item is false
        /// </summary>
        public DataTemplate? FalseTemplate { get; set; }
        /// <summary>
        /// The <see cref="DataTemplate"/> to use when the selector item is true
        /// </summary>
        public DataTemplate? TrueTemplate { get; set; }

        /// <inheritdoc />
        public object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}
