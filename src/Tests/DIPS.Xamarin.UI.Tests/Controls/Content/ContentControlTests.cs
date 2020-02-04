using System;
using DIPS.Xamarin.UI.Controls.Content;
using FluentAssertions;
using Xamarin.Forms;
using Xunit;

namespace DIPS.Xamarin.UI.Tests.Controls.Content
{
    public class ContentControlTests
    {
        [Fact]
        public void UpdateContent_WithoutBindingContext_NoContent()
        {
            var contentControl = new ContentControl();

            contentControl.BindingContext = null;
            contentControl.TemplateSelector = null;

            contentControl.Content.Should().BeNull();
        }

        [Fact]
        public void UpdateContent_WithBindingContextAndSelector()
        {
            var contentControl = new ContentControl();

            contentControl.TemplateSelector = new TemplateSelector();
            contentControl.BindingContext = "text";

            contentControl.Content.Should().NotBeNull();
        }

        [Fact]
        public void UpdateContent_SelectorItem_notString()
        {
            var contentControl = new ContentControl();

            contentControl.TemplateSelector = new TemplateSelector();
            contentControl.BindingContext = "Text";
            contentControl.SelectorItem = 5;

            contentControl.Content.Should().BeOfType<Button>();
        }

        [Fact]
        public void UpdateContent_SelectorItem_String()
        {
            var contentControl = new ContentControl();

            contentControl.TemplateSelector = new TemplateSelector();
            contentControl.BindingContext = 5;
            contentControl.SelectorItem = "text";

            contentControl.Content.Should().BeOfType<StackLayout>();
        }
    }

    public class TemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return new DataTemplate(() =>
            {
                if (item is string)
                    return new StackLayout();
                else
                    return new Button();
            });
        }
    }
}
