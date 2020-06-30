using System;
using System.Collections.Generic;
using System.Text;
using DIPS.Xamarin.UI.Converters.ValueConverters;
using DIPS.Xamarin.UI.Tests.TestHelpers;
using FluentAssertions;
using Xamarin.Forms.Xaml;
using Xunit;

namespace DIPS.Xamarin.UI.Tests.Converters.ValueConverters
{
    public class TypeToObjectConverterTests
    {
        private const string ExpectedTrueObject = "true string";
        private const string ExpectedFalseObject = "false string";
        private const string InputObject = "input string";
        private TypeToObjectConverter m_typeToObjectConverter = new TypeToObjectConverter();


        [Fact]
        public void Convert_ValueIsNull_ThrowsXamlParseException()
        {
            m_typeToObjectConverter.TrueObject = ExpectedTrueObject;
            m_typeToObjectConverter.FalseObject = ExpectedFalseObject;
            m_typeToObjectConverter.Type = typeof(string);

            Action act = () => m_typeToObjectConverter.Convert<object>(null);

            act.Should().Throw<XamlParseException>().Where(ex => ex.Message.Contains("value")).And.Message.Contains("null");
        }

        [Fact]
        public void Convert_TrueObjectIsNull_ThrowsXamlParseException()
        {
            m_typeToObjectConverter.FalseObject = ExpectedFalseObject;
            m_typeToObjectConverter.Type = typeof(string);

            Action act = () => m_typeToObjectConverter.Convert<object>(InputObject);

            act.Should().Throw<XamlParseException>().Where(ex => ex.Message.Contains("TrueObject")).And.Message.Contains("null");
        }
        [Fact]
        public void Convert_FalseObjectIsNull_ThrowsXamlParseException()
        {
            m_typeToObjectConverter.TrueObject = ExpectedTrueObject;
            m_typeToObjectConverter.Type = typeof(string);

            Action act = () => m_typeToObjectConverter.Convert<object>(InputObject);

            act.Should().Throw<XamlParseException>().Where(ex => ex.Message.Contains("FalseObject")).And.Message.Contains("null");
        }

        [Fact]
        public void Convert_ValueIsSameType_TrueObjectAsOutput()
        {
            m_typeToObjectConverter.TrueObject = ExpectedTrueObject;
            m_typeToObjectConverter.FalseObject = ExpectedFalseObject;
            m_typeToObjectConverter.Type = typeof(string);

            var actualOutput = m_typeToObjectConverter.Convert<object>(InputObject);

            actualOutput.Should().Be(ExpectedTrueObject);
        }
        [Fact]
        public void Convert_ValueIsNotSameType_FalseObjectAsOutput()
        {
            m_typeToObjectConverter.TrueObject = ExpectedTrueObject;
            m_typeToObjectConverter.FalseObject = ExpectedFalseObject;
            m_typeToObjectConverter.Type = typeof(string);

            var actualOutput = m_typeToObjectConverter.Convert<object>(1);

            actualOutput.Should().Be(ExpectedFalseObject);
        }
    }
}
