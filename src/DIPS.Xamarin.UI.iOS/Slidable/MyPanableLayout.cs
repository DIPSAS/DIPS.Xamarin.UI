using System;
using DIPS.Xamarin.UI.Controls.Slidable;
using DIPS.Xamarin.UI.iOS.Slidable;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PanableLayout), typeof(MyPanableLayout))]
namespace DIPS.Xamarin.UI.iOS.Slidable
{
    public class MyPanableLayout : ViewRenderer
    {
        public MyPanableLayout()
        {
        }
    }
}
