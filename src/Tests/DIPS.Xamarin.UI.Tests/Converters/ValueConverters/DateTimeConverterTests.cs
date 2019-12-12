using System;
using System.Globalization;
using FluentAssertions;
using Xunit;
using DIPS.Xamarin.UI.Tests.TestHelpers;
using DateTimeConverter = DIPS.Xamarin.UI.Converters.ValueConverters.DateTimeConverter;
using DateTimeFormat = DIPS.Xamarin.UI.Converters.ValueConverters.DateTimeConverter.DateTimeFormat;

namespace DIPS.Xamarin.UI.Tests.Converters.ValueConverters
{
    public class DateTimeConverterTests
    {
        private readonly DateTime m_input = new DateTime(1991, 12, 12, 09, 09, 00);
        private readonly DateTimeConverter m_dateTimeConverter = new DateTimeConverter();

        [Theory]
        [InlineData(0)]
        [InlineData(0.0)]
        [InlineData(0.0f)]
        [InlineData("test")]
        public void Convert_InvalidInput_ThrowsArgumentException(object invalidInput)
        {
            Action act = () => m_dateTimeConverter.Convert<string>(invalidInput);

            act.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(DateTimeFormat.Default ,"no","12. des. 1991 kl 09:09")]
        [InlineData(DateTimeFormat.Default, "en","12. dec 1991 09:09")]
        public void Convert_WithFormat_WithCulture_CorrectFormat(DateTimeFormat format, string cultureName, string expected)
        {
            m_dateTimeConverter.Format = format;

            var actual = m_dateTimeConverter.Convert<string>(m_input, new CultureInfo(cultureName));

            actual.Should().Be(expected);
        }
    }
}
