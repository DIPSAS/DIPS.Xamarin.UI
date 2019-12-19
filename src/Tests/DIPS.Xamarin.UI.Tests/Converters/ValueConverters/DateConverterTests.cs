using System;
using System.Collections.Generic;
using System.Globalization;
using DIPS.Xamarin.UI.Converters.ValueConverters;
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
            m_dateConverter.Format = DateConverterFormat.Short;

            var actual = m_dateConverter.Convert<string>(date, new CultureInfo(cultureName));

            actual.Should().Be(expected);
        }

        public static IEnumerable<object[]> TestDataForTextFormat =>
            new List<object[]>()
            {
                new object[] { "en", DateTime.Now, "Today" },
                new object[] { "en", DateTime.Now.AddDays(-1), "Yesterday" },
                new object[] { "en", DateTime.Now.AddDays(1), "Tomorrow" },
                new object[] { "en", new DateTime(1991, 12, 12, 09, 09, 00), "12th Dec" },
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