using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Internal.Utilities
{
    public class BoxViewNoElevationEffect : RoutingEffect
    {
        public BoxViewNoElevationEffect() : base($"DIPS.Xamarin.UI.Effects.{nameof(BoxViewNoElevationEffect)}")
        {
        }
    }
}
