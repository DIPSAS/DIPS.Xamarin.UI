using System;
using System.Collections.Generic;
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

        public static IEnumerable<object[]> TestDataForDefaultFormat => new List<object[]>()
        {
            new object[] {"no", new DateTime(1991,12,12), "12. des 1991"},
            new object[] {"en", new DateTime(1991, 12, 12),"12. dec 1991" },
        };

        [Theory]
        [MemberData(nameof(TestDataForDefaultFormat))]
        public void Convert_WithDefaultFormat_WithCulture_CorrectFormat(string cultureName, DateTime date, string expected)
        {
            var actual = m_dateConverter.Convert<string>(date, new CultureInfo(cultureName));

            actual.Should().Be(expected);
        }
        public static IEnumerable<object[]> TestDataForTextFormat => new List<object[]>()
        {
            new object[] {"en", DateTime.Now,"Today"},
            new object[] {"en", DateTime.Now.AddDays(-1),"Yesterday"},
            new object[] {"en", DateTime.Now.AddDays(1),"Tomorrow"},
            new object[] {"en", new DateTime(1991, 12, 12, 09, 09, 00), "12. dec" },
            new object[] {"no", DateTime.Now,"I dag"},
            new object[] {"no", DateTime.Now.AddDays(-1),"I går"},
            new object[] {"no", DateTime.Now.AddDays(1),"I morgen"},
            new object[] {"no", new DateTime(1991, 12, 12, 09, 09, 00), "12. des" }

        };
        [Theory]
        [MemberData(nameof(TestDataForTextFormat))]
        public void Convert_WithTextFormat_WithDate_WithCulture_CorrectFormat(string cultureName, DateTime date, string expected)
        {
            m_dateConverter.Format = DateConverter.DateConverterFormat.Text;
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName); //To force localized strings

            var actual = m_dateConverter.Convert<string>(date, CultureInfo.CurrentCulture);

            actual.Should().Be(expected);
        }

      
    }
}