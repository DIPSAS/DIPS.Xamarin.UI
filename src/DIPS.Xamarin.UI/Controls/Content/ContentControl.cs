using System;
using System.Linq;
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
            BindingContextChanged += ContentControl_BindingContextChanged;
        }

        private void ContentControl_BindingContextChanged(object sender, EventArgs e)
        {
            if(BindingContext == null)
            {
                return;
            }

            if(TemplateSelector != null)
            {
                var template = TemplateSelector.SelectTemplate(BindingContext, this);
                Content = template.CreateContent() as View;
            }
            else
            {
                var resources = Application.Current.Resources;
                foreach(var resource in resources)
                {
                    var obj = resource.Value;
                    if (!(obj is DataTemplate template)) continue;

                }
            }
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
            propertyChanged: (s, o, n) => ((ContentControl)s).ContentControl_BindingContextChanged(s, EventArgs.Empty));

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
