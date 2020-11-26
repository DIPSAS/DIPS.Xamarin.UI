using System;
using System.Collections.Generic;
using DIPS.Xamarin.UI.Converters.ValueConverters;
using DIPS.Xamarin.UI.Extensions.Markup;
using FluentAssertions;
using Xunit;

namespace DIPS.Xamarin.UI.Tests.Converters.ValueConverters
{
    public class StringCaseConverterTests
    {
        private readonly StringCaseConverter m_stringCaseConverter;

        public StringCaseConverterTests()
        {
            m_stringCaseConverter = new StringCaseConverter();
        }

        [Theory]
        [InlineData(null)]
        [InlineData(1)]
        [InlineData(1.0f)]
        [InlineData(1.0)]
        [InlineData('c')]
        public void Convert_InvalidInput_ThrowsException(object input)
        {
            m_stringCaseConverter.Convert(input, null, null, null).Should().BeNull();
        }


        public static IEnumerable<object[]> ValidData =>
            new List<object[]>
            {
                new object[] { StringCase.Upper, "test", "TEST" },
                new object[] { StringCase.Lower, "TEST", "test" },
                new object[] { StringCase.Title, "test this right here", "Test This Right Here" },
                new object[] { StringCase.Title, "this   has   extra   space", "This   Has   Extra   Space" },
            };

        [Theory]
        [MemberData(nameof(ValidData))]
        public void Convert_ValidData_ShouldBeAsExpected(StringCase stringCase, string input, string expected)
        {
            m_stringCaseConverter.StringCase = stringCase;
            var actual = m_stringCaseConverter.Convert(input, null, null, null);
            actual.Should().Be(expected);
        }

        [Fact]
        public void Convert_InputStringIsEmpty_OutputShouldBeStringEmpty()
        {
            var actual = m_stringCaseConverter.Convert(string.Empty, null, null, null);
            actual.Should().Be(string.Empty);
        }
    }
}
