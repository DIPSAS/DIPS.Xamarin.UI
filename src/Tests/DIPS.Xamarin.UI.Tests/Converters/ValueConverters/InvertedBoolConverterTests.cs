using System;
using System.Collections.Generic;
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

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Convert_BooleanValue_InvertedResult(bool value)
        {
            var result = m_invertedBoolConverter.Convert(value, null, null, null);
            result.Should().Be(!value);
        }


        [Theory]
        [InlineData("Not a bool")]
        [InlineData(0)]
        [InlineData(0.1)]
        [InlineData(null)]
        public void Convert_ValueIsNull_ThrowsArgumentException(object value)
        {
            Action act = () => m_invertedBoolConverter.Convert(value, null, null, null);

            act.Should().Throw<ArgumentException>();
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
