using System;
using DIPS.Xamarin.UI.Converters.MultiValueConverters;
using Xunit;
using DIPS.Xamarin.UI.Tests.TestHelpers;
using FluentAssertions;
using System.Collections.ObjectModel;

namespace DIPS.Xamarin.UI.Tests.Converters.MultiValueConverters
{
    public class ItemIsLastConverterTests
    {
        private ItemIsLastConverter m_cut = new ItemIsLastConverter();

        [Theory]
        [InlineData(new object[] { "a", new object[] { "abc", "de", "a" }, false, "first", "second", "first" })]
        [InlineData(new object[] { "de", new object[] { "abc", "de", "a" }, false, "first", "second", "second" })]
        [InlineData(new object[] { "a", new object[] { "abc", "de", "a" }, true, "first", "second", "second" })]
        [InlineData(new object[] { "abc", new object[] { "abc", "de", "a" }, false, "first", "second", "second" })]
        public void Convert_Cases_CorrectOutput(string item, object[] items, bool inverted, string trueObj, string falseObj, string expected)
        {
            var values = new object[] { item, items };
            m_cut.TrueObject = trueObj;
            m_cut.FalseObject = falseObj;
            m_cut.Inverted = inverted;

            var output = m_cut.Convert<string>(values);

            output.Should().Be(expected);
        }

        [Theory]
        [InlineData(new object[] { new object[] { "abc", "de", "a" }, "a" , false, "first", "second", "first" })]
        [InlineData(new object[] { new object[] { "abc", "de", "a" }, "de",  false, "first", "second", "second" })]
        [InlineData(new object[] { new object[] { "abc", "de", "a" }, "a",  true, "first", "second", "second" })]
        [InlineData(new object[] { new object[] { "abc", "de", "a" }, "abc",  false, "first", "second", "second" })]
        public void Convert_ReversedCases_CorrectOutput(object[] items, string item, bool inverted, string trueObj, string falseObj, string expected)
        {
            var values = new object[] { item, items };
            m_cut.TrueObject = trueObj;
            m_cut.FalseObject = falseObj;
            m_cut.Inverted = inverted;

            var output = m_cut.Convert<string>(values);

            output.Should().Be(expected);
        }

        [Fact]
        public void Convert_ObservableCollection_CorrectOutput()
        {
            var item = "a";
            var items = new ObservableCollection<string>
            {
                 "abc", "de", "a"
            };
            var values = new object[] { item, items };

            var output = m_cut.Convert<bool>(values);

            output.Should().Be(true);
        }
    }
}
