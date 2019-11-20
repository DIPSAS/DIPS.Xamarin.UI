using DIPS.Xamarin.UI.Extensions;
using FluentAssertions;
using Xamarin.Forms;
using Xunit;

namespace DIPS.Xamarin.UI.Tests.Extensions
{
    public class PositionExtensions
    {
        [Fact]
        public void NoParent_ChildsX()
        {
            var child = new Frame();
            var relativeLayout = new RelativeLayout();
            relativeLayout.Children.Add(child, Constraint.RelativeToParent(r => 42));

            var x = child.X;

            x.Should().Be(0);
        }
    }
}
