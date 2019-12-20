using System;
using System.Collections.Generic;
using System.Globalization;
using DIPS.Xamarin.UI.Converters.ValueConverters;
using DIPS.Xamarin.UI.InternalUtils;
using DIPS.Xamarin.UI.Tests.TestHelpers;
using FluentAssertions;
using Xunit;
using TimeConverterFormat = DIPS.Xamarin.UI.Converters.ValueConverters.TimeConverter.TimeConverterFormat;

namespace DIPS.Xamarin.UI.Tests.Converters.ValueConverters
{
    public class TimeConverterTests
    {
        private readonly TimeConverter m_timeConverter = new TimeConverter();

        [Theory]
        [InlineData(0)]
        [InlineData(0.0)]
        [InlineData(0.0f)]
        [InlineData("test")]
        [InlineData(null)]
        public void Convert_InvalidInput_ThrowsArgumentException(object invalidInput)
        {
            Action act = () => m_timeConverter.Convert<string>(invalidInput);

            act.Should().Throw<ArgumentException>();
        }

        public static IEnumerable<object[]> TestDataForDefaultFormat =>
            new List<object[]>()
            {
                new object[] { "no", new TimeSpan(09,12,00), "09:12" },
                new object[] { "no", new TimeSpan(21,12,00), "21:12" },
                new object[] { "en", new TimeSpan(09,12,00), "09:12 AM" },
                new object[] { "en", new TimeSpan(21,12,00), "09:12 PM" },
            };

        [Theory]
        [MemberData(nameof(TestDataForDefaultFormat))]
        public void Convert_WithDefaultFormat_WithCulture_CorrectFormat(string cultureName, TimeSpan time, string expected)
        {
            m_timeConverter.Format = TimeConverterFormat.Default;

            var actual = m_timeConverter.Convert<string>(time, new CultureInfo(cultureName));

            actual.Should().Be(expected);
        }

        [Fact]
        public void Convert_WithShortFormat_WithDateTime_CorrectFormat()
        {
            var expected = "12:12 PM";
            var date = new DateTime(1991, 12, 12, 12, 12, 12);
            Clock.OverrideClock(date);
            m_timeConverter.Format = TimeConverterFormat.Default;

            var actual = m_timeConverter.Convert<string>(date, new CultureInfo("en"));

            actual.Should().Be(expected);
        }
    }
}
