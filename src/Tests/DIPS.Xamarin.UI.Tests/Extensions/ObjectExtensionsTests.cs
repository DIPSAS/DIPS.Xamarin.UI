using System;
using Xunit;
using DIPS.Xamarin.UI.Extensions;
using FluentAssertions;

namespace DIPS.Xamarin.UI.Tests.Extensions
{
    public class ObjectExtensionsTests
    {
        [Fact]
        public void PropertyExists_DoubleReturned()
        {
            var obj = new TypeTest();

            var result = obj.ExtractDouble("Value", 1);

            result.Should().Be(42);
        }

        [Fact]
        public void PropertyDoesNotExist_ToStringDoubleReturned()
        {
            var obj = new TypeTest();

            var result = obj.ExtractDouble(string.Empty, 1);

            result.Should().Be(1337);
        }

        [Fact]
        public void PropertyDoesNotExist_InvalidToString_DefaultReturned()
        {
            var obj = new TypeTest_InvalidTostring();

            var result = obj.ExtractDouble("Value2", 1);

            result.Should().Be(1);
        }

        public class TypeTest
        {
            public double Value { get; set; } = 42;
            public override string ToString()
            {
                return "1337";
            }
        }

        public class TypeTest_InvalidTostring
        {
            public override string ToString()
            {
                return "abc";
            }
        }
    }
}
