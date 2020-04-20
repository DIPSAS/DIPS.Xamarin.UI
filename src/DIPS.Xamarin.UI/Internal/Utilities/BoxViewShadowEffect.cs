using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Internal.Utilities
{
    public class BoxViewShadowEffect : RoutingEffect
    {
        public BoxViewShadowEffect() : base($"DIPS.Xamarin.UI.Effects.{nameof(BoxViewShadowEffect)}")
        {
        }
    }
}
