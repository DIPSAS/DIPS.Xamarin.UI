using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using DIPS.Xamarin.UI.Converters.ValueConverters;
using DIPS.Xamarin.UI.Resources.LocalizedStrings;
using FluentAssertions;
using Xunit;
using DIPS.Xamarin.UI.Tests.TestHelpers;
using DateConverterFormat = DIPS.Xamarin.UI.Converters.ValueConverters.DateConverter.DateConverterFormat;

namespace DIPS.Xamarin.UI.Tests.Converters.ValueConverters
{
    public class DateConverterTests
    {
        private DateConverter m_dateConverter = new DateConverter();

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

        public static IEnumerable<object[]> TestDataForDefaultFormat =>
            new List<object[]>()
            {
                new object[] { "no", new DateTime(1991, 12, 12), "12. des 1991" },
                new object[] { "en", new DateTime(1991, 12, 12), "12. dec 1991" },
            };

        [Theory]
        [MemberData(nameof(TestDataForDefaultFormat))]
        public void Convert_WithShortFormat_WithCulture_CorrectFormat(string cultureName, DateTime date, string expected)
        {
            m_dateConverter.Format = DateConverter.DateConverterFormat.Short;

            var actual = m_dateConverter.Convert<string>(date, new CultureInfo(cultureName));

            actual.Should().Be(expected);
        }

        [Fact]  
        public void DateConverterFormat_SetToDefault_FormatShouldBeShort()
        {
            m_dateConverter.Format = DateConverterFormat.Default;

            m_dateConverter.Format.Should().Be(DateConverterFormat.Short);
        }

        public static IEnumerable<object[]> TestDataForTextFormat =>
            new List<object[]>()
            {
                new object[] { "en", DateTime.Now, "Today" },
                new object[] { "en", DateTime.Now.AddDays(-1), "Yesterday" },
                new object[] { "en", DateTime.Now.AddDays(1), "Tomorrow" },
                new object[] { "en", new DateTime(1991, 12, 12, 09, 09, 00), "12. dec" },
                new object[] { "no", DateTime.Now, "I dag" },
                new object[] { "no", DateTime.Now.AddDays(-1), "I går" },
                new object[] { "no", DateTime.Now.AddDays(1), "I morgen" },
                new object[] { "no", new DateTime(1991, 12, 12, 09, 09, 00), "12. des" }
            };

        [Theory]
        [MemberData(nameof(TestDataForTextFormat))]
        public void Convert_WithTextFormat_WithDate_WithCulture_CorrectFormat(string cultureName, DateTime date, string expected)
        {
            m_dateConverter.Format = DateConverterFormat.Text;

            InternalLocalizedStrings.Culture = new CultureInfo(cultureName);//To force localized strings

            var actual = m_dateConverter.Convert<string>(date, InternalLocalizedStrings.Culture);
            actual.Should().Be(expected);
        }
    }
}