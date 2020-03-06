using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Controls.Content.DataTemplateSelectors
{
    /// <summary>
    /// A template selector with only 1 template. To be used with ContentControl with only 1 possible template.
    /// </summary>
    public class SingleTemplateSelector : DataTemplateSelector, IMarkupExtension
    {
        /// <inheritdoc />
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (Template == null) throw new ArgumentException("Data template can not be null");
            return Template;
        }

        /// <summary>
        /// The <see cref="DataTemplate"/> to use as the Template.
        /// </summary>
        public DataTemplate? Template { get; set; }

        /// <inheritdoc />
        public object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}
