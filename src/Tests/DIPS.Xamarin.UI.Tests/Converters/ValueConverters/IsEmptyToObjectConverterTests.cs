using DIPS.Xamarin.UI.Converters.ValueConverters;
using DIPS.Xamarin.UI.Tests.TestHelpers;
using FluentAssertions;
using Xunit;

namespace DIPS.Xamarin.UI.Tests.Converters.ValueConverters
{
    public class IsEmptyToObjectConverterTests
    {
        private readonly IsEmptyToObjectConverter m_isEmptyToObjectConverter = new IsEmptyToObjectConverter();

        [Theory]
        [InlineData("Non-empty-string", "FalseObject")]
        [InlineData("", "TrueObject")]
        public void Convert_WithInput_NonInverted_CorrectOutput(object input, object expected)
        {
            m_isEmptyToObjectConverter.TrueObject = "TrueObject";
            m_isEmptyToObjectConverter.FalseObject = "FalseObject";

            var actual = m_isEmptyToObjectConverter.Convert<object>(input);

            actual.Should().BeOfType(typeof(string));
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData("Non-empty-string", "TrueObject")]
        [InlineData("", "FalseObject")]
        public void Convert_WithInput_Inverted_CorrectOutput(object input, object expected)
        {
            m_isEmptyToObjectConverter.Inverted = true;
            m_isEmptyToObjectConverter.TrueObject = "TrueObject";
            m_isEmptyToObjectConverter.FalseObject = "FalseObject";

            var actual = m_isEmptyToObjectConverter.Convert<object>(input);

            actual.Should().BeOfType(typeof(string));
            actual.Should().Be(expected);
        }
    }
}