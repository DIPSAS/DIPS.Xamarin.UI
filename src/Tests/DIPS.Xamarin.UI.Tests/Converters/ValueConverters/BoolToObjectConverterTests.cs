using System;
using System.Collections.Generic;
using DIPS.Xamarin.UI.Converters.ValueConverters;
using FluentAssertions;
using Xamarin.Forms.Xaml;
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
        public void Convert_TrueFalseObjectsAreDifferent_ThrowsXamlParseException()
        {
            m_boolToObjectConverter.TrueObject = "2.0";
            m_boolToObjectConverter.FalseObject = 2.0;

            Action act = () => m_boolToObjectConverter.Convert(false, null, null, null);

            act.Should().Throw<XamlParseException>();
        }

        [Fact]
        public void Convert_InputIsNull_XamlParseExceptionThrown()
        {
            Action act = () => m_boolToObjectConverter.Convert(null, null, null, null);

            act.Should().Throw<XamlParseException>();
        }

        [Fact]
        public void Convert_TrueObjectIsNull_XamlParseExceptionThrown()
        {
            m_boolToObjectConverter.TrueObject = null;
            m_boolToObjectConverter.FalseObject = "Something";
            Action act = () => m_boolToObjectConverter.Convert(true, null, null, null);

            act.Should().Throw<XamlParseException>();
        }
        
        [Fact]
        public void Convert_FalseObjectIsNull_XamlParseExceptionThrown()
        {
            m_boolToObjectConverter.FalseObject = null;
            m_boolToObjectConverter.TrueObject = "Something";
            Action act = () => m_boolToObjectConverter.Convert(true, null, null, null);

            act.Should().Throw<XamlParseException>();
        }
    }
}