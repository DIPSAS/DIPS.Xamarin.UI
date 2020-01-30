using System;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Samples.Controls.Content
{
    public class ContentControlTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? VM1Template { get; set; }
        public DataTemplate? VM2Template { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (VM1Template == null || VM2Template == null) throw new ArgumentNullException();
            if (item is ViewModel1) return VM1Template;
            return VM2Template;
        }
    }
}
