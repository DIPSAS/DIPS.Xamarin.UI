using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DIPS.Xamarin.UI.Internal.Utilities
{
    internal class BoxViewNoElevationEffect : RoutingEffect
    {
        internal BoxViewNoElevationEffect() : base($"DIPS.Xamarin.UI.Effects.{nameof(BoxViewNoElevationEffect)}")
        {
        }
    }
}
