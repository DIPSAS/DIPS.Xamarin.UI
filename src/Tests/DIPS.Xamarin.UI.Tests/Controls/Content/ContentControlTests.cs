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

            contentControl.BindingContext = "text";
            contentControl.TemplateSelector = new TemplateSelector();

            contentControl.Content.Should().NotBeNull();
        }
    }

    public class TemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return new DataTemplate(() =>
            {
                return new StackLayout();
            });
        }
    }
}
