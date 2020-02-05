using System;
using System.Collections.Generic;
using DIPS.Xamarin.UI.Controls.Content;
using DIPS.Xamarin.UI.Controls.Content.DataTemplateSelectors;
using FluentAssertions;
using Xamarin.Forms;
using Xunit;

namespace DIPS.Xamarin.UI.Tests.Controls.Content.DataTemplateSelectors
{
    public class BooleanDataTemplateSelectorTests
    {
        private readonly BooleanDataTemplateSelector m_booleanDataTemplateSelector = new BooleanDataTemplateSelector();

        [Theory]
        [InlineData(null)]
        [InlineData("test")]
        [InlineData(1)]
        [InlineData(1.1)]
        [InlineData((float)1.1)]
        public void OnSelectTemplate_InvalidSelectorItem_ThrowsArgumentException(object selectorItem)
        {
            var contentControl = new ContentControl { };
            contentControl.TemplateSelector = m_booleanDataTemplateSelector;

            Action act = () =>
            {
                contentControl.SelectorItem = selectorItem;
                contentControl.BindingContext = "Test";
            };

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void OnSelectTemplate_SelectorItemIsTrue_TrueTemplateSelected()
        {
            var contentControl = new ContentControl { };
            contentControl.TemplateSelector = m_booleanDataTemplateSelector;
            m_booleanDataTemplateSelector.TrueTemplate = new DataTemplate(() => new CheckBox() { IsChecked = true });
            m_booleanDataTemplateSelector.FalseTemplate = new DataTemplate(() => new CheckBox() { IsChecked = false });

            contentControl.SelectorItem = true;
            contentControl.BindingContext = "Test";
            

            contentControl.Content.Should().BeOfType<CheckBox>();
            ((CheckBox)contentControl.Content).IsChecked.Should().BeTrue();
        }

        [Fact]
        public void OnSelectTemplate_SelectorItemIsFalse_FalseTemplateSelected()
        {
            var contentControl = new ContentControl { };
            contentControl.TemplateSelector = m_booleanDataTemplateSelector;
            m_booleanDataTemplateSelector.TrueTemplate = new DataTemplate(() => new CheckBox() { IsChecked = true });
            m_booleanDataTemplateSelector.FalseTemplate = new DataTemplate(() => new CheckBox() { IsChecked = false });

            contentControl.SelectorItem = false;
            contentControl.BindingContext = "Test";


            contentControl.Content.Should().BeOfType<CheckBox>();
            ((CheckBox)contentControl.Content).IsChecked.Should().BeFalse();
        }

        [Fact]
        public void OnSelectTemplate_TrueTemplateIsNull_ThrowsArgumentException()
        {
            var contentControl = new ContentControl { };
            contentControl.TemplateSelector = m_booleanDataTemplateSelector;
            m_booleanDataTemplateSelector.FalseTemplate = new DataTemplate();

            Action act = () =>
            {
                contentControl.SelectorItem = false;
                contentControl.BindingContext = "Test";
            };

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void OnSelectTemplate_FalseTemplateIsNull_ThrowsArgumentException()
        {
            var contentControl = new ContentControl { };
            contentControl.TemplateSelector = m_booleanDataTemplateSelector;
            m_booleanDataTemplateSelector.TrueTemplate = new DataTemplate();

            Action act = () =>
            {
                contentControl.SelectorItem = true;
                contentControl.BindingContext = "Test";
            };

            act.Should().Throw<ArgumentException>();
        }
    }
}