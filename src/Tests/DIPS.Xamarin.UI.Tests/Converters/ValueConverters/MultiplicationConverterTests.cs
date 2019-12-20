using System;
using DIPS.Xamarin.UI.Converters.ValueConverters;
using FluentAssertions;
using Xunit;

namespace DIPS.Xamarin.UI.Tests.Converters.ValueConverters
{
    public class MultiplicationConverterTests
    {
        private readonly MultiplicationConverter m_multiplicationConverter = new MultiplicationConverter();

        [Theory]
        [InlineData(1f, 2f, 2)]
        [InlineData(1, 2, 2)]
        [InlineData(1.0, 2.0, 2.0)]
        [InlineData((ulong)1.0, (ulong)2, 2)]
        [InlineData((uint)1, (uint)2, 2)]
        [InlineData((long)1.0, (long)2.0, 2)]
        [InlineData((ushort)1.0, (ushort)2.0, 2)]
        [InlineData((short)1.0, (short)2.0, 2)]
        [InlineData((byte)1.0, (byte)2.0, 2)]
        public void Convert_WithValueAndFactor_CorrectMultiplication(object value, object factor, double expected)
        {
            m_multiplicationConverter.Factor = factor;
            var actual = m_multiplicationConverter.Convert(value, null, null, null);

            actual.Should().Be(expected);
        }

        [Fact]
        public void Convert_Decimal_CorrectMultiplication()
        {
            var expected = 2;
            m_multiplicationConverter.Factor = 2.0M;
            var actual = m_multiplicationConverter.Convert(1.0M, null, null, null);
            actual.Should().Be(expected);
        }

        [Fact]
        public void Convert_ValueIsNull_ArgumentExceptionThrown()
        {
            Action act = () => m_multiplicationConverter.Convert(null, null, null, null);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Convert_FactorIsNull_ArgumentExceptionThrown()
        {
            Action act = () => m_multiplicationConverter.Convert(1.0, null, null, null);

            act.Should().Throw<ArgumentException>();
        }
    }
}