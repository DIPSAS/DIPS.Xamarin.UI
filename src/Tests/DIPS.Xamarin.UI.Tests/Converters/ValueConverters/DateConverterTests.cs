using System;
using System.Collections.Generic;
using System.Globalization;
using DIPS.Xamarin.UI.Converters.ValueConverters;
using DIPS.Xamarin.UI.InternalUtils;
using DIPS.Xamarin.UI.Resources.LocalizedStrings;
using FluentAssertions;
using Xunit;
using DIPS.Xamarin.UI.Tests.TestHelpers;
using DateConverterFormat = DIPS.Xamarin.UI.Converters.ValueConverters.DateConverter.DateConverterFormat;

namespace DIPS.Xamarin.UI.Tests.Converters.ValueConverters
{
    [Collection("Sequential")] //This test class is using an static shared property that is used in other tests
    public class DateConverterTests
    {
        private readonly DateTime m_now = new DateTime(1990, 12, 12, 12, 00,00);
        private readonly DateConverter m_dateConverter = new DateConverter();

        [Theory]
        [InlineData(0)]
        [InlineData(0.0)]
        [InlineData(0.0f)]
        [InlineData("test")]
        [InlineData(null)]
        public void Convert_InvalidInput_ThrowsArgumentException(object invalidInput)
        {
            Action act = () => m_dateConverter.Convert<string>(invalidInput);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void DateConverterFormat_SetToDefault_FormatShouldBeShort()
        {
            m_dateConverter.Format = DateConverterFormat.Default;

            m_dateConverter.Format.Should().Be(DateConverterFormat.Short);
        }

        public static IEnumerable<object[]> TestDataForShortFormat =>
            new List<object[]>()
            {
                new object[] { "no", new DateTime(1991, 12, 12), "12. des 1991" },
                new object[] { "en", new DateTime(1991, 12, 12), "12th Dec 1991" },
            };

        [Theory]
        [MemberData(nameof(TestDataForShortFormat))]
        public void Convert_WithShortFormat_WithCulture_CorrectFormat(string cultureName, DateTime date, string expected)
        {
            Clock.OverrideClock(date);

            m_dateConverter.Format = DateConverterFormat.Short;

            var actual = m_dateConverter.Convert<string>(date, new CultureInfo(cultureName));

            actual.Should().Be(expected);
        }

        [Fact]
        public void Converter_WithEnglishCulture_DayLessThanTen_ShouldNotIncludeFirstZero()
        {
            InternalLocalizedStrings.Culture = new CultureInfo("en");//To force localized strings
            var date = new DateTime(1990, 12, 03);
            var expected = "3rd Dec 1990";

            var actual = m_dateConverter.Convert<string>(date, InternalLocalizedStrings.Culture);

            actual.Should().Be(expected);
        }

        public static IEnumerable<object[]> TestDataForTextFormat =>
            new List<object[]>()
            {
                new object[] { "en", new DateTime(1990,12,12), "Today" },
                new object[] { "en", new DateTime(1990, 12, 12).AddDays(-1), "Yesterday" },
                new object[] { "en", new DateTime(1990, 12, 12).AddDays(1), "Tomorrow" },
                new object[] { "en", new DateTime(1990, 12, 10), "10th Dec" },
                new object[] { "no", new DateTime(1990, 12, 12), "I dag" },
                new object[] { "no", new DateTime(1990, 12, 12).AddDays(-1), "I går" },
                new object[] { "no", new DateTime(1990, 12, 12).AddDays(1), "I morgen" },
                new object[] { "no", new DateTime(1990, 12, 10), "10. des" }
            };

        [Theory]
        [MemberData(nameof(TestDataForTextFormat))]
        public void Convert_WithTextFormat_WithDate_WithCulture_CorrectFormat(string cultureName, DateTime date, string expected)
        {
            Clock.OverrideClock(m_now);

            m_dateConverter.Format = DateConverterFormat.Text;

            InternalLocalizedStrings.Culture = new CultureInfo(cultureName);//To force localized strings

            var actual = m_dateConverter.Convert<string>(date, InternalLocalizedStrings.Culture);
            actual.Should().Be(expected);
        }
    }
}