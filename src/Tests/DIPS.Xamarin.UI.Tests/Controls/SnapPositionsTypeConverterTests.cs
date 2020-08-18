using System;
using Xunit;
using DIPS.Xamarin.UI.Controls.Sheet;
using FluentAssertions;
using FluentAssertions.Common;

namespace DIPS.Xamarin.UI.Tests.Controls
{
    public class SnapPositionsTypeConverterTests
    {
        [Theory]
        [InlineData(new object[] { "", new double[0]})]
        [InlineData(new object[] { "0.5", new double[] { 0.5 } })]
        [InlineData(new object[] { "0.5, 0.7, 0.9", new double[] { 0.5, 0.7, 0.9 } })]
        [InlineData(new object[] { "0.5,0.7,0.9", new double[] { 0.5, 0.7, 0.9 } })]
        [InlineData(new object[] { "0.5 0.7 0.9", new double[] { 0.5, 0.7, 0.9 } })]
        public void ConvertFromInvariantString(string input, double[] expected)
        {
            var typeConverter = new SnapPositionsTypeConverter();

            var result = typeConverter.ConvertFromInvariantString(input);

            if(expected == null)
            {
                result.Should().BeNull();
            }
            else
            {
                result.Should().IsSameOrEqualTo(expected);

            }
        }
    }
}
