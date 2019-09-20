using System;
using DIPS.Xamarin.UI.Converters.ValueConverters;
using FluentAssertions;
using Xunit;

namespace DIPS.Xamarin.UI.Tests.Converters.ValueConverters
{
    public class InvertedBoolConverterTests
    {
        private InvertedBoolConverter m_invertedBoolConverter;

        public InvertedBoolConverterTests()
        {
            m_invertedBoolConverter = new InvertedBoolConverter();
        }

        [Fact]
        public void Convert_InvalidValue_ThrowsArgumentException()
        {
            try
            {
                m_invertedBoolConverter.Convert("Not a bool", null, null, null);
                true.Should().Be(false);
            }
            catch (Exception exception)
            {
                exception.Should().BeOfType<ArgumentException>();
            }
        }

        [Fact]
        public void Convert_ValueIsNull_ThrowsArgumentException()
        {
            try
            {
                m_invertedBoolConverter.Convert(null, null, null, null);
                true.Should().Be(false);
            }
            catch (Exception exception)
            {
                exception.Should().BeOfType<ArgumentException>();
            }
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Convert_BooleanValue_InvertedResult(bool value)
        {
            var result = m_invertedBoolConverter.Convert(value, null, null, null);
            result.Should().Be(!value);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ConvertBack_BooleanValue_IsNotInverted(bool value)
        {
            var result = m_invertedBoolConverter.ConvertBack(value, null, null, null);
            result.Should().Be(value);
        }
    }
}
