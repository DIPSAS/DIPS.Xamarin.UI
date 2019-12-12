using System;
using System.Globalization;
using DIPS.Xamarin.UI.Converters.ValueConverters;
using FluentAssertions;
using Xunit;
using DIPS.Xamarin.UI.Tests.TestHelpers;

namespace DIPS.Xamarin.UI.Tests.Converters.ValueConverters
{
    public class DateConverterTests
    {
        private readonly DateConverter m_dateConverter = new DateConverter();

        [Theory]
        [InlineData(0)]
        [InlineData(0.0)]
        [InlineData(0.0f)]
        [InlineData("test")]
        public void Convert_InvalidInput_ThrowsArgumentException(object invalidInput)
        {
            Action act = () => m_dateConverter.Convert<string>(invalidInput);

            act.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData("no", "12. des. 1991")]
        [InlineData("en", "12. dec 1991")]
        public void Convert_WithDefaultFormat_WithCulture_CorrectFormat(string cultureName, string expected)
        {
            var dateTimeInput = new DateTime(1991, 12, 12, 09, 09, 00);

            var actual = m_dateConverter.Convert<string>(dateTimeInput, new CultureInfo(cultureName));

            actual.Should().Be(expected);
        }
    }
}