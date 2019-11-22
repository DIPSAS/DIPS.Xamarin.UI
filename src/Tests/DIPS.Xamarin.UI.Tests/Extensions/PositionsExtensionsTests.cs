using System;
using Xunit;
using DIPS.Xamarin.UI.Extensions;
using FluentAssertions;

namespace DIPS.Xamarin.UI.Tests.Extensions
{
    public class PositionsExtensionsTests
    {
        [Fact]
        public void CalculateRelativePosition_Center()
        {
            var result = (5.0).CalculateRelativePosition(0, 10);

            result.Should().BeApproximately(0.5, 0.001);
        }

        [Fact]
        public void CalculateRelativePosition_OneThird()
        {
            var result = (5.0).CalculateRelativePosition(0, 15);

            result.Should().BeApproximately(0.3333, 0.01);
        }

        [Fact]
        public void CalculateRelativePosition_Above()
        {
            var result = (5.0).CalculateRelativePosition(0, 3);

            result.Should().BeApproximately(1, 0.01);
        }

        [Fact]
        public void CalculateRelativePosition_Below()
        {
            var result = (5.0).CalculateRelativePosition(10, 20);

            result.Should().BeApproximately(0, 0.01);
        }
    }
}
