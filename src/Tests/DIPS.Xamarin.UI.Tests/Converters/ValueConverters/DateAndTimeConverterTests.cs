using System;
using System.Collections.Generic;
using System.Globalization;
using DIPS.Xamarin.UI.Converters.ValueConverters;
using DIPS.Xamarin.UI.InternalUtils;
using DIPS.Xamarin.UI.Resources.LocalizedStrings;
using DIPS.Xamarin.UI.Tests.TestHelpers;
using FluentAssertions;
using Xunit;
using DateAndTimeConverterFormat = DIPS.Xamarin.UI.Converters.ValueConverters.DateAndTimeConverter.DateAndTimeConverterFormat;

namespace DIPS.Xamarin.UI.Tests.Converters.ValueConverters
{
    [Collection("Sequential")] //This test class is using an static shared property that is used in other tests
    public class DateAndTimeConverterTests
    {
        private readonly DateTime m_now = new DateTime(1990, 12, 12, 12, 00, 00);
        private readonly DateAndTimeConverter m_dateAndTimeConverter = new DateAndTimeConverter();

        [Theory]
        [InlineData(0)]
        [InlineData(0.0)]
        [InlineData(0.0f)]
        [InlineData("test")]
        [InlineData(null)]
        public void Convert_InvalidInput_ThrowsArgumentException(object invalidInput)
        {
            Action act = () => m_dateAndTimeConverter.Convert<string>(invalidInput);

            act.Should().Throw<ArgumentException>();
        }

        public static IEnumerable<object[]> TestDataForShortFormat =>
            new List<object[]>()
            {
                new object[] { "no", new DateTime(1991, 12, 12,12,12,12), "12. des 1991 kl 12:12" },
                new object[] { "en", new DateTime(1991, 12, 12,12,12,12), "12th Dec 1991 12:12 PM" },
                new object[] { "en", new DateTime(1991, 12, 12,10,12,12), "12th Dec 1991 10:12 AM" },
            };

        [Theory]
        [MemberData(nameof(TestDataForShortFormat))]
        public void Convert_WithShortFormat_WithCulture_CorrectFormat(string cultureName, DateTime date, string expected)
        {
            m_dateAndTimeConverter.Format = DateAndTimeConverterFormat.Short;

            var actual = m_dateAndTimeConverter.Convert<string>(date, new CultureInfo(cultureName));

            actual.Should().Be(expected);
        }

        public static IEnumerable<object[]> TestDataForTextFormat =>
            new List<object[]>()
            {
                new object[] { "en", new DateTime(1990,12,12,12,12,00), "Today 12:12 PM" },
                new object[] { "en", new DateTime(1991, 12, 10, 12, 12, 12), "10th Dec 12:12 PM" },
                new object[] { "en", new DateTime(1990, 12, 12, 12, 12, 00).AddDays(-1), "Yesterday 12:12 PM" },
                new object[] { "en", new DateTime(1990, 12, 12, 12, 12, 00).AddDays(1), "Tomorrow 12:12 PM" },
                new object[] { "no", new DateTime(1990, 12, 12, 10, 12, 00), "I dag kl 10:12" },
                new object[] { "no", new DateTime(1990, 12, 12, 12, 12, 00).AddDays(-1), "I går kl 12:12" },
                new object[] { "no", new DateTime(1990, 12, 12, 12, 12, 00).AddDays(1), "I morgen kl 12:12" },
                new object[] { "no", new DateTime(1990, 12, 10, 09, 09, 00), "10. des kl 09:09" }
            };

        [Theory]
        [MemberData(nameof(TestDataForTextFormat))]
        public void Convert_WithTextFormat_WithDate_WithCulture_CorrectFormat(string cultureName, DateTime date, string expected)
        {
            Clock.OverrideClock(m_now);

            m_dateAndTimeConverter.Format = DateAndTimeConverterFormat.Text;
            InternalLocalizedStrings.Culture = new CultureInfo(cultureName);//To force localized strings

            var actual = m_dateAndTimeConverter.Convert<string>(date, InternalLocalizedStrings.Culture);
            actual.Should().Be(expected);
        }
    }
}
