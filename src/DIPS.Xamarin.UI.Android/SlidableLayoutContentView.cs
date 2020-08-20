using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DIPS.Xamarin.UI.Android;
using DIPS.Xamarin.UI.Controls.Slidable;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SlidableLayout), typeof(SlidableLayoutContentView))]
namespace DIPS.Xamarin.UI.Android
{
    internal class SlidableLayoutContentView : ViewRenderer
    {
        public SlidableLayoutContentView(Context context) : base(context)
        {
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            if (ev.ActionMasked == MotionEventActions.Move)
            {
                return true;
            }

            return base.OnInterceptTouchEvent(ev);
        }
    }
}