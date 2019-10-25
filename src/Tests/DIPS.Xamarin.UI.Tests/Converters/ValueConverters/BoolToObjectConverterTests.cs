using System;
using System.Collections.Generic;
using DIPS.Xamarin.UI.Converters.ValueConverters;
using FluentAssertions;
using Xunit;

namespace DIPS.Xamarin.UI.Tests.Converters.ValueConverters
{
    public class BoolToObjectConverterTests
    {
        private readonly BoolToObjectConverter m_boolToObjectConverter;

        public BoolToObjectConverterTests()
        {
            m_boolToObjectConverter = new BoolToObjectConverter();
        }

        [Theory]
        [InlineData(10, 20)]
        [InlineData("True string", "False string")]
        [InlineData(0.01, 0.02)]
        [InlineData(0.01f, 0.02f)]
        [InlineData(10, 20, true)]
        [InlineData("True string", "False string", true)]
        [InlineData(0.01, 0.02, true)]
        [InlineData(0.01f, 0.02f, true)]

        public void Convert_WhenValueIsTrue_CorrectOutput(object trueObject, object falseObject, bool inverted = false)
        {
            m_boolToObjectConverter.TrueObject = trueObject;
            m_boolToObjectConverter.FalseObject = falseObject;
            m_boolToObjectConverter.Inverted = inverted;

            var result = m_boolToObjectConverter.Convert(true, null, null, null);

            result.Should().Be(inverted ? falseObject : trueObject);
        }

        [Theory]
        [InlineData(10, 20)]
        [InlineData("True string", "False string")]
        [InlineData(0.01, 0.02)]
        [InlineData(0.01f, 0.02f)]
        [InlineData(10, 20, true)]
        [InlineData("True string", "False string", true)]
        [InlineData(0.01, 0.02, true)]
        [InlineData(0.01f, 0.02f, true)]

        public void Convert_WhenValueIsFalse_CorrectOutput(object trueObject, object falseObject, bool inverted = false)
        {
            m_boolToObjectConverter.TrueObject = trueObject;
            m_boolToObjectConverter.FalseObject = falseObject;
            m_boolToObjectConverter.Inverted = inverted;

            var result = m_boolToObjectConverter.Convert(false, null, null, null);

            result.Should().Be(inverted ? trueObject : falseObject);
        }

        [Fact]
        public void Convert_TrueFalseObjectsAreDifferent_ThrowsArgumentException()
        {
            m_boolToObjectConverter.TrueObject = "2.0";
            m_boolToObjectConverter.FalseObject = 2.0;

            Action act = () => m_boolToObjectConverter.Convert(false, null, null, null);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Convert_InputIsNull_ThrowsArgumentException()
        {
            Action act = () => m_boolToObjectConverter.Convert(null, null, null, null);

            act.Should().Throw<ArgumentException>();
        }
    }
}