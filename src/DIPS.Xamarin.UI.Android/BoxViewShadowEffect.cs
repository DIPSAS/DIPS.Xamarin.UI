using DIPS.Xamarin.UI.Android;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("DIPS.Xamarin.UI.Effects")]
[assembly: ExportEffect(typeof(BoxViewShadowEffect), nameof(BoxViewShadowEffect))]

namespace DIPS.Xamarin.UI.Android
{
    public class BoxViewShadowEffect : PlatformEffect
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