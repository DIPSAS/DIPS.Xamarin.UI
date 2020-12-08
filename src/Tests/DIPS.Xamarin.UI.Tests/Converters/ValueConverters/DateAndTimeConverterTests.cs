using System;
using System.Collections.Generic;
using System.Globalization;
using DIPS.Xamarin.UI.Converters.ValueConverters;
using DIPS.Xamarin.UI.Internal.Utilities;
using DIPS.Xamarin.UI.Resources.LocalizedStrings;
using DIPS.Xamarin.UI.Tests.TestHelpers;
using FluentAssertions;
using Xamarin.Forms.Xaml;
using Xunit;
using DateAndTimeConverterFormat =
    DIPS.Xamarin.UI.Converters.ValueConverters.DateAndTimeConverter.DateAndTimeConverterFormat;

namespace DIPS.Xamarin.UI.Tests.Converters.ValueConverters
{
    [Collection("Sequential")] //This test class is using an static shared property that is used in other tests
    public class DateAndTimeConverterTests
    {
        private readonly DateTime m_now = new DateTime(1990, 12, 12, 13, 00, 00);
        private readonly DateAndTimeConverter m_dateAndTimeConverter = new DateAndTimeConverter();

        [Theory]
        [InlineData(0)]
        [InlineData(0.0)]
        [InlineData(0.0f)]
        [InlineData("test")]
        public void Convert_InvalidInput_XamlParseExceptionThrown(object invalidInput)
        {
            Action act = () => m_dateAndTimeConverter.Convert<string>(invalidInput);

            act.Should().Throw<XamlParseException>();
        }

        [Fact]
        public void Convert_NullInput_ShouldReturnEmptyString()
        {
            var actual = m_dateAndTimeConverter.Convert<string>(null);

            actual.Should().Be(string.Empty);
        }

        public static IEnumerable<object[]> TestDataForShortFormat =>
            new List<object[]>()
            {
                new object[] {"no", new DateTime(1991, 12, 12, 13, 12, 12), "12. des 1991 kl 13:12"},
                new object[] {"en", new DateTime(1991, 12, 12, 10, 12, 12), "Dec 12th, 1991 10:12 AM"},
                new object[] {"en", new DateTime(1991, 12, 12, 13, 12, 12), "Dec 12th, 1991 01:12 PM"},
                new object[] {"enu", new DateTime(1991, 12, 12, 13, 12, 12), "12th Dec 1991 13:12"},
                new object[] {"enu", new DateTime(1991, 12, 12, 10, 12, 00), "12th Dec 1991 10:12"},
            };

        [Theory]
        [MemberData(nameof(TestDataForShortFormat))]
        public void Convert_WithShortFormat_WithCulture_CorrectFormat(string cultureName, DateTime date,
            string expected)
        {
            m_dateAndTimeConverter.Format = DateAndTimeConverterFormat.Short;

            var actual = m_dateAndTimeConverter.Convert<string>(date, new CultureInfo(cultureName));

            actual.Should().Be(expected);
        }

        public static IEnumerable<object[]> TestDataForTextFormat =>
            new List<object[]>()
            {
                new object[] {"enu", new DateTime(1990, 12, 12, 13, 00, 00), "Today 13:00"},
                new object[] {"enu", new DateTime(1991, 12, 10, 13, 00, 00), "10th Dec 13:00"},
                new object[] {"enu", new DateTime(1990, 12, 12, 13, 00, 00).AddDays(-1), "Yesterday 13:00"},
                new object[] {"enu", new DateTime(1990, 12, 12, 13, 00, 00).AddDays(1), "Tomorrow 13:00"},
                new object[] {"en", new DateTime(1990, 12, 12, 13, 00, 00), "Today 01:00 PM"},
                new object[] {"en", new DateTime(1991, 12, 10, 13, 00, 00), "Dec 10th, 01:00 PM"},
                new object[] {"en", new DateTime(1990, 12, 12, 13, 00, 00).AddDays(-1), "Yesterday 01:00 PM"},
                new object[] {"en", new DateTime(1990, 12, 12, 13, 00, 00).AddDays(1), "Tomorrow 01:00 PM"},
                new object[] {"no", new DateTime(1990, 12, 12, 13, 00, 00), "I dag, kl 13:00"},
                new object[] {"no", new DateTime(1990, 12, 12, 13, 00, 00).AddDays(-1), "I går, kl 13:00"},
                new object[] {"no", new DateTime(1990, 12, 12, 13, 00, 00).AddDays(1), "I morgen, kl 13:00"},
                new object[] {"no", new DateTime(1990, 12, 10, 13, 00, 00), "10. des kl 13:00"}
            };

        [Theory]
        [MemberData(nameof(TestDataForTextFormat))]
        public void Convert_WithTextFormat_WithDate_WithCulture_CorrectFormat(string cultureName, DateTime date,
            string expected)
        {
            Clock.OverrideClock(m_now);

            m_dateAndTimeConverter.Format = DateAndTimeConverterFormat.Text;
            InternalLocalizedStrings.Culture = new CultureInfo(cultureName); //To force localized strings

            var actual = m_dateAndTimeConverter.Convert<string>(date, InternalLocalizedStrings.Culture);
            actual.Should().Be(expected);
        }
    }
}