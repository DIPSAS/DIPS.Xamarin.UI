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

            var template = TemplateSelector.SelectTemplate(BindingContext, this);
            Content = template.CreateContent() as View;
        }

        /// <summary>
        ///  <see cref="TemplateSelector" />
        /// </summary>
        public static readonly BindableProperty TemplateSelectorProperty = BindableProperty.Create(
            nameof(TemplateSelector),
            typeof(DataTemplateSelector),
            typeof(ContentControl),
            null,
            BindingMode.TwoWay,
            propertyChanged: (s, o, n) => ((ContentControl)s).UpdateContent());

        /// <summary>
        /// Sets the selector to show the content as defined by the BindingContext of this control
        /// </summary>
        public DataTemplateSelector TemplateSelector
        {
            get => (DataTemplateSelector)GetValue(TemplateSelectorProperty);
            set => SetValue(TemplateSelectorProperty, value);
        }
    }
}
