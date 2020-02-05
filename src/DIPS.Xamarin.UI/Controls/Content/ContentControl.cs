using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Controls.Content
{
    /// <summary>
    /// A control to consume a DataTemplateSelector
    /// </summary>
    public class ContentControl : ContentView
    {
        /// <summary>
        /// Creates an instance to use a DataTemplateSelector
        /// </summary>
        public ContentControl()
        {
            BindingContextChanged += (s, e) => UpdateContent();
        }

        private void UpdateContent()
        {
            if(BindingContext == null || TemplateSelector == null)
            {
                return;
            }

            var template = TemplateSelector.SelectTemplate(SelectorItem ?? BindingContext, this);
            Content = template.CreateContent() as View;
        }

        /// <summary>
        /// Sets the selector to show the content as defined by the BindingContext of this control
        /// </summary>
        public DataTemplateSelector? TemplateSelector { get; set; }


        /// <summary>
        ///  <see cref="SelectorItem" />
        /// </summary>
        public static readonly BindableProperty SelectorItemProperty = BindableProperty.Create(
            nameof(SelectorItem),
            typeof(object),
            typeof(ContentControl),
            null,
            BindingMode.OneWay,
            propertyChanged: (s, o, n) => ((ContentControl)s).UpdateContent());

        /// <summary>
        /// Used to select the template in the selector
        /// </summary>
        public object SelectorItem
        {
            get => (object)GetValue(SelectorItemProperty);
            set => SetValue(SelectorItemProperty, value);
        }
    }
}
