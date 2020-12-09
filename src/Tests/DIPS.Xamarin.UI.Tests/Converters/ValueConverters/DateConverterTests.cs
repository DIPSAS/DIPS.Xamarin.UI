using System;
using System.Collections.Generic;
using System.Globalization;
using DIPS.Xamarin.UI.Converters.ValueConverters;
using DIPS.Xamarin.UI.Internal.Utilities;
using DIPS.Xamarin.UI.Resources.LocalizedStrings;
using FluentAssertions;
using Xunit;
using DIPS.Xamarin.UI.Tests.TestHelpers;
using DateConverterFormat = DIPS.Xamarin.UI.Converters.ValueConverters.DateConverter.DateConverterFormat;
using Xamarin.Forms.Xaml;

namespace DIPS.Xamarin.UI.Tests.Converters.ValueConverters
{
    [Collection("Sequential")] //This test class is using an static shared property that is used in other tests
    public class DateConverterTests
    {
        private readonly DateTime m_now = new DateTime(1990, 12, 12, 12, 00, 00);
        private readonly DateConverter m_dateConverter = new DateConverter();
        private string m_expected;

        [Theory]
        [InlineData(0)]
        [InlineData(0.0)]
        [InlineData(0.0f)]
        [InlineData("test")]
        public void Convert_InvalidInput_XamlParseExceptionThrown(object invalidInput)
        {
            Action act = () => m_dateConverter.Convert<string>(invalidInput);

            act.Should().Throw<XamlParseException>();
        }

        [Fact]
        public void Convert_NullInput_ShouldReturnEmptyString()
        {
            var actual = m_dateConverter.Convert<string>(null);

            actual.Should().Be(string.Empty);
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
                new object[] {"no", new DateTime(1991, 12, 12), "12. des 1991"},
                new object[] {"en-gb", new DateTime(1991, 12, 12), "12th Dec 1991"},
                new object[] {"en-us", new DateTime(1991, 12, 12), "Dec 12th, 1991"},
            };

        [Theory]
        [MemberData(nameof(TestDataForShortFormat))]
        public void Convert_WithShortFormat_WithCulture_CorrectFormat(string cultureName, DateTime date,
            string expected)
        {
            Clock.OverrideClock(date);

            m_dateConverter.Format = DateConverterFormat.Short;
            var actual = m_dateConverter.Convert<string>(date, new CultureInfo(cultureName));
            actual.Should().Be(expected);
        }

        [Fact]
        public void Converter_WithEnglishCulture_DayLessThanTen_ShouldNotIncludeFirstZero()
        {
            var date = new DateTime(1990, 12, 03);

            //To force localized strings
            if (CultureInfo.CurrentCulture.Equals("en-us"))
            {
                InternalLocalizedStrings.Culture = new CultureInfo("en-us");
                m_expected = "Dec 3rd, 1990";
            }
            else
            {
                InternalLocalizedStrings.Culture = new CultureInfo("en-gb");
                m_expected = "3rd Dec 1990";
            }

            var actual = m_dateConverter.Convert<string>(date, InternalLocalizedStrings.Culture);

            actual.Should().Be(m_expected);
        }

        public static IEnumerable<object[]> TestDataForTextFormat =>
            new List<object[]>()
            {
                new object[] {"en-us", new DateTime(1990, 12, 12), "Today"},
                new object[] {"en-us", new DateTime(1990, 12, 12).AddDays(-1), "Yesterday"},
                new object[] {"en-us", new DateTime(1990, 12, 12).AddDays(1), "Tomorrow"},
                new object[] {"en-us", new DateTime(1990, 12, 10), "Dec 10th,"},
                new object[] {"en-gb", new DateTime(1990, 12, 12), "Today"},
                new object[] { "en-gb", new DateTime(1990, 12, 12).AddDays(-1), "Yesterday"},
                new object[] { "en-gb", new DateTime(1990, 12, 12).AddDays(1), "Tomorrow"},
                new object[] { "en-gb", new DateTime(1990, 12, 10), "10th Dec"},
                new object[] {"no", new DateTime(1990, 12, 12), "I dag"},
                new object[] {"no", new DateTime(1990, 12, 12).AddDays(-1), "I går"},
                new object[] {"no", new DateTime(1990, 12, 12).AddDays(1), "I morgen"},
                new object[] {"no", new DateTime(1990, 12, 10), "10. des"}
            };

        [Theory]
        [MemberData(nameof(TestDataForTextFormat))]
        public void Convert_WithTextFormat_WithDate_WithCulture_CorrectFormat(string cultureName, DateTime date,
            string expected)
        {
            Clock.OverrideClock(m_now);

            m_dateConverter.Format = DateConverterFormat.Text;

            InternalLocalizedStrings.Culture = new CultureInfo(cultureName); //To force localized strings

            var actual = m_dateConverter.Convert<string>(date, InternalLocalizedStrings.Culture);
            actual.Should().Be(expected);
        }
    }
}