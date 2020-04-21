using DIPS.Xamarin.UI.Android;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("DIPS.Xamarin.UI.Effects")]
[assembly: ExportEffect(typeof(BoxViewNoElevationEffect), nameof(BoxViewNoElevationEffect))]

namespace DIPS.Xamarin.UI.Android
{
    public class BoxViewNoElevationEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Container is BoxRenderer renderer)
            {
                renderer.OutlineProvider = null;
            }
        }

        protected override void OnDetached()
        {
        }
    }
}