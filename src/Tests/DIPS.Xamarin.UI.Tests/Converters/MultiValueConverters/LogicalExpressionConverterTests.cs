using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DIPS.Xamarin.UI.Converters.MultiValueConverters;
using FluentAssertions;
using Xunit;
using DIPS.Xamarin.UI.Tests.TestHelpers;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Tests.Converters.MultiValueConverters
{
    public class LogicalExpressionConverterTests
    {
        private LogicalExpressionConverter m_logicalExpressionConverter;

        public LogicalExpressionConverterTests()
        {
            m_logicalExpressionConverter = new LogicalExpressionConverter();
        }


        [Fact]
        public void Convert_ValuesAreNull_ReturnsFalse()
        {
            var output = m_logicalExpressionConverter.Convert<bool>(null);

            output.Should().BeFalse(because: "empty input can not be converted and converter should return false");
        }


        [Fact]
        public void Convert_AllValuesInInputAreNull_ReturnsFalse()
        {
            var output = m_logicalExpressionConverter.Convert<bool>(new object[] { null, null });

            output.Should().BeFalse(because: "if all values are null it can not be converted and converter should return false");
        }

        [Fact]
        public void Convert_AnyValuesInInputAreNull_ReturnsFalse()
        {
            var output = m_logicalExpressionConverter.Convert<bool>(new object[] { true, null });

            output.Should().BeFalse("if any values are null it can not be converted and converter should return false");
        }

        [Theory]
        [InlineData(new object[] { new object[] { false, false }, false })]
        [InlineData(new object[] { new object[] { true, false}, false})]
        [InlineData(new object[] { new object[] { false, true}, false})]
        [InlineData(new object[] { new object[] { true, true}, true})]
        public void Convert_And_ShouldReturnAsExpected(object[] inputs, bool expectedOutput)
        {
            m_logicalExpressionConverter.LogicalGate = LogicalGate.And;
            var output = m_logicalExpressionConverter.Convert<bool>(inputs);

            output.Should().Be(expectedOutput);
        }

        [Theory]
        [InlineData(new object[] { new object[] { false, false }, true })]
        [InlineData(new object[] { new object[] { true, false }, true })]
        [InlineData(new object[] { new object[] { false, true }, true })]
        [InlineData(new object[] { new object[] { true, true }, false })]
        public void Convert_Nand_ShouldReturnAsExpected(object[] inputs, bool expectedOutput)
        {
            m_logicalExpressionConverter.LogicalGate = LogicalGate.Nand;
            var output = m_logicalExpressionConverter.Convert<bool>(inputs);

            output.Should().Be(expectedOutput);
        }

        [Theory]
        [InlineData(new object[] { new object[] { false, false }, true })]
        [InlineData(new object[] { new object[] { true, false }, false })]
        [InlineData(new object[] { new object[] { false, true }, false })]
        [InlineData(new object[] { new object[] { true, true }, true })]
        public void Convert_Xand_ShouldReturnAsExpected(object[] inputs, bool expectedOutput)
        {
            m_logicalExpressionConverter.LogicalGate = LogicalGate.Xand;
            var output = m_logicalExpressionConverter.Convert<bool>(inputs);

            output.Should().Be(expectedOutput);
        }

        [Theory]
        [InlineData(new object[] { new object[] { false, false }, false })]
        [InlineData(new object[] { new object[] { true, false }, true })]
        [InlineData(new object[] { new object[] { false, true }, true })]
        [InlineData(new object[] { new object[] { true, true }, true })]
        public void Convert_Or_ShouldReturnAsExpected(object[] inputs, bool expectedOutput)
        {
            m_logicalExpressionConverter.LogicalGate = LogicalGate.Or;
            var output = m_logicalExpressionConverter.Convert<bool>(inputs);

            output.Should().Be(expectedOutput);
        }

        [Theory]
        [InlineData(new object[] { new object[] { false, false }, true })]
        [InlineData(new object[] { new object[] { true, false }, false })]
        [InlineData(new object[] { new object[] { false, true }, false })]
        [InlineData(new object[] { new object[] { true, true }, false })]
        public void Convert_Nor_ShouldReturnAsExpected(object[] inputs, bool expectedOutput)
        {
            m_logicalExpressionConverter.LogicalGate = LogicalGate.Nor;
            var output = m_logicalExpressionConverter.Convert<bool>(inputs);

            output.Should().Be(expectedOutput);
        }

        [Theory]
        [InlineData(new object[] { new object[] { false, false }, false })]
        [InlineData(new object[] { new object[] { true, false }, true })]
        [InlineData(new object[] { new object[] { false, true }, true })]
        [InlineData(new object[] { new object[] { true, true }, false })]
        public void Convert_Xor_ShouldReturnAsExpected(object[] inputs, bool expectedOutput)
        {
            m_logicalExpressionConverter.LogicalGate = LogicalGate.Xor;
            var output = m_logicalExpressionConverter.Convert<bool>(inputs);

            output.Should().Be(expectedOutput);
        }

    }
}