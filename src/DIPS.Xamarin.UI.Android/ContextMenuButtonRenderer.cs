using DIPS.Xamarin.UI.Android;
using DIPS.Xamarin.UI.Controls.ContextMenu;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ContextMenuButton), typeof(ContextMenuButtonRenderer))]

namespace DIPS.Xamarin.UI.Android
{
    public class ContextMenuButtonRenderer : ButtonRenderer
    {
        internal static void Initialize() { }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
        }
    }
}