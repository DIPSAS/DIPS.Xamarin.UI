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
using DIPS.Xamarin.UI.Internal.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Button = Xamarin.Forms.Button;

[assembly: ExportRenderer(typeof(LegacyButton), typeof(LegacyButtonRenderer))]

namespace DIPS.Xamarin.UI.Android
{

    /// Remove when this closes :https://github.com/xamarin/Xamarin.Forms/issues/10067
    public class LegacyButtonRenderer : global::Xamarin.Forms.Platform.Android.ButtonRenderer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public LegacyButtonRenderer(Context context) : base(context)
        {
            
        }
    }
}