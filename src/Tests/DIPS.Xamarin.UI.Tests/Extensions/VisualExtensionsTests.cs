using DIPS.Xamarin.UI.Extensions;
using FluentAssertions;
using Xamarin.Forms;
using Xunit;

namespace DIPS.Xamarin.UI.Tests.Extensions
{
    public class VisualExtensionsTests
    {
        [Fact]
        public void HasParent_ParentFound()
        {
            var child = new Frame();
            var relativeLayout = new RelativeLayout();
            relativeLayout.Children.Add(child, Constraint.RelativeToParent(r => 42));

            var parent = child.GetParentOfType<RelativeLayout>();

            parent.Should().Be(relativeLayout);
        }

        [Fact]
        public void HasParent_WrongType_Null()
        {
            var child = new Frame();
            var relativeLayout = new RelativeLayout();
            relativeLayout.Children.Add(child, Constraint.RelativeToParent(r => 42));

            var parent = child.GetParentOfType<AbsoluteLayout>();

            parent.Should().BeNull();
        }
    }
}
